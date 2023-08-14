using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IMDBDataProcessor.dao;
using Moq;
#pragma warning disable NUnit2005

namespace Tests
{
    public class MovieDaoTest
    {
        [Test]
        public void ExtractMovie()
        {
            var results = new Mock<IDataReader>();
            results.Setup(r => r["tmdb_id"]).Returns(1);
            results.Setup(r => r["imdb_id"]).Returns("tt1881109");
            results.Setup(r => r["title"]).Returns("Alpha");
            results.Setup(r => r["release_date"]).Returns("08-17-2018");
            results.Setup(r => r["popularity"]).Returns(1.0);

            var movie = MovieDao.ExtractMovie(results.Object);

            Assert.AreEqual(1, movie.TmdbId);
            Assert.AreEqual("tt1881109", movie.ImdbId);
            Assert.AreEqual("Alpha", movie.Title);
            Assert.AreEqual(new DateOnly(2018, 8, 17), movie.ReleaseDate);
            Assert.AreEqual(1.0, movie.Popularity);
        }
    }
}
