using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Teage.Entity
{
    public enum CaptchaType
    {
        /// <summary>
        /// 注册
        /// </summary>
        Register = 1,
        /// <summary>
        /// 找回密码
        /// </summary>
        ResetPwd = 2,
        /// <summary>
        /// 登录
        /// </summary>
        Login = 3
    }
}
