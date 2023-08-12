using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Models;
using Newtonsoft.Json;
using RestSharp;
using RestSharp.Authenticators.OAuth2;
using Utils;

namespace IMDBDataProcessor
{
    public class TmdbApiClient
    {
        public static IEnumerable<Movie> GetMovies(IList<string> imdbIds)
        {
            var options = new RestClientOptions($"https://api.themoviedb.org/3/find/")
            {
                Authenticator = new OAuth2AuthorizationRequestHeaderAuthenticator(
                    ConfigurationManager.AppSettings["TmdbApiToken"], "Bearer")
            };

            var client = new DefaultRestClient(new RestClient(options));

            return imdbIds.Skip(2600)
                .Select((id, i) => GetMovie(client, id, i))
                .Where(movie => movie != null);
        }

        public static Movie GetMovie(IRestClientWrapper client, string imdbId, int index)
        {
            //Use RestSharp to get the movie from the TMDB API
            try
            {
                var request = new RestRequest(imdbId);
                request.AddParameter("external_source", "imdb_id");
                var response = client.Get(request);

                Console.WriteLine($"{index} {imdbId}: {response.StatusCode}");
                if (response.IsSuccessful && response.Content != null)
                {
                    var movieResponse = JsonConvert.DeserializeObject<MovieResponse>(response.Content);
                    if (movieResponse.MovieList.Count == 0)
                    {
                        Console.WriteLine($"No movie found for {imdbId}");
                        return null;
                    }

                    var movie = movieResponse.MovieList[0];
                    movie.ImdbId = imdbId;
                    return movie;
                }
                else
                {
                    Console.WriteLine($"Error getting movie {imdbId}: {response.ErrorMessage}");
                    return null;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine($"Error getting movie {imdbId}: {e.Message}");
                return null;
            }
        }

        internal class MovieResponse
        {
            [JsonProperty("movie_results")]
            public IList<Movie> MovieList { get; set; }
        }
    }
}
