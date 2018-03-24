using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace WebApi2017.Controllers
{
    public class SimpDbServerController : ApiController
    {
        public string Get()
        {
            return "Hello World";
        }
    }
}
