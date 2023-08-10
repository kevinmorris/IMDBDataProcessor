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
        }

        public static List<string> Import(string json)
        {
            var items = JsonConvert.DeserializeObject<List<MovieListItem>>(json);
            return items != null 
                ? items.Select(i =>
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
