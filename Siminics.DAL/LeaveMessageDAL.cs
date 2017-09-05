using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BaseLibrary.MySqlConfig;
using MySql.Data.MySqlClient;
using Siminics.Model;
using MySqlHelper = BaseLibrary.MySqlConfig.MySqlHelper;

namespace Siminics.DAL
{
    public class LeaveMessageDAL:DbBase<LeaveMessageModel>
    {
        public LeaveMessageDAL()
        {
            base.TableName = "leavemessage";
            base.ColumnKey = "id";
        }


        public int Add(LeaveMessageModel entity)
        {
            string sql = "insert leavemessage(username,phone,company,email,message,createtime) values(@username,@phone,@company,@email,@message,@createtime)";

            MySqlParameter[] parameters=new MySqlParameter[]
            {
                new MySqlParameter("@username",entity.UserName),
                new MySqlParameter("@phone",entity.Phone),
                new MySqlParameter("@company",entity.Company),
                new MySqlParameter("@email",entity.Email),
                new MySqlParameter("@message",entity.Message),
                new MySqlParameter("@createtime",DateTime.Now),
            };

            return MySqlHelper.ExecuteNonQuery(MySqlHelper.ConnectionString, sql, parameters);
        }
    }

}
