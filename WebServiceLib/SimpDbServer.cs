using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Web.Script.Serialization;
using Newtonsoft.Json.Linq;
using WebServiceLib.Common;

namespace WebServiceLib
{
    public class SimpDbServer : ISimpDbServer
    {
        private const string ErrorColumnName = "ERROR";

        private static JavaScriptSerializer CreateJsonSerial()
        {
            return new JavaScriptSerializer { MaxJsonLength = 60000000 };
        }

        public string DataRequest_By_String(string methodRequests)
        {
            var metodReq = CreateJsonSerial().Deserialize<MethodRequest>(methodRequests);
            var sh = new ShareSqlManager();
            return sh.ExecStoredProc(metodReq.ProceName, metodReq.ParamKeys, metodReq.ParamVals, metodReq.ProceDb, RetType.String).ToString();
        }
        public DataTable DataRequest_By_DataTable(string methodRequests)
        {
            var metodReq = CreateJsonSerial().Deserialize<MethodRequest>(methodRequests);
            var sh = new ShareSqlManager();
            var dt = (DataTable)sh.ExecStoredProc(metodReq.ProceName, metodReq.ParamKeys, metodReq.ParamVals, metodReq.ProceDb, RetType.Table);
            dt.TableName = metodReq.ProceName;
            return dt;
        }

        public DataSet DataRequest_By_DataSet(string methodRequests)
        {
            var metodReq = CreateJsonSerial().Deserialize<MethodRequest>(methodRequests);
            var sh = new ShareSqlManager();
            var ds = (DataSet)sh.ExecStoredProc(metodReq.ProceName, metodReq.ParamKeys, metodReq.ParamVals, metodReq.ProceDb, RetType.DataSet);
            for (int i = 0, iCnt = ds.Tables.Count; i < iCnt; i++)
            {
                ds.Tables[i].TableName = string.Format("{0}_{1}", metodReq.ProceName, i);
            }
            return ds;
        }

        public SimpDataEnterys DataRequest_By_SimpDataEnterys(string methodRequests)
        {
            var metodReq = CreateJsonSerial().Deserialize<MethodRequest>(methodRequests);
            var sh = new ShareSqlManager();
            var simpEtys = new List<SimpDataEnterys>();
            var lis = (List<SimpDataEntery>)sh.ExecStoredProc(metodReq.ProceName, metodReq.ParamKeys, metodReq.ParamVals, metodReq.ProceDb, RetType.SimpDEs);
            var sim = new SimpDataEnterys
            {
                ListSimpDEs = lis
            };
            return sim; 
        }

        public string DataRequest_By_SimpDEs(string methodRequests)
        {
            var metodReqs = CreateJsonSerial().Deserialize<MethodRequest[]>(methodRequests);
            var sh = new ShareSqlManager();
            var simpEtys = new List<List<SimpDataEntery>>();
            for (int i = 0, iCnt = metodReqs.Length; i < iCnt; i++)
            {
                var lis = (List<SimpDataEntery>)sh.ExecStoredProc(metodReqs[i].ProceName, metodReqs[i].ParamKeys, metodReqs[i].ParamVals, metodReqs[i].ProceDb, RetType.SimpDEs);
                if (string.IsNullOrWhiteSpace(metodReqs[i].ProceName))
                {
                    throw (new Exception("调用名称为空."));
                }
                simpEtys.Add(lis);
            }
            return CreateJsonSerial().Serialize(simpEtys);
        }

        public byte[] DataRequest_By_SimpDEs_GZip(string methodRequests)
        {
            var jsonSimpEtys = DataRequest_By_SimpDEs(methodRequests);
            var bts = System.Text.Encoding.UTF8.GetBytes(jsonSimpEtys);
            return GZipStreamHelper.GZipCompress(bts);
        }

        public byte[] DataRequest_By_SimpDEs_All_GZip(byte[] methodBts)
        {
            if (methodBts==null)
            {
                return null;
            } 
            var metBts = GZipStreamHelper.GZipDecompress(methodBts);
            var jsonSimpEtys = DataRequest_By_SimpDEs(Encoding.UTF8.GetString(metBts));
            var bts = Encoding.UTF8.GetBytes(jsonSimpEtys);
            return GZipStreamHelper.GZipCompress(bts);
        }
    }
}
