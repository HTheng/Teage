using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Teage.Entity;

namespace Teage.Entity
{
    //用户地址
    public class AddressQuery : QueryBase
    {
        /// <summary>
        /// UserID
        /// </summary>
        public string UserID { get; set; }
        /// <summary>
        /// 手机号
        /// </summary>
        public string Phone { get; set; }
        /// <summary>
        /// 用户地址
        /// </summary>
        public string Address { get; set; }
        /// <summary>
        /// 收货人名称
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 地址状态
        /// </summary>
        public int State { get; set; }
        /// <summary>
        /// 密钥
        /// </summary>
        public string Token { get; set; }

    }
}
