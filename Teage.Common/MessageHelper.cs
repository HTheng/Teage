using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Hosting;
using System.Xml.Linq;

namespace Teage.Common
{
    public static class MessageHelper
    {
        /// <summary>
        /// 获取短信消息内容
        /// </summary>
        /// <param name="name">节点名称</param>
        /// <param name="property">属性名称</param>
        /// <returns></returns>
        public static string GetMessageContent(string name, string property)
        {
            string path = HostingEnvironment.MapPath("~/bin/Xml/message.xml");
            XElement element = XElement.Load(path);
            var query = element.Elements("sms").Descendants("message").Where(x => (string)x.Attribute("name") == name).FirstOrDefault().Element(property);
            return query.Value;
        }

    }
}
