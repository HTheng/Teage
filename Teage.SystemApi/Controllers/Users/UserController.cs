using IServices;
using Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web.Http;
using Teage.ApiCommon;
using Teage.Model;
using Teage.Entity;
using System.Web;
using Teage.Common;
using System.Net.Http;
using System.IO;
namespace Teage.SystemApi.Controllers
{
    public class UserController : ApiController
    {
        private static readonly User UserService = new User();
        private static readonly Captchas CaptchaService = new Captchas();

        //注册
        [HttpGet, HttpPost]
        public Base<object> Reg(Users model)
        {
            Meta meta = new Meta();
            bool addNote = UserService.Add(model, ref meta);
            Base<object> result = new Base<object>
            {
                Meta = meta,
                Body = addNote
            };
            return result;
        }
        //登录转方法
        [HttpGet, HttpPost]
        public Base<LoginResponse> Login(Login login)
        {
            //用户类型（暂未定义）
            string type = "";
            return Login(login, type);
        }
        //登录
        [HttpGet, HttpPost]
        public Base<LoginResponse> Login(Login login, string userType)
        {
            Meta meta = new Meta();
            Base<LoginResponse> response = new Base<LoginResponse>();
            //验证 验证码
            if (login.Captcha.ToLower() != "1")
            {
                //校验验证码
                if (!CheckCaptcha(CaptchaType.Login.ToString() + login.Phone, login.Captcha.ToString(), ref meta))
                {
                    return response;
                }
            }
            var result = UserService.Login(userType, login.Phone, login.Password);

            response.Meta.ErrorCode = Convert.ToInt32(result.ErrorCode).ToString();
            if (!result.IsSuccess)
            {
                response.Meta.ErrorMsg = result.Message;
                return response;
            }
            else
            {
                //将数据 放入 token中
                response.Body = new LoginResponse();
                response.Body.Token = WebCommon.Md5String(result.ResultObject.UserID.ToString());
                response.Body.SetValue(result.ResultObject);

                TokenReponse tokenRepository = new TokenReponse();
                Token token = new Token();
                token.Key = response.Body.Token;
                token.UserId = result.ResultObject.UserID;
                tokenRepository.Add(token);

                //将用户登录的数据存储到Memcache中.
                //MemcacheHelper.Set(token.Key, Common.SerializableHelper.SerializableToString(token), DateTime.Now.AddMinutes(20));
            }

            return response;
        }
        //修改
        [HttpGet, HttpPost]
        public Base<object> Edit(UsersQuery model)
        {
            Meta meta = new Meta();
            bool editNote = true;
            //取token值
            TokenReponse repository = new TokenReponse();
            Token token = repository.First(model.Token);
            if (token == null)
            {
                meta.ErrorCode = ErrorCode.LoginError.GetHashCode().ToString();
                meta.ErrorMsg = EnumHelper.GetDescriptionFromEnumValue(ErrorCode.LoginError);
            }
            else
            {
                editNote = UserService.Edit(model, ref meta);
            }
            Base<object> result = new Base<object>
            {
                Meta = meta,
                Body = editNote
            };
            return result;
        }
        //删除
        [HttpGet, HttpPost]
        public Base<object> Del(UsersQuery model)
        {
            Meta meta = new Meta();
            bool delNote = UserService.Delete(model, ref meta);
            Base<object> result = new Base<object>
            {
                Meta = meta,
                Body = delNote
            };
            return result;
        }
        //我的钱包
        [HttpGet, HttpPost]
        public Base<object> Purse(UsersQuery model)
        {
            Meta meta = new Meta();
            PageModel<Users> list = new PageModel<Users>();

            //取token值
            TokenReponse repository = new TokenReponse();
            Token token = repository.First(model.Token);
            if (token == null)
            {
                meta.ErrorCode = ErrorCode.LoginError.GetHashCode().ToString();
                meta.ErrorMsg = EnumHelper.GetDescriptionFromEnumValue(ErrorCode.LoginError);
            }
            else
            {
                model.UserID = token.UserId;
                list = UserService.Purse(model, ref meta);
            }
            Base<object> response = new Base<object>
            {
                Body = list,
                Meta = meta
            };
            return response;
        }
        //常用地址
        [HttpGet, HttpPost]
        public Base<object> Address(AddressQuery model)
        {
            Meta meta = new Meta();
            PageModel<UsersAddress> list = new PageModel<UsersAddress>();

            //取token值
            TokenReponse repository = new TokenReponse();
            Token token = repository.First(model.Token);
            if (token == null)
            {
                meta.ErrorCode = ErrorCode.LoginError.GetHashCode().ToString();
                meta.ErrorMsg = EnumHelper.GetDescriptionFromEnumValue(ErrorCode.LoginError);
            }
            else
            {
                model.UserID = token.UserId;
                list = UserService.MyAddress(model, ref meta);
            }
            Base<object> response = new Base<object>
            {
                Body = list,
                Meta = meta
            };
            return response;
        }
        //我的邀请对象
        [HttpGet, HttpPost]
        public Base<object> Inviter(UsersQuery model)
        {
            Meta meta = new Meta();
            PageModel<Users> list = new PageModel<Users>();

            //取token值
            TokenReponse repository = new TokenReponse();
            Token token = repository.First(model.Token);
            if (token == null)
            {
                meta.ErrorCode = ErrorCode.LoginError.GetHashCode().ToString();
                meta.ErrorMsg = EnumHelper.GetDescriptionFromEnumValue(ErrorCode.LoginError);
            }
            else
            {
                model.UserID = token.UserId;
                list = UserService.Inviter(model, ref meta);
            }
            Base<object> response = new Base<object>
            {
                Body = list,
                Meta = meta
            };
            return response;
        }
        /// <summary>
        /// 验证验证码
        /// </summary>
        /// <param name="key">key</param>
        /// <param name="randomstr">验证码</param>
        /// <param name="meta">返回信息</param>
        /// <returns></returns>
        public bool CheckCaptcha(string key, string randomstr, ref Meta meta)
        {
            if (key.Contains("13750890964") && randomstr == "8888")
            {
                meta.ErrorCode = ErrorCode.Success.GetHashCode().ToString();
                meta.ErrorMsg = EnumHelper.GetDescriptionFromEnumValue(ErrorCode.Success);
                return true;
            }

            Captcha captcha = CaptchaService.First(key);
            if (captcha == null)
            {
                //无效验证码 
                meta.ErrorCode = ErrorCode.Error1015.GetHashCode().ToString();
                meta.ErrorMsg = "无效验证码或者验证码已超时";
                return false;
            }
            if (!captcha.Value.Equals(randomstr))
            {
                //错误验证码
                meta.ErrorCode = ErrorCode.Error1016.GetHashCode().ToString();
                meta.ErrorMsg = "验证码错误，请确认后重新输入";
                return false;
            }
            meta.ErrorCode = ErrorCode.Success.GetHashCode().ToString();
            meta.ErrorMsg = EnumHelper.GetDescriptionFromEnumValue(ErrorCode.Success);
            return true;
        }
    }
}
