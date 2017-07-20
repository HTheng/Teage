using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Teage.Entity
{
    //用户信息
    public class UsersQuery : QueryBase
    {
        /// <summary>
        /// UserID
        /// </summary>
        public string UserID { get; set; }
        /// <summary>
        /// 用户名称
        /// </summary>
        public string LoginName { get; set; }
        /// <summary>
        /// 密钥
        /// </summary>
        public string Token { get; set; }
        /// <summary>
        /// 用户密码
        /// </summary>
        public string LoginPwd { get; set; }
        /// <summary>
        /// 手机号
        /// </summary>
        public string Phone { get; set; }
        /// <summary>
        /// 用户头像
        /// </summary>
        public string Icon { get; set; }
        /// <summary>
        /// 用户地址
        /// </summary>
        public int AddressId { get; set; }
        /// <summary>
        /// 用户邮箱
        /// </summary>
        public string Mail { get; set; }
        /// <summary>
        /// 账户余额
        /// </summary>
        public decimal vMoney { get; set; }
        /// <summary>
        /// 注册时间
        /// </summary>
        public DateTime RegTime { get; set; }
        /// <summary>
        /// 用户状态
        /// </summary>
        public int UserStateId { get; set; }
        /// <summary>
        /// 邀请人编号
        /// </summary>
        public string ParentID { get; set; }
    }
}
