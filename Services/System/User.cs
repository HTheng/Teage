using IServices;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Teage.ApiCommon;
using Teage.Common;
using Teage.Entity;
using Teage.Model;

namespace Services
{
    public partial class User : Service, IUsers
    {
        //用户新增
        public bool Add(Users model, ref Meta meta)
        {
            string msg = EnumHelper.GetDescriptionFromEnumValue(ErrorCode.Success);
            string msgCode = ErrorCode.Success.GetHashCode().ToString();
            model.UserID = Guid.NewGuid().ToString().Replace("-", "").Trim();
            model.LoginPwd = WebCommon.Md5String(model.LoginPwd);
            model.RegTime = DateTime.Now;
            model.UserStateId = 0;
            Try(context =>
            {
                //查询用户名是否重复
                var user = context.Users.Where(c => c.LoginName == model.LoginName).FirstOrDefault();
                if (user != null)
                {
                    msgCode = ErrorCode.Error.GetHashCode().ToString();
                    msg = EnumHelper.GetDescriptionFromEnumValue(ErrorCode.Error);
                }
                else
                {
                    context.Users.Add(model);
                    context.SaveChanges();
                }
            });
            meta.ErrorCode = msgCode;
            meta.ErrorMsg = msg;
            return true;
        }

        //用户登录
        public ActionResult<Users> Login(string userType, string StrPhone, string strPassword)
        {
            ActionResult<Users> myResult = new ActionResult<Users>();

            if (string.IsNullOrWhiteSpace(StrPhone))
            {
                myResult.IsSuccess = false;
                myResult.Message = "手机号码不能为空";
                return myResult;
            }
            myResult.IsSuccess = true;
            Try(context =>
            {
                //var myLogon = context.Users.First(c => c.Phone.Equals(StrPhone));
                var myLogon = context.Users.First(c => c.Phone == StrPhone);

                if (myLogon == null)
                {
                    myResult.IsSuccess = false;
                    myResult.Message = "您的账号还未注册";
                }
                //if (!myLogon.LoginPwd.Equals(strPassword))
                //{
                //    myResult.IsSuccess = false;
                //    myResult.Message = "密码错误，请确认后重新输入";
                //}
                myResult.ResultObject = myLogon;
            });
            return myResult;
        }
        //修改用户
        public bool Edit(UsersQuery model, ref Meta meta)
        {
            string msg = EnumHelper.GetDescriptionFromEnumValue(ErrorCode.Success);
            string msgCode = ErrorCode.Success.GetHashCode().ToString();
            Try(context =>
            {
                //查询用户名是否存在
                var user = context.Users.Where(c => c.UserID == model.UserID && model.UserStateId == 0).FirstOrDefault();
                if (user != null)
                {
                    msgCode = ErrorCode.Error.GetHashCode().ToString();
                    msg = EnumHelper.GetDescriptionFromEnumValue(ErrorCode.Error);
                }
                else
                {
                    //开始修改
                    user.LoginName = model.LoginName;
                    user.Phone = model.Phone;
                    user.Icon = model.Icon;
                    user.Mail = model.Mail;
                    context.SaveChanges();
                }
            });
            meta.ErrorCode = msgCode;
            meta.ErrorMsg = msg;
            return true;
        }
        //删除用户
        public bool Delete(UsersQuery model, ref Meta meta)
        {
            string msg = EnumHelper.GetDescriptionFromEnumValue(ErrorCode.Success);
            string msgCode = ErrorCode.Success.GetHashCode().ToString();
            Try(context =>
            {
                //查询用户名是否存在
                var user = context.Users.Where(c => c.UserID == model.UserID && model.UserStateId == 0).FirstOrDefault();
                if (user != null)
                {
                    msgCode = ErrorCode.Error.GetHashCode().ToString();
                    msg = EnumHelper.GetDescriptionFromEnumValue(ErrorCode.Error);
                }
                else
                {
                    //停用
                    user.UserStateId = 1;
                    context.SaveChanges();
                }
            });
            meta.ErrorCode = msgCode;
            meta.ErrorMsg = msg;
            return true;
        }
        //我的钱包
        public PageModel<Users> Purse(UsersQuery queryInfo, ref Meta meta)
        {
            return Try(context =>
            {
                string sql = "SELECT vMoney FROM Users WHERE 1=1  ";
                List<SqlParameter> paramList = new List<SqlParameter>();
                //查询
                if (!string.IsNullOrEmpty(queryInfo.UserID))
                {
                    sql += " AND UserID= @UserID";
                    paramList.Add(new SqlParameter("@UserID", queryInfo.UserID));
                }
                if (!string.IsNullOrEmpty(queryInfo.LoginName))
                {
                    sql += " AND LoginName like @LoginName";
                    paramList.Add(new SqlParameter("@LoginName", "%" + queryInfo.LoginName + "%"));
                }
                return context.QuerySql<Users>(sql, queryInfo, paramList.ToArray());
            });
        }
        //我的常用地址
        public PageModel<UsersAddress> MyAddress(AddressQuery queryInfo, ref Meta meta)
        {
            return Try(context =>
            {
                string sql = "SELECT * FROM UsersAddress WHERE 1=1  ";
                List<SqlParameter> paramList = new List<SqlParameter>();
                //查询
                if (!string.IsNullOrEmpty(queryInfo.UserID))
                {
                    sql += " AND UserID= @UserID";
                    paramList.Add(new SqlParameter("@UserID", queryInfo.UserID));
                }
                if (!string.IsNullOrEmpty(queryInfo.Address))
                {
                    sql += " AND Address like @Address";
                    paramList.Add(new SqlParameter("@Address", "%" + queryInfo.Address + "%"));
                }
                if (!string.IsNullOrEmpty(queryInfo.Phone))
                {
                    sql += " AND Phone like @Phone";
                    paramList.Add(new SqlParameter("@Phone", "%" + queryInfo.Phone + "%"));
                }
                return context.QuerySql<UsersAddress>(sql, queryInfo, paramList.ToArray());
            });
        }
        //我的邀请人
        public PageModel<Users> Inviter(UsersQuery queryInfo, ref Meta meta)
        {
            return Try(context =>
            {
                string sql = "SELECT * FROM Users WHERE 1=1  ";
                List<SqlParameter> paramList = new List<SqlParameter>();
                //查询
                if (!string.IsNullOrEmpty(queryInfo.ParentID))
                {
                    sql += " AND ParentID= @ParentID";
                    paramList.Add(new SqlParameter("@ParentID", queryInfo.ParentID));
                }
                return context.QuerySql<Users>(sql, queryInfo, paramList.ToArray());
            });
        }
        //查询一条用户信息
        public Users First(string phone)
        {
            return Try(context =>
              {
                  var model = context.Users.Where(c => c.Phone == phone).FirstOrDefault();
                  return model;
              });
        }
    }
}
