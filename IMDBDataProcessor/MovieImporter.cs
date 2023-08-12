using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Configuration;
using System.Data.SqlClient;
using System.Text;
using System.Threading.Tasks;
using IMDBDataProcessor.dao;
using Models;
using Newtonsoft.Json;

namespace IMDBDataProcessor
{
    public class MovieImporter
    {
        private static readonly IDao<Movie, int> _dao = new MovieDao();
        public static void Import()
        {
            var assembly = Assembly.GetExecutingAssembly();
            using var stream = assembly.GetManifestResourceStream("IMDBDataProcessor.data.movies-1972-2016.json");
            using var reader = new StreamReader(stream);
            var json = reader.ReadToEnd();
            var ids = Import(json);

            foreach (var movie in TmdbApiClient.GetMovies(ids))
            {
                _dao.Save(movie);
            }
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

        internal class MovieList
        {
            [JsonProperty("@type")]
            public string Type { get; set; }
            [JsonProperty("itemListElement")]
            public List<MovieListItem> ItemListElement { get; set; }
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
