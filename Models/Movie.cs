using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class Movie
    {
        public int TmdbId { get; set; }
        public int ImdbId { get; set; }
        public string Title { get; set; }
        public DateOnly ReleaseDate { get; set; }
        public double Popularity { get; set; }
    }
}
