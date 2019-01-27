using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Qiniu.Http;
using Qiniu.IO;
using Qiniu.IO.Model;
using Qiniu.Util;
  
namespace QiniuTest
{
    class Program
    {
        static void Main(string[] args)
        {
            string AK = "aYDym1DTzZnCVH6laD7LTV6OyHah2ik3wdCkCy3w";
            string SK = "rYP-A_bXKtP65_Hx-g7_edM8-_0xF_z0275dG20L";
            Mac mac = new Mac(AK, SK);
            Auth auth = new Auth(mac);
            string bucket = "mytest";
            string saveKey = "1.png";
            string localFile = "D:\\1.jpg";
            // 上传策略，参见 
            // https://developer.qiniu.com/kodo/manual/put-policy
            PutPolicy putPolicy = new PutPolicy();
            // 如果需要设置为"覆盖"上传(如果云端已有同名文件则覆盖)，请使用 SCOPE = "BUCKET:KEY"
            // putPolicy.Scope = bucket + ":" + saveKey;
            putPolicy.Scope = bucket;
            // 上传策略有效期(对应于生成的凭证的有效期)          
            putPolicy.SetExpires(3600);
            // 上传到云端多少天后自动删除该文件，如果不设置（即保持默认默认）则不删除
            putPolicy.DeleteAfterDays = 1;
            // 生成上传凭证，参见
            // https://developer.qiniu.com/kodo/manual/upload-token            
            string jstr = putPolicy.ToJsonString();
            string token = Auth.CreateUploadToken(mac, jstr);
            UploadManager um = new UploadManager();
            HttpResult result = um.UploadFile(localFile, saveKey, token);
            Console.WriteLine(result);
            Console.ReadKey();
        }
    }
}
