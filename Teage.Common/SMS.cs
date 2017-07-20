using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;
using System.Web;
namespace Teage.Common
{
    public class SMS
    {
        private static SMS _instance = new SMS();
        public static SMS _
        {
            get
            {
                return _instance;
            }
            set
            {
                _instance = value;
            }
        }

        public void SendSMS(string mobile, string content)
        {
            if (string.IsNullOrEmpty(content.Trim()))
            {
                return;
            }
            string message = HttpUtility.UrlEncode(content, System.Text.Encoding.GetEncoding("gbk"));

            string Url = ConfigurationManager.AppSettings["sms_url"];
            string UserId = ConfigurationManager.AppSettings["user_id"];
            string UserPwd = ConfigurationManager.AppSettings["user_pwd"];
            string Mobile = Base64._.Encode(mobile);
            string Key = MD5(UserPwd + "&" + mobile);
            string SendUrl = Url + "user_id=" + UserId + "&user_pwd=" + Key + "&mobile=" + Mobile + "&msg_content=" + message + "&ext=8888";
            WebRequest myRequest = WebRequest.Create(SendUrl);
            myRequest.Timeout = 10000;
            myRequest.Credentials = CredentialCache.DefaultCredentials;
            WebResponse myResponse = myRequest.GetResponse();
            StreamReader myStreamReader = new StreamReader(myResponse.GetResponseStream());
            String msg = myStreamReader.ReadToEnd();
            myStreamReader.Close();
            myResponse.Close();
        }

        private string MD5(string input)
        {
            MD5 myMD5 = new MD5CryptoServiceProvider();
            byte[] signed = myMD5.ComputeHash(Encoding.UTF8.GetBytes(input));
            string signResult = byte2mac(signed);
            return signResult.ToUpper();
        }

        private static string byte2mac(byte[] signed)
        {
            StringBuilder EnText = new StringBuilder();
            foreach (byte Byte in signed)
            {
                EnText.AppendFormat("{0:x2}", Byte);
            }

            return EnText.ToString();
        }
    }
}
