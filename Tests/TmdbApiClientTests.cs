using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using IMDBDataProcessor;
using RestSharp;
using Utils;

namespace Tests
{
    public class TmdbApiClientTests
    {
        [Test]
        public void GetMovie_Fail()
        {
            var failClient = new FailRestClientWrapper();
            var movie = TmdbApiClient.GetMovie(failClient, "tt1881109", 0);

            Assert.IsNull(movie);
        }

        internal class FailRestClientWrapper : IRestClientWrapper
        {
            public RestResponse Get(RestRequest request)
            {
                return new RestResponse
                {
                    ResponseStatus = ResponseStatus.Completed,
                    IsSuccessStatusCode = false,
                    StatusCode = HttpStatusCode.InternalServerError,
                    ErrorMessage = request.ToString()
                };
            }
        }

        [Test]
        public void GetMovie_Empty()
        {
            var emptyClient = new EmptyRestClientWrapper();
            var movie = TmdbApiClient.GetMovie(emptyClient, "tt1881109", 0);
            Assert.IsNull(movie);
        }

        internal class EmptyRestClientWrapper : IRestClientWrapper
        {
            public RestResponse Get(RestRequest request)
            {
                return new RestResponse
                {
                    ResponseStatus = ResponseStatus.Completed,
                    StatusCode = HttpStatusCode.OK,
                    IsSuccessStatusCode = true,
                    Content = @"{""movie_results"":[]}"
                };
            }
        }
    }
}
