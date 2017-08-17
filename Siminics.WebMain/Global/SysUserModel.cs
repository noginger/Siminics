using System;
using System.Collections.Generic;
using Siminics.Model;

namespace Siminics.WebMain.Global
{
    [Serializable]
    public class SysUserModel
    {
        /// <summary>
        ///     系统用户信息
        /// </summary>
        public SysUserInfo SysUserInfo { get; set; }

        /// <summary>
        ///     当前URL
        /// </summary>
        public string CurrentUrl { get; set; }

        /// <summary>
        ///     用户登录编码号
        /// </summary>
        public string EncryptLoginId { get; set; }

        /// <summary>
        /// 姓名
        /// </summary>
        public string Name { get; set; }
    }
}