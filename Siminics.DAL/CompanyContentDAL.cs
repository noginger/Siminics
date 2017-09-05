using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BaseLibrary.MySqlConfig;
using MySql.Data.MySqlClient;
using Siminics.Model;
using MySqlHelper=BaseLibrary.MySqlConfig.MySqlHelper;

namespace Siminics.DAL
{
    public class CompanyContentDAL:DbBase<CompanyContentModel>
    {
        public CompanyContentDAL()
        {
            base.TableName = "companycontent";
            base.ColumnKey = "contentid";
        }

        public int Add(CompanyContentModel entity)
        {
            string sql = "Insert companycontent(typeid,title,content,createtime,imageurl,sort) values(@typeid,@title,@content,@createtime,@imageurl,@sort)";
            MySqlParameter[] parameters=new MySqlParameter[]
            {
                new MySqlParameter("@typeid",entity.typeid),
                new MySqlParameter("@title",entity.title),
                new MySqlParameter("@content",entity.content),
                new MySqlParameter("@createtime",DateTime.Now),
                new MySqlParameter("@imageurl",entity.imageurl),
                new MySqlParameter("@sort",entity.sort),
            };

            return MySqlHelper.ExecuteNonQuery(MySqlHelper.ConnectionString, sql, parameters);
        }

        public int Edit(CompanyContentModel entity)
        {
            string sql = "update companycontent set typeid=@typeid,title=@title,content=@content,imageurl=@imageurl,sort=@sort where contentid=@contentid";

            MySqlParameter[] parameters=new MySqlParameter[]
            {
                new MySqlParameter("@typeid",entity.typeid),
                new MySqlParameter("@title",entity.title),
                new MySqlParameter("@content",entity.content),
                new MySqlParameter("@imageurl",entity.imageurl),
                new MySqlParameter("@sort",entity.sort),
                new MySqlParameter("@contentid",entity.contentid), 
            };

            return MySqlHelper.ExecuteNonQuery(MySqlHelper.ConnectionString, sql, parameters);
        }
    }
}
