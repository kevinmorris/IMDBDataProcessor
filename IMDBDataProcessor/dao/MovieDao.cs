using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Models;

namespace IMDBDataProcessor.dao
{
    public class MovieDao : IDao<Movie, int>
    {
        private readonly string _connStr;

        public MovieDao()
        {
            _connStr = ConfigurationManager.AppSettings["SqlServerConnectionString"];

        }
        public Movie Get(int id)
        {
            throw new NotImplementedException();
        }

        public IList<Movie> GetAll()
        {
            using var conn = new SqlConnection(_connStr);
            conn.Open();

            var cmd = new SqlCommand("SELECT * FROM Movies", conn);
            var movies = new List<Movie>();

            var results = cmd.ExecuteReader();
            while (results.Read())
            {
                movies.Add(new Movie
                {
                    TmdbId = (int)results["tmdb_id"],
                    ImdbId = (string)results["imdb_id"],
                    Title = (string)results["title"],
                    ReleaseDate = DateOnly.ParseExact((string)results["release_date"], "MM-dd-yyyy"),
                    Popularity = (double)results["popularity"]
                });
            }

            return movies;
        }

        public void Save(Movie item)
        {
            using var conn = new SqlConnection(_connStr);
            conn.Open();

            Save(item, conn);
        }

        private static void Save(Movie movie, SqlConnection conn)
        {
            var countCmd = new SqlCommand("SELECT COUNT(*) FROM Movies WHERE tmdb_id = @tmdb_id", conn);
            countCmd.Parameters.AddWithValue("@tmdb_id", movie.TmdbId);
            var count = (int)countCmd.ExecuteScalar();

            var cmd = count > 0
                ? new SqlCommand(
                    "UPDATE Movies SET imdb_id = @imdb_id, title = @title, release_date = @release_date, popularity = @popularity WHERE tmdb_id = @tmdb_id",
                    conn)
                : new SqlCommand(
                    "INSERT INTO Movies VALUES (@tmdb_id, @imdb_id, @title, @release_date, @popularity)",
                    conn);

            cmd.Parameters.AddWithValue("@tmdb_id", movie.TmdbId);
            cmd.Parameters.AddWithValue("@imdb_id", movie.ImdbId);
            cmd.Parameters.AddWithValue("@title", movie.Title);
            cmd.Parameters.AddWithValue("@release_date", movie.ReleaseDate.ToString("MM-dd-yyyy"));
            cmd.Parameters.AddWithValue("@popularity", movie.Popularity);
            cmd.ExecuteNonQuery();

            Console.WriteLine($"Saved {movie.Title}");
        }

        public void SaveAll(IList<Movie> items)
        {
            using var conn = new SqlConnection(_connStr);
            conn.Open();

            foreach (var movie in items)
            {
                Save(movie, conn);
            }
        }
    }
}
