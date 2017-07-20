using IServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Teage.ApiCommon;
using Teage.Common;
using Teage.Entity;
using Teage.Model;

namespace Services
{
    public class Captchas : Service, ICaptcha
    {
        //新增验证码 
        public bool Add(Captcha model, ref Meta meta)
        {
            string msg = "";
            string msgCode = "";
            model.ID = Guid.NewGuid().ToString().Replace("-", "");
            //

            Try(context =>
            {
                context.Captcha.Add(model);
                context.SaveChanges();
            });
            meta.ErrorCode = msgCode;
            meta.ErrorMsg = msg;
            return true;
        }
        //查询验证码信息
        public Captcha First(string key)
        {
            return Try(context =>
            {
                var model = context.Captcha.Where(c => c.ID == key).FirstOrDefault();
                return model;
            });
        }

    }
}
