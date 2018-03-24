using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Windows.Forms;
using Newtonsoft.Json.Linq;
using WebServiceLib.Common;

namespace WebApiClient
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                var url = "http://localhost:7131/api/SimpDbServer/DataRequest_By_DataTable";
                HttpWebRequest request = (HttpWebRequest)HttpWebRequest.Create(url);    //创建一个请求示例

                request.ContentType = "application/json";
                request.Method = "POST";
                JObject jo = new JObject();
                jo["ProceDb"] = "FengShenDB";
                jo["ProceName"] = "ProcedureTest";
                jo["ParamKeys"] = new JArray();
                jo["ParamVals"] = new JArray();

                byte[] data = Encoding.UTF8.GetBytes(jo.ToString());
                request.ContentLength = data.Length;
                Stream myRequestStream = request.GetRequestStream();
                myRequestStream.Write(data, 0, data.Length);
                myRequestStream.Close();

                HttpWebResponse response = (HttpWebResponse)request.GetResponse();　　//获取响应，即发送请求
                Stream responseStream = response.GetResponseStream();
                StreamReader streamReader = new StreamReader(responseStream, Encoding.UTF8);
                string result = streamReader.ReadToEnd();
                //this.dataGridView1.DataSource = StringToDataTable(result);
                this.dataGridView1.DataSource = JsonHelper.JsonToDataSet(result).Tables[0];
            }
            catch (Exception)
            {
                
               // throw;
            }
            
        }
        /// <summary>
        /// string 到 DataTable
        /// </summary>
        /// <param name="strdata"></param>
        /// <returns></returns>
        public static DataTable StringToDataTable(string strdata)
        {
            if (string.IsNullOrEmpty(strdata))
            {
                return null;
            }
            DataTable dt = new DataTable();
            string[] strSplit = { "@&@" };
            string[] strRow = { "#$%" };    //分解行的字符串
            string[] strColumn = { "^&*" }; //分解字段的字符串

            string[] strArr = strdata.Split(strSplit, StringSplitOptions.None);
            StringReader sr = new StringReader(strArr[0]);
            dt.ReadXmlSchema(sr);
            sr.Close();


            string strTable = strArr[1]; //取表的数据
            if (!string.IsNullOrEmpty(strTable))
            {
                string[] strRows = strTable.Split(strRow, StringSplitOptions.None); //解析成行的字符串数组
                for (int rowIndex = 0; rowIndex < strRows.Length; rowIndex++)       //行的字符串数组遍历
                {
                    string vsRow = strRows[rowIndex]; //取行的字符串
                    string[] vsColumns = vsRow.Split(strColumn, StringSplitOptions.None); //解析成字段数组
                    dt.Rows.Add(vsColumns);
                }
            }
            return dt;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                var url = "http://localhost:7131/api/SimpDbServer/DataRequest_By_SimpDataEnterys";
                HttpWebRequest request = (HttpWebRequest)HttpWebRequest.Create(url);    //创建一个请求示例

                request.ContentType = "application/json";
                request.Method = "POST";
                JObject jo = new JObject();
                jo["ProceDb"] = "FengShenDB";
                jo["ProceName"] = "ProcedureTest";
                jo["ParamKeys"] = new JArray();
                jo["ParamVals"] = new JArray();

                byte[] data = Encoding.UTF8.GetBytes(jo.ToString());
                request.ContentLength = data.Length;
                Stream myRequestStream = request.GetRequestStream();
                myRequestStream.Write(data, 0, data.Length);
                myRequestStream.Close();

                HttpWebResponse response = (HttpWebResponse)request.GetResponse();　　//获取响应，即发送请求
                Stream responseStream = response.GetResponseStream();
                StreamReader streamReader = new StreamReader(responseStream, Encoding.UTF8);
                string result = streamReader.ReadToEnd();
                var si = JObject.Parse(result);
                var sim = si["ListSimpDEs"].ToObject<SimpDataEntery>();
                this.textBox1.Text = result;
                this.dataGridView1.DataSource = sim.Rows;
                //this.dataGridView1.DataSource = JsonHelper.JsonToDataSet(result).Tables[0];
            }
            catch (Exception)
            {

                // throw;
            }
        }    
    }
}
