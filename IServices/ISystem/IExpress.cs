using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Teage.ApiCommon;
using Teage.Entity;
using Teage.Model;

namespace IServices
{
    public partial interface IExpress
    {
        /// <summary>
        /// 新增优递
        /// </summary>
        /// <param name="model"></param>
        bool Add(Express model, ref Meta meta);
        /// <summary>
        /// 编辑优递（用户开始做任务）
        /// </summary>
        /// <param name="model"></param>
        bool Edit(ExpressInfo model, ref Meta meta);
        /// <summary>
        /// 修改优递的状态
        /// </summary>
        /// <param name="model"></param>
        bool Update(Express model, string flag, ref Meta meta);
        /// <summary>
        /// 优递列表
        /// </summary>
        /// <param name="model"></param>
        PageModel<Express> List(ExpressQuery queryInfo, ref Meta meta);


        /// <summary>
        /// 编辑优递
        /// </summary>
        /// <param name="model"></param>
        bool Modify(ExpressInfo model, ref Meta meta);
        /// <summary>
        /// 优递列表
        /// </summary>
        /// <param name="model"></param>
        PageModel<Express> HostList(ExpressQuery queryInfo, ref Meta meta);
        /// 优递列表
        /// </summary>
        /// <param name="model"></param>
        bool Delete(ExpressInfo model, ref Meta meta);

    }
}
