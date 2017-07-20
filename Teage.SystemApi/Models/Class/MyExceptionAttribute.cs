using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Teage.SystemApi
{
    public class MyExceptionAttribute : HandleErrorAttribute
    {
        public static Queue<Exception> ExceptionQueue = new Queue<Exception>();
        //public static IRedisClientsManager clientManager = new PooledRedisClientManager(new string[] { "127.0.0.1:6379" });
        //public static IRedisClient redisClent = clientManager.GetClient();

        public override void OnException(ExceptionContext filterContext)
        {
            //捕获异常信息.
            ExceptionQueue.Enqueue(filterContext.Exception);//将异常信息写到队列中.
            //redisClent.EnqueueItemOnList("exceptionMsg", filterContext.Exception.ToString());
            filterContext.HttpContext.Response.Redirect("/Home/Error");//跳转到错误页面
            base.OnException(filterContext);
        }
    }
}