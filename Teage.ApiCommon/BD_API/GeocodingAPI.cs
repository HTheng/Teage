using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Teage.ApiCommon.BD_API
{
    public class GeocodingAPI
    {
        /// <summary>
        /// 逆地理编码
        /// </summary>
        /// <param name="lng">经度</param>
        /// <param name="lat">纬度</param>
        /// <returns></returns>
        public static ReGeocodingResult ReGeocoding(string lng, string lat)
        {
            string myUrl = "http://api.map.baidu.com/geocoder/v2/?ak=773523b92550e15edc4ab84d62f994a7&location={lat},{lng}&output=json";
            WebRequest myWebRequest = WebRequest.Create(myUrl);
            myWebRequest.Timeout = 60000;
            myWebRequest.Credentials = CredentialCache.DefaultCredentials;

            ReGeocodingResult myResult = null;
            using (WebResponse myWebResponse = myWebRequest.GetResponse())
            {
                StreamReader mySteamReader = new StreamReader(myWebResponse.GetResponseStream());

                try
                {
                    string myJson = mySteamReader.ReadToEnd();
                    myResult = Newtonsoft.Json.JsonConvert.DeserializeObject<ReGeocodingResult>(myJson);
                }
                finally { }
                mySteamReader.Close();
            }

            return myResult;
        }

        /// <summary>
        /// 调用百度Api,通过经纬度获取省市县
        /// </summary>
        /// <param name="lon"></param>
        /// <param name="lat"></param>
        /// <returns></returns>
        public static BaiduGisModel GetByLonlat(float lon, float lat)
        {
            const string urltemp = "http://api.map.baidu.com/geocoder/v2/?ak=773523b92550e15edc4ab84d62f994a7&location={0},{1}&output=json";
            string url = string.Format(urltemp, lat, lon);
            var request = (HttpWebRequest)WebRequest.Create(url);
            WebResponse response = request.GetResponse();
            Stream stream = response.GetResponseStream();
            if (stream != null)
            {
                var reader = new StreamReader(stream);
                var text = reader.ReadToEnd();
                reader.Dispose();
                stream.Dispose();
                response.Close();

                return Newtonsoft.Json.JsonConvert.DeserializeObject<BaiduGisModel>(text);
            }
            return null;
        }


        /// <summary>
        /// 调用百度Api通过地址获取经纬度
        /// </summary>
        /// <param name="address"></param>
        /// <returns></returns>
        public static LonLatModel GetLonLatByAddress(string address)
        {


            const string urltemp = "http://api.map.baidu.com/geocoder/v2/?ak=773523b92550e15edc4ab84d62f994a7&address={0}&output=json";
            string url = string.Format(urltemp, address);
            var request = (HttpWebRequest)WebRequest.Create(url);
            WebResponse response = request.GetResponse();
            Stream stream = response.GetResponseStream();
            if (stream != null)
            {
                var reader = new StreamReader(stream);
                var text = reader.ReadToEnd();
                reader.Dispose();
                stream.Dispose();
                response.Close();
                return Newtonsoft.Json.JsonConvert.DeserializeObject<LonLatModel>(text);
            }
            return null;
        }
        #region 调用百度Api返回对象模型

        public class BaiduGisModel
        {
            /// <summary>
            /// 返回状态
            /// </summary>
            public int status { get; set; }
            public ApiModelResult result { get; set; }
            //{"status":0,"result":{"location":{"lng":119.99999995764,"lat":30.554999878338},"formatted_address":"浙江省湖州市德清县丰庆街558号","business":"",
            //"addressComponent":{"city":"湖州市","country":"中国","direction":"附近","distance":"35","district":"德清县","province":"浙江省","street":"丰庆街",
            //  "street_number":"558号","country_code":0},"poiRegions":[],"sematic_description":"德清县体育中心(民乐小区北)西北805米","cityCode":294}}}}
        }

        public class ApiModelResult
        {
            /// <summary>
            /// 地理位置（经纬度）
            /// </summary>
            public Location location { get; set; }
            /// <summary>
            /// 详细地址
            /// </summary>
            public string formatted_address { get; set; }
            public string business { get; set; }
            public AddressComponent addressComponent { get; set; }
            /// <summary>
            /// 详细指示位置
            /// </summary>
            public string sematic_description { get; set; }
        }

        public class Location
        {
            /// <summary>
            /// 经度
            /// </summary>
            public float lng { get; set; }
            /// <summary>
            /// 纬度
            /// </summary>
            public float lat { get; set; }
        }

        public class AddressComponent
        {
            /// <summary>
            /// 市
            /// </summary>
            public string city { get; set; }
            /// <summary>
            /// 国家
            /// </summary>
            public string country { get; set; }
            /// <summary>
            /// 距离
            /// </summary>
            public string distance { get; set; }
            /// <summary>
            /// 省
            /// </summary>
            public string province { get; set; }
            /// <summary>
            /// 街道
            /// </summary>
            public string street { get; set; }
            /// <summary>
            /// 县
            /// </summary>
            public string district { get; set; }
            /// <summary>
            /// 街道号码
            /// </summary>
            public string street_number { get; set; }
        }

        public class LonLatModel
        {
            //{
            //    "status":"OK",
            //    "result":{
            //        "location":{
            //            "lng":116.38738,
            //            "lat":39.881456
            //        },
            //        "precise":0,
            //        "confidence":50,
            //        "level":""
            //    }
            //}
            /// <summary>
            /// 返回结果状态值， 成功返回0，其他值请查看下方返回码状态表。
            /// </summary>
            public string status { get; set; }
            public LonLatModelResult result { get; set; }
        }

        public class LonLatModelResult
        {
            public Location location { get; set; }
            /// <summary>
            /// 位置的附加信息，是否精确查找。1为精确查找，即准确打点；0为不精确，即模糊打点。
            /// </summary>
            public int precise { get; set; }
            /// <summary>
            /// 可信度，描述打点准确度
            /// </summary>
            public int confidencd { get; set; }
            /// <summary>
            /// 地址类型
            /// </summary>
            public string level { get; set; }
        }

        #endregion
    }
}
