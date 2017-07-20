using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;
using Teage.UI.Models;

namespace Teage.UI.Areas.Main.Controllers
{
    public class ExpressController : Controller
    {
        //UD列表
        public ActionResult Index()
        {
            ViewBag.api = SystemApi.Api;
            return View();
        }
        //UD详情
        public ActionResult About(string Id,int flag,int type)
        {
            ViewBag.api = SystemApi.Api;
            ViewBag.Flag = flag;
            ViewBag.Type = type;
            ViewBag.Id = Id;
            return View();
        }
        //新增UD
        public ActionResult Add()
        {
            ViewBag.Url = "/Main/Express/Add";
            ViewBag.api = SystemApi.Api;
            return View();
        }
        //编辑UD
        public ActionResult Edit()
        {
            return View();
        }

        //改版
        public ActionResult Indexs()
        {
            ViewBag.api = SystemApi.Api;
            return View();
        }
        public ActionResult Indexss()
        {
            ViewBag.api = SystemApi.Api;
            return View();
        }
        //新增UD
        public ActionResult Adds()
        {
            ViewBag.Url = "/Main/Express/Add";
            ViewBag.api = SystemApi.Api;
            return View();
        }
        public ActionResult Indexes()
        {
            ViewBag.api = SystemApi.Api;
            return View();
        }
	}
}