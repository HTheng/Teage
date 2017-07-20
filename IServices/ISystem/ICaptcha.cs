using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Teage.ApiCommon;
using Teage.Model;

namespace IServices
{
    public interface ICaptcha
    {
        /// <summary>
        /// 新增验证码
        /// </summary>
        /// <param name="model"></param>
        bool Add(Captcha model, ref Meta meta);
        /// <summary>
        /// 查询验证码
        /// </summary>
        /// <param name="model"></param>
        Captcha First(string key);
    }
}
