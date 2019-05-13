using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Common
{
    public class HttpHepler
    {
        //private static string appkey = System.Configuration.ConfigurationManager.AppSettings["zh_appkey"];
        //private static string appsecret = System.Configuration.ConfigurationManager.AppSettings["zh_appsecret"];

       
        private static string url = System.Configuration.ConfigurationManager.AppSettings["zh_url"];

        public static string UrlResponseByPost(string method, SortedDictionary<string, string> dic,string appkey,string appsecret)
        {
            dic.Add("appkey", appkey);
            dic.Add("method", method);

            string connectStr = "";
            string para = "";
            foreach (string key in dic.Keys)
            {
                connectStr += key + dic[key];
                if (key != "appkey" && key != "method")
                    para += key + "=" + dic[key] + "&";
            }
            connectStr = appsecret + connectStr + appsecret;
            if (para != "")
                para = para.TrimEnd('&');

            string sign = CommonHelper.CalcMD5(connectStr).ToUpper();
            string ur = url + "?appkey=" + appkey + "&method=" + method + "&sign=" + sign;
            try
            {
                byte[] dataArray = Encoding.GetEncoding("GBK").GetBytes(para);
                //创建请求
                HttpWebRequest request = (HttpWebRequest)HttpWebRequest.Create(ur);
                request.Method = "POST";
                request.Timeout = 60000;
                request.ContentLength = dataArray.Length;
                request.ContentType = "application/x-www-form-urlencoded;charset=GBK";

                //创建输入流
                Stream dataStream = request.GetRequestStream();

                //发送请求
                dataStream.Write(dataArray, 0, dataArray.Length);
                dataStream.Close();

                //获取返回内容
                WebResponse response = request.GetResponse();
                Stream responseStream = response.GetResponseStream();

                StreamReader responseReader = new StreamReader(responseStream, Encoding.GetEncoding("GBK"));
                string content = responseReader.ReadToEnd();
                responseReader.Close();
                responseStream.Close();
                return content;
            }
            catch (Exception Ex)
            {
                return Ex.Message;
            }
        }
    
       }
}
