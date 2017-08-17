using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Contexts;
using System.Text;
using System.Threading.Tasks;
using BaseLibrary.MySqlConfig;
using Siminics.Model;
using System.Data.SqlClient;

namespace Siminics.DAL
{
    public class SysUser
    {
        #region default

        //public SysUser()
        //{
        //    base.TableName = "sysuser";
        //    base.ColumnKey = "UserId";
        //}

        
        #endregion

        #region 登录
        /// <summary>
        /// 登录
        /// </summary>
        /// <param name="userInfo"></param>
        /// <returns></returns>
        public SysUserInfo Login(SysUserInfo userInfo)
        {
            if (string.IsNullOrEmpty(userInfo.UserName) || string.IsNullOrEmpty(userInfo.PassWord))
                return new SysUserInfo();

            string sql = "select * from sysuser where UserName='{0}' and PassWord='{1}' and IsActive=1";
            
            //MySqlHelper.ExecuteObject<SysUserInfo>(sql,null);
            return new SysUserInfo();

            //var entity = Context.Sql(string.Format("select * from sysuser where UserName='{0}' and PassWord='{1}' and IsActive=1",
            //                              userInfo.UserName, userInfo.PassWord))
            //        .QuerySingle<SysUserInfo>();
            //if (entity == null || entity.UserId <= 0)
            //    return new SysUserInfo();

            //entity.LastLoginIp = entity.LoginIp;
            //entity.LastLoginDate = entity.LoginDate;

            //entity.LoginIp = userInfo.LoginIp;
            //entity.LoginDate =DateTime.Now;

            //int rowsAffected = Context.Update(TableName)
            //    .Column("LastLoginIp", entity.LastLoginIp)
            //    .Column("LastLoginDate", entity.LastLoginDate)
            //    .Column("LoginIp", entity.LoginIp)
            //    .Column("LoginCount", entity.LoginCount + 1)
            //    .Column("LoginDate", entity.LoginDate)
            //    .Where("UserId", entity.UserId)
            //    .Execute();

            //return entity;
        }

        #endregion
    }
}
