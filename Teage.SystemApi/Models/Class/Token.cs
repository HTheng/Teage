using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Teage.SystemApi
{
    public class Token 
    {
        /// <summary>
        /// Token 的Key
        /// </summary>
        public string Key { get; set; }

        /// <summary>
        /// Token 代表的用户ID
        /// </summary>
        public string UserId { get; set; }
    }
}