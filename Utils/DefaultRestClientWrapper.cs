using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RestSharp;

namespace Utils
{
    public class DefaultRestClient : IRestClientWrapper
    {
        public RestClient Client { get; set; }

        public DefaultRestClient(RestClient client)
        {
            Client = client;
        }

        public RestResponse Get(RestRequest request)
        {
            return Client.Get(request);
        }
    }
}
