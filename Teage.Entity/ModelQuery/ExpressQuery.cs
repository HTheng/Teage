using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Teage.Entity
{
    public class ExpressQuery : QueryBase
    {
        /// <summary>
        /// 快递名称
        /// </summary>
        public string ExpressName { get; set; }
        /// <summary>
        /// 快递编号
        /// </summary>
        public string ID { get; set; }
        /// <summary>
        /// 密钥
        /// </summary>
        public string Token { get; set; }
        /// <summary>
        /// 显示列表（1,所有数据，2.自己发起的，3，自己拿到的）
        /// </summary>
        public string flag { get; set; }
        /// <summary>
        /// 用户编号
        /// </summary>
        public string UserID { get; set; }
    }
}
