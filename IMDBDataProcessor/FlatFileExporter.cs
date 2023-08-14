using System;
using System.Collections.Generic;
using System.Linq;
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
        }
    }
}
