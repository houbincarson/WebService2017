using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace WebServiceLib
{
    public interface ISimpDbServer
    {
        string DataRequest_By_String(string methodRequests);
        DataTable DataRequest_By_DataTable(string methodRequests);
        DataSet DataRequest_By_DataSet(string methodRequests);
        string DataRequest_By_SimpDEs(string methodRequests);
        byte[] DataRequest_By_SimpDEs_GZip(string methodRequests);
        byte[] DataRequest_By_SimpDEs_All_GZip(byte[] methodBts);
    }
}
