﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RestSharp;

namespace Utils
{
    public interface IRestClientWrapper
    {
        public RestResponse Get(RestRequest request);
    }
}
