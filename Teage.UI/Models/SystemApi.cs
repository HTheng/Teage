using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace Teage.UI.Models
{
    public static class SystemApi
    {
          /// <summary>
        /// api接口
        /// </summary>
        public static string Api = ConfigurationManager.AppSettings["SystemApi"];
    }
}