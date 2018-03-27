using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Newtonsoft.Json.Linq;
using WebServiceLib;
using WebServiceLib.Common;

namespace WebService2017.Controllers
{
    public class SimpDbServerController : ApiController
    {
        public static readonly SimpDbServer SimpDbServer = new SimpDbServer();

        [HttpPost]
        [ActionName("DataRequest_By_String")]
        public string DataRequest_By_String(dynamic methodRequests)
        {
            if (string.IsNullOrEmpty(methodRequests.ToString()))
            {
                return "";
            }
            return SimpDbServer.DataRequest_By_String(methodRequests.ToString());
        }
      
        [HttpPost]
        [ActionName("DataRequest_By_DataTable")]
        public DataTable DataRequest_By_DataTable(dynamic methodRequests)
        {
            if (string.IsNullOrEmpty(methodRequests.ToString()))
            {
                return new DataTable();
            }
            return SimpDbServer.DataRequest_By_DataTable(methodRequests.ToString());
        }
        [HttpPost]
        [ActionName("DataRequest_By_JsonString")]
        public string DataRequest_By_JsonString(dynamic methodRequests)
        {
            return SimpDbServer.DataRequest_By_JsonString(methodRequests.ToString());
        }

        [HttpPost]
        [ActionName("DataRequest_By_SimpDEs")]
        public string DataRequest_By_SimpDEs(string methodRequests)
        {
            return SimpDbServer.DataRequest_By_SimpDEs(methodRequests);
        }
        [HttpPost]
        [ActionName("DataRequest_By_SimpDEs_GZip")]
        public byte[] DataRequest_By_SimpDEs_GZip(string methodRequests)
        {
            return SimpDbServer.DataRequest_By_SimpDEs_GZip(methodRequests);
        }
        [HttpPost]
        [ActionName("DataRequest_By_SimpDEs_All_GZip")]
        public byte[] DataRequest_By_SimpDEs_All_GZip(byte[] methodBts)
        {
            return SimpDbServer.DataRequest_By_SimpDEs_All_GZip(methodBts);
        }
    }
}
