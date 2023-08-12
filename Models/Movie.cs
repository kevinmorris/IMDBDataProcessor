using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class Movie
    {
        [JsonProperty("id")]
        public int TmdbId { get; set; }
        public string ImdbId { get; set; }

        [JsonProperty("title")]
        public string Title { get; set; }

        [JsonProperty("release_date")]
        public DateOnly ReleaseDate { get; set; }

        [JsonProperty("popularity")]
        public double Popularity { get; set; }
    }
}
