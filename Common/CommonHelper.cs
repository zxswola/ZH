using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Common
{
   public class CommonHelper
    {
        public static string CalcMD5(string str)
        {
            byte[] bytes = System.Text.Encoding.UTF8.GetBytes(str);
            return CalcMD5(bytes);
        }

        public static string CalcMD5(byte[] bytes)
        {
            using (MD5 md5 = MD5.Create())
            {
                byte[] computeBytes = md5.ComputeHash(bytes);
                string result = "";
                for (int i = 0; i < computeBytes.Length; i++)
                {
                    result += computeBytes[i].ToString("X").Length == 1 ? "0" + computeBytes[i].ToString("X") : computeBytes[i].ToString("X");
                }
                return result;
            }
        }

        public static string CalcMD5(Stream stream)
        {
            using (MD5 md5 = MD5.Create())
            {
                byte[] computeBytes = md5.ComputeHash(stream);
                string result = "";
                for (int i = 0; i < computeBytes.Length; i++)
                {
                    result += computeBytes[i].ToString("X").Length == 1 ? "0" + computeBytes[i].ToString("X") : computeBytes[i].ToString("X");
                }
                return result;
            }
        }

        //Chapcha
        public static string CreateVerifyCode(int len)
        {
            char[] data = { 'a','c','d','e','f','h','k','m',
                'n','r','s','t','w','x','y','3','4','5','6','8'};
            StringBuilder sb = new StringBuilder();
            Random rand = new Random();
            for (int i = 0; i < len; i++)
            {
                int index = rand.Next(data.Length);//[0,data.length)
                char ch = data[index];
                sb.Append(ch);
            }
            //勤测！
            return sb.ToString();
        }

        ///// <summary>
        ///// MD5加密。  
        ///// </summary>
        ///// <param name="text"></param>
        ///// <returns></returns>
        //public static string Encrypt(string text)
        //{
        //    byte[] b = System.Text.Encoding.UTF8.GetBytes(text);
        //    b = new System.Security.Cryptography.MD5CryptoServiceProvider().ComputeHash(b);
        //    var sb = new StringBuilder();
        //    for (int i = 0; i < b.Length; i++)
        //        sb.Append(b[i].ToString("x").PadLeft(2, '0'));

        //    return sb.ToString();
        //}

        /// <summary>
        /// 获取文件内容
        /// </summary>
        /// <param name="Path"></param>
        /// <returns></returns>
        public static string GetFile(string Path)
        {
            try
            {
                IsExist_File(Path);
                string Json = string.Empty; ;
                FileStream fs = new FileStream(Path, FileMode.Open, FileAccess.Read, FileShare.Read);
                using (StreamReader sr = new StreamReader(fs, Encoding.Default))
                {
                    Json = sr.ReadToEnd();
                    sr.Close();
                    fs.Close();
                    fs.Dispose();
                }
                return Json;
            }
            catch (Exception e)
            {
                return "";
            }
        }
        /// <summary>
        /// 写入文件
        /// </summary>
        /// <param name="Path"></param>
        /// <param name="Json"></param>
        /// <returns></returns>
        public static bool SetFile(string Path, string Text)
        {
            try
            {
                IsExist_File(Path);
                //清空
                System.IO.File.WriteAllText(Path, "");
                FileStream fs = new FileStream(Path, FileMode.Open, FileAccess.ReadWrite, FileShare.Write);
                using (StreamReader sr = new StreamReader(fs, Encoding.UTF8))
                {

                    StreamWriter sw = new StreamWriter(fs);
                    //开始写入
                    sw.Write(Text);
                    //清空缓冲区 
                    sw.Flush();
                    //关闭流 
                    sw.Close();
                    fs.Close();
                }
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        /// <summary>
        /// 没有就创建(文件)
        /// </summary>
        /// <param name="Path"></param>
        public static void IsExist_File(string Path)
        {
            if (!File.Exists(Path))
            {
                File.Create(Path).Close();
            }
        }

        /// <summary>
        /// 返回本对象的Json序列化
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static string ToJson(object obj)
        {
            if (null == obj)
                return string.Empty;

            String str;
            if (obj is String || obj is Char)
            {
                str = obj.ToString();
            }
            else
            {
                str = JsonConvert.SerializeObject(obj, new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore });
            }
            return str;
        }

        /**/
        /// <summary>
        /// 将本JSON字符串反序列化为对象
        /// </summary>
        /// <typeparam name="T">返序列化类型</typeparam>
        /// <param name="json">Json字符串</param>
        /// <returns></returns>
        public static T DeJson<T>(string json)
        {
            return (T)JsonConvert.DeserializeObject<T>(json);
        }

        /// <summary>
        /// 贝贝店转换MD5获取Sign
        /// </summary>
        /// <param name="dict"></param>
        /// <param name="secret"></param>
        /// <returns></returns>
        public static string BbOpera(Dictionary<string, string> dict, string secret)
        {
            string str = string.Empty;
            if (dict.Count > 0)
            {
                var dicAsc = dict.OrderBy(d => d.Key);
                str += secret;
                foreach (var dic in dicAsc)
                {
                    str += dic.Key + dic.Value;
                }
                str += secret;
            }
            return CalcMD5(str);
        }
    }
}
