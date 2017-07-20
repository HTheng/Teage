using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Teage.Entity
{
    public class PageModel<T> where T : class,new()
    {
        /// <summary>
        /// 模型集合
        /// </summary>
        public List<T> Models { get; set; }

        /// <summary>
        /// 符合条件的总数
        /// </summary>
        public int Total { get; set; }
    }
}
