using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Teage.UI.Models;

namespace Teage.UI.Areas.Main.Controllers
{
    public class UsersController : Controller
    {
        //用户主页
        public ActionResult Index()
        {
            return View();
        }
        //注册
        public ActionResult Reg()
        {
            ViewBag.api = SystemApi.Api;
            return View();
        }
        //登录
        public ActionResult Login()
        {
            return View();
        }
	}
}