using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Teage.Common;

namespace Teage.UI.Areas.Main.Controllers
{
    public class BaseController : Controller
    {
         /// <summary>
        /// 执行控制器中方法里面的代码之前先执行该方法.
        /// </summary>
        /// <param name="filterContext"></param>
        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
           // if (Session["adminInfo"] == null)
            //if(Session["user"]==null)
            if (Request.Cookies["Token"] == null)//获取Cookie中存储的自定义的SessionId
            {
                WebCommon.GoPage();

            }
        }
	}
}