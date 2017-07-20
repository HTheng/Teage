using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Teage.UI.Controllers
{
    public class HomeController : Controller
    {
        //登录主页
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Indexs()
        {
            return View();
        }
        //错误页
        public ActionResult Error()
        {
            string msg = string.IsNullOrEmpty(Request["Msg"]) ? "暂无信息" : Server.UrlDecode(Request["Msg"]);
            string txt = string.IsNullOrEmpty(Request["Txt"]) ? "首页" : Server.UrlDecode(Request["Txt"]);
            string url = Request["Url"].ToString().Trim()=="/Home/Error" ? "/Home/Index" : Request["Url"];
            ViewBag.Msg = msg;
            ViewBag.Txt = txt;
            ViewBag.Url = url;
            return View();
        }
        public ActionResult About()
        {
            return View();
        }
        //测试地图(未使用)
        public ActionResult Map()
        {
            return View();
        }
    }
}