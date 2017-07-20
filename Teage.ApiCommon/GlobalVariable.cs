using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Teage.ApiCommon
{
    /// <summary>
    /// Session Key
    /// HttpContext.Item Key
    /// </summary>
    public static class GlobalVariable
    {
        /// <summary>
        /// 验证码Key
        /// </summary>
        public const string CAPTCHA = "CAPTCHA";

        /// <summary>
        /// 用户Id Key
        /// </summary>
        [Obsolete]
        public const string USERID = "USERID";

        public const string USER = "SYSTEMAPI_USER";
    }
}
