using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Teage.Common
{
    public class SerializableHelper
    {
        //object转为字符串
        public static string SerializableToString(object value)
        {
            return JsonConvert.SerializeObject(value);
        }
        //字符串转为object
        public static T DeserializeToObject<T>(string str)
        {
            return JsonConvert.DeserializeObject<T>(str);
        }
        //object 转为List
        public static List<T> DeserializeToObjectToList<T>(string str)
        {
            return JsonConvert.DeserializeObject<List<T>>(str);
        }
    }
}
