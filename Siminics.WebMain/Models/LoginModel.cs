using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using BaseLibrary.Common;

namespace Siminics.WebMain.Models
{
    public class LoginModel: BaseMvcModel
    {
        /// <summary>
        /// 登录账号
        /// </summary>
        [Display(Name = "姓名"), Required(ErrorMessage = "账号不能为空")]
        public string UserName { get; set; }

        /// <summary>
        /// 登录密码
        /// </summary>
        [Required(ErrorMessage = "密码不能为空")]
        [DataType(DataType.Password)]
        public string PassWord { get; set; }

        public string LoginIp { get; set; }

        public DateTime LoginDate { get; set; }
        /// <summary>
        ///     是否记住登录
        /// </summary>
        public bool IsRememberLogin { get; set; }

        /// <summary>
        ///     登录成功后返回地址
        /// </summary>
        public string ReturnUrl { get; set; }
    }
}