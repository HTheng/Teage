using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Teage.Common;
using Teage.UI.Models;

namespace Teage.UI.Controllers
{
    public class LoginController : Controller
    {
        //登录窗口
        public ActionResult Index()
        {
            string url = Request["Url"] == null ? "" : Request["Url"].ToString().Trim().Replace("'", ""); ;
            ViewBag.Url = url;
            ViewBag.api = SystemApi.Api;
            return View();
        }
        /// <summary>
        /// 验证码
        /// </summary>
        /// <returns></returns>
        public ActionResult VerificationCode()
        {
            ValidateCode validateCode = new Common.ValidateCode();
            string code = validateCode.CreateValidateCode(4);//生成验证码.
            Session["ValidateCode"] = code;
            byte[] buffer = validateCode.CreateValidateGraphic(code);
            return File(buffer, "image/jpeg");
        }
        public ActionResult GetCurrentValidateCode()
        {
            return Content(Session["ValidateCode"].ToString());
        }
        //demo 
        public ActionResult Indexs()
        {
            string url = Request["Url"] == null ? "" : Request["Url"].ToString().Trim().Replace("'", ""); ;
            ViewBag.Url = url;
            ViewBag.api = SystemApi.Api;
            return View();
        }
        //demo 
        public ActionResult Indexes()
        {
            string url = Request["Url"] == null ? "" : Request["Url"].ToString().Trim().Replace("'", ""); ;
            ViewBag.Url = url;
            ViewBag.api = SystemApi.Api;
            return View();
        }
    }
}