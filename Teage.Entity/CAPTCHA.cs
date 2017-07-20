using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Teage.Entity
{
    public class CAPTCHA 
    {
        /// <summary>
        /// 验证码的key
        /// </summary>
        public string Key { get; set; }
        /// <summary>
        /// 验证码的Value
        /// </summary>
        public object Value { get; set; }
        /// <summary>
        /// 过期时间点
        /// </summary>
        public DateTimeOffset DateTime { get; set; }
    }
}
