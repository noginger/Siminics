using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Siminics.Model;
using Siminics.WebMain.Models;

namespace Siminics.WebMain.Global
{
    public class LoginMaper
    {
        public static SysUserInfo ModelMapInfo(LoginModel model)
        {
            var info = new SysUserInfo()
            {
                UserName = model.UserName,
                PassWord = model.PassWord,
                LoginIp = model.LoginIp,
                LoginDate = model.LoginDate
            };

            return info;
        }
    }
}