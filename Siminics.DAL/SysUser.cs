using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Contexts;
using System.Text;
using System.Threading.Tasks;
using Siminics.Model;
using System.Data.SqlClient;
using MySql.Data.MySqlClient;
using MySqlHelper = BaseLibrary.MySqlConfig.MySqlHelper;

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

            string sql = string.Format("select * from sysuser where UserName='{0}' and PassWord='{1}' and IsActive=1",userInfo.UserName,userInfo.PassWord);

             var entity = MySqlHelper.ExecuteObject<SysUserInfo>(sql, null);

            if (entity == null || entity.UserId <= 0)
                return new SysUserInfo();

            entity.LastLoginIp = entity.LoginIp;
            entity.LastLoginDate = entity.LoginDate;

            entity.LoginIp = userInfo.LoginIp;
            entity.LoginDate = DateTime.Now;

            MySqlParameter[] parameters = new MySqlParameter []
            {
                new MySqlParameter("@LastLoginIp",entity.LastLoginIp),
                new MySqlParameter("@LastLoginDate",entity.LastLoginDate),
                new MySqlParameter("@LoginIp",entity.LoginIp),
                new MySqlParameter("@LoginDate",entity.LoginDate),
                new MySqlParameter("@UserId",entity.UserId),
            };

            MySqlHelper.ExecuteNonQuery(MySqlHelper.ConnectionString, @"update sysuser set LastLoginIp=@LastLoginIp,LastLoginDate=@LastLoginDate,LoginIp=@LoginIp,LoginDate=@LoginDate 
                                                                        Where UserId=@UserId", parameters);
            
            return entity;
        }

        #endregion
    }
}
