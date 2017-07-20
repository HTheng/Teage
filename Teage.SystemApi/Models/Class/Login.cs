using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Teage.SystemApi
{
    public class Login
    {
        /// <summary>
        /// 登录名字
        /// </summary>
        public string LoginName { get; set; }

        /// <summary>
        /// 登录密码
        /// </summary>
        public string Password { get; set; }
        /// <summary>
        /// 手机号码
        /// </summary>
        public string Phone { get; set; }

        /// <summary>
        /// 验证码
        /// </summary>
        public string Captcha { get; set; }
    }
}