using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Teage.Model;

namespace Teage.SystemApi
{
    public class LoginResponse
    {
        public string Token { get; set; }

        public string Phone { get; set; }

        public string UserName { get; set; }

        //public string RoleName { get; set; }

        //public List<Module> Modules { get; set; }

        public void SetValue(Users user)
        {
            UserName = user.LoginName;
            Phone = user.Phone;
            //RoleName = user.Role.RoleName;
            //Modules = user.Role.Modules;
        }
    }
}