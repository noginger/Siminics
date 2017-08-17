using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BaseLibrary.Common;
using Siminics.Model;

namespace Siminics.BL
{
    public class SysUser
    {
        #region 登录

        /// <summary>
        /// 登录
        /// </summary>
        /// <param name="userInfo"></param>
        /// <returns></returns>
        public OperationResult Login(SysUserInfo userInfo)
        {
            if (string.IsNullOrEmpty(userInfo.UserName) || string.IsNullOrEmpty(userInfo.PassWord))
                return new OperationResult(OperationResultType.ParamError, "请输入用户名/密码");

            SysUserInfo loginInfo = new DAL.SysUser().Login(userInfo);
            if (loginInfo == null || loginInfo.UserId <= 0)
                return new OperationResult(OperationResultType.QueryNull, "用户名/密码错误");

            return new OperationResult(OperationResultType.Success, "登录成功", loginInfo);
        }

        #endregion
    }
}
