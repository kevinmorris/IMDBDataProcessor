﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using IMDBDataProcessor.dao;
using Models;

namespace IMDBDataProcessor
{
    public class FlatFileExporter
    {
        private static readonly IDao<Movie, int> _dao = new MovieDao();

        public static void Export()
        {
            var movies = _dao.GetAll();
            var sb = new StringBuilder();
            foreach (var movie in movies)
            {
                var padTmdbId = movie.TmdbId.ToString().PadRight(6);
                var padImdbId = movie.ImdbId.PadRight(9);
                var padTitle = movie.Title.PadRight(128);
                var padReleaseDate = movie.ReleaseDate.ToString("MM-dd-yyyy").PadRight(10);
                var padPopularity = movie.Popularity.ToString("0.0000").PadRight(10);
                sb.AppendLine($"{padTmdbId}{padImdbId}{padTitle}{padReleaseDate}{padPopularity}");

                Console.WriteLine($"Exported {movie.Title}");
            }

            File.WriteAllText("MOVIES.txt", sb.ToString());
        }
    }
}
