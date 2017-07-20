using log4net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using Teage.UI.Models;

namespace Teage.UI
{
    public class MvcApplication : HttpApplication
    {
        protected void Application_Start()
        {
            log4net.Config.XmlConfigurator.Configure();//读取Log4Net配置信息.
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            //开启一个线程，处理异常信息.
            string filePath = Server.MapPath("/Log/");
            ThreadPool.QueueUserWorkItem(o =>
            {
                while (true)
                {
                    if (MyExceptionAttribute.ExceptionQueue.Count > 0)
                    //if (MyExceptionAttribute.redisClent.GetListCount("exceptionMsg") > 0)
                    {
                        Exception ex = MyExceptionAttribute.ExceptionQueue.Dequeue();
                        //string exceptionMsg = MyExceptionAttribute.redisClent.DequeueItemFromList("exceptionMsg");
                        if (ex != null)
                        //if (!string.IsNullOrEmpty(exceptionMsg))
                        {
                            //string fullPath = filePath + DateTime.Now.ToString("yyyy-MM-dd")+".txt";
                            //File.AppendAllText(fullPath, ex.ToString(), Encoding.Default);
                            ILog logger = LogManager.GetLogger("ex");
                            logger.Error(ex);
                        }
                        else
                        {
                            Thread.Sleep(3000);
                        }
                    }
                    else
                    {
                        Thread.Sleep(3000);
                    }
                }

            }, filePath);
        }
    }
}
