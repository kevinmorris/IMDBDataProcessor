using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace IMDBDataProcessor
{
    public class MovieImporter
    {
        public static void Import()
        {
            var assembly = Assembly.GetExecutingAssembly();
            using var stream = assembly.GetManifestResourceStream("IMDBDataProcessor.data.movies-1972-2016.json");
            using var reader = new StreamReader(stream);
            var json = reader.ReadToEnd();
            var ids = Import(json);
            var movies = TmdbApiClient.GetMovies(ids);
        }

        public static List<string> Import(string json)
        {
            var list = JsonConvert.DeserializeObject<MovieList>(json);
            return list is { ItemListElement: not null } 
                ? list.ItemListElement.Select(i =>
                {
                    //Pull out the IMDB ID from the URL
                    //URL is of the form: /title/{id}/
                    var urlParts = i.Url.Split('/');
                    return urlParts[2];
                }).ToList()
                : new List<string>();
        }

        internal class MovieListItem
        {
            [JsonProperty("@type")]
            public string Type { get; set; }

            [JsonProperty("position")]
            public string Position { get; set; }

            [JsonProperty("url")]
            public string Url { get; set; }
        }
    }
}
