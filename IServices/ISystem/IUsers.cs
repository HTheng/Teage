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
    public partial interface IUsers
    {
        /// <summary>
        /// 新增用户
        /// </summary>
        /// <param name="model"></param>
        bool Add(Users model, ref Meta meta);
        /// <summary>
        /// 新增用户
        /// </summary>
        /// <param name="model"></param>
        ActionResult<Users> Login(string userType, string strLoginName, string strPassword);
        /// <summary>
        /// 修改用户
        /// </summary>
        /// <param name="model"></param>
        bool Edit(UsersQuery model, ref Meta meta);
        /// <summary>
        /// 启用停用用户
        /// </summary>
        /// <param name="model"></param>
        bool Delete(UsersQuery model, ref Meta meta);
        /// <summary>
        /// 我的钱包
        /// </summary>
        /// <param name="queryInfo"></param>
        PageModel<Users> Purse(UsersQuery queryInfo, ref Meta meta);
        /// <summary>
        /// 我的常用地址
        /// </summary>
        /// <param name="queryInfo"></param>
        PageModel<UsersAddress> MyAddress(AddressQuery queryInfo, ref Meta meta);
        /// <summary>
        /// 我的邀请人
        /// </summary>
        /// <param name="queryInfo"></param>
        PageModel<Users> Inviter(UsersQuery queryInfo, ref Meta meta);
        /// <summary>
        /// 查询一条数据
        /// </summary>
        /// <param name="model"></param>
        Users First(string phone);
    }
}
