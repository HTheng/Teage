using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Teage.ApiCommon.BD_API
{
     /// <summary>
    /// IP定位API
    /// </summary>
    public static class LocationIp
    {
        public static int GetCityCode(string ip)
        {
            string myUrl = "http://api.map.baidu.com/location/ip?ak=773523b92550e15edc4ab84d62f994a7&ip={ip}";

            WebRequest myWebRequest = WebRequest.Create(myUrl);
            myWebRequest.Timeout = 60000;
            myWebRequest.Credentials = CredentialCache.DefaultCredentials;

            int intCode =0;
            using (WebResponse myWebResponse = myWebRequest.GetResponse())
            {
                StreamReader mySteamReader = new StreamReader(myWebResponse.GetResponseStream());

                try
                {
                    string myJson = mySteamReader.ReadToEnd();
                    //myResult = Newtonsoft.Json.JsonConvert.DeserializeObject<ReGeocodingResult>(myJson);
                    LocationIpResult result = Newtonsoft.Json.JsonConvert.DeserializeObject<LocationIpResult>(myJson);
                    if (result.status == 0)
                    {
                        intCode = result.content.address_detail.city_code;
                    }
                }
                finally
                {
                    mySteamReader.Close();
                }
                
            }

            return intCode;
        }
    }
}
