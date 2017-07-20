using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Teage.Common
{
    public class Base64
    {
        private static Base64 _instance = new Base64();
        public static Base64 _
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

        public string Encode(string str)
        {
            byte[] encbuff = System.Text.Encoding.UTF8.GetBytes(str);
            return Convert.ToBase64String(encbuff);
        }

        public string Decode(string str)
        {
            byte[] decbuff = Convert.FromBase64String(str);
            return System.Text.Encoding.UTF8.GetString(decbuff);
        }
    }
}
