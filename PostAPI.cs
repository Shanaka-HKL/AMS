using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.Script.Services;
using System.Net;
using System.Text;
using System.IO;
using System.Text.RegularExpressions;
using System.Data;
using Newtonsoft.Json;
using System.Configuration;

namespace AMS
{
    public class PostAPI
    {
        String ApiKey = ConfigurationManager.AppSettings["Kripton_Key"].ToString().Trim(); String ApiPoint = ConfigurationManager.AppSettings["Kripta_Link"].ToString().Trim();

        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public DataTable get_datatable(string method, string JsonOutput, string meth_)
        {
            string result = "";
            DataTable dt = new DataTable();
            try
            {
                Uri lnk = new Uri(ApiPoint + "api/AMS/" + method);
                var http = (HttpWebRequest)WebRequest.Create(lnk);
                http.KeepAlive = true;
                http.PreAuthenticate = true;
                http.AllowAutoRedirect = true;
                ASCIIEncoding encoding = new ASCIIEncoding();
                byte[] bytes = encoding.GetBytes(JsonOutput);
                http.ContentLength = bytes.Length;
                http.Headers["AMS_KEY"] = ApiKey;
                http.Accept = "application/json";
                http.ContentType = "application/json";
                http.MediaType = "application/json";
                http.Headers["Cache-Control"] = "no-cache";
                http.Method = meth_.ToUpper();

                Stream newStream = http.GetRequestStream();
                newStream.Write(bytes, 0, bytes.Length);

                using ((HttpWebResponse)http.GetResponse())
                {
                    StreamReader sr = new StreamReader(http.GetResponse().GetResponseStream());
                    result = sr.ReadToEnd().Trim();

                    dt = (DataTable)JsonConvert.DeserializeObject(result, (typeof(DataTable)));
                }
                return dt;
            }
            catch //(WebException e)
            {
                //using (WebResponse response = e.Response)
                //{
                //    HttpWebResponse httpResponse = (HttpWebResponse)response;
                //    Console.WriteLine("Error code: {0}", httpResponse.StatusCode);
                //    using (Stream data = response.GetResponseStream())
                //    using (var reader = new StreamReader(data))
                //    {
                //        string text = reader.ReadToEnd();
                //    }
                //}
                return dt;
            }
        }

        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public string get_string(string method, string JsonOutput, string meth_)
        {
            string result = "";
            try
            {
                Uri lnk = new Uri(ApiPoint + "api/AMS/" + method);
                var http = (HttpWebRequest)WebRequest.Create(lnk);
                http.KeepAlive = true;
                http.PreAuthenticate = true;
                http.AllowAutoRedirect = true;
                ASCIIEncoding encoding = new ASCIIEncoding();
                byte[] bytes = encoding.GetBytes(JsonOutput);
                http.ContentLength = bytes.Length;
                http.Headers["AMS_KEY"] = ApiKey;
                http.Accept = "application/json";
                http.ContentType = "application/json";
                http.MediaType = "application/json";
                http.Headers["Cache-Control"] = "no-cache";
                http.Method = meth_.ToUpper();

                Stream newStream = http.GetRequestStream();
                newStream.Write(bytes, 0, bytes.Length);

                using ((HttpWebResponse)http.GetResponse())
                {
                    StreamReader sr = new StreamReader(http.GetResponse().GetResponseStream());
                    result = sr.ReadToEnd().Trim();
                    string[] resultCollection = Regex.Split(result, ",");
                }
                return result;
            }
            catch (WebException e)
            {
                return e.Message;
                //using (WebResponse response = e.Response)
                //{
                //    HttpWebResponse httpResponse = (HttpWebResponse)response;
                //    Console.WriteLine("Error code: {0}", httpResponse.StatusCode);
                //    using (Stream data = response.GetResponseStream())
                //    using (var reader = new StreamReader(data))
                //    {
                //        string text = reader.ReadToEnd();
                //    }
                //}
                //return result;
            }
        }

        public string gAPIRequest(string result_)
        {
            try
            {
                Uri lnk = new Uri("https://www.google.com/recaptcha/api/siteverify?secret=" + "6LfnczUeAAAAAFem7LGtdKTfNoc6YIpDJt7VzZnB" + "&&response=" + result_);
                var http = (HttpWebRequest)WebRequest.Create(lnk);
                http.KeepAlive = true;
                http.PreAuthenticate = true;
                http.AllowAutoRedirect = true;
                http.Accept = "application/json";
                http.ContentType = "application/json";
                http.MediaType = "application/json";
                http.Method = "POST";

                ASCIIEncoding encoding = new ASCIIEncoding();
                byte[] bytes = encoding.GetBytes(lnk.AbsoluteUri);
                http.ContentLength = bytes.Length;
                Stream newStream = http.GetRequestStream();
                newStream.Write(bytes, 0, bytes.Length);

                string result = "";
                using ((HttpWebResponse)http.GetResponse())
                {
                    StreamReader sr = new StreamReader(http.GetResponse().GetResponseStream());
                    result = sr.ReadToEnd().Trim();
                    string[] resultCollection = Regex.Split(result, ",");

                    if (result.ToString().ToUpper().Contains("true"))
                    {
                        return result;
                    }
                    else
                    {
                        return result;
                    }
                }
            }
            catch
            {
                return null;
            }
        }
    }
}