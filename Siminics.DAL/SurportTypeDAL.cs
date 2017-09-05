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
    public class SurportTypeDAL:DbBase<SurportTypeModel>
    {
        public SurportTypeDAL()
        {
            base.TableName = "surporttype";
            base.ColumnKey = "id";
        }

        public int Add(SurportTypeModel entity)
        {
            string sql = @"INSERT `surporttype`(`typename`,`parentid`,`shortdesc`,`typeid`,sort,imageurl) VALUES  (@typename,@parentid,@shortdesc,@typeid,@sort,@imageurl)";

            MySqlParameter[] parameters=new MySqlParameter[]
            {
                new MySqlParameter("@typename",entity.TypeName),
                new MySqlParameter("@parentid",entity.ParentId),
                new MySqlParameter("@shortdesc",entity.ShortDesc),
                new MySqlParameter("@typeid",entity.TypeId),
                new MySqlParameter("@sort",entity.Sort),
                new MySqlParameter("@imageurl",entity.ImageUrl), 
            };

            return MySqlHelper.ExecuteNonQuery(MySqlHelper.ConnectionString,sql,parameters);
        }

        public int Edit(SurportTypeModel entity)
        {
            string sql = @"Update `surporttype` set `typename`=@typename,`parentid`=@parentid,`shortdesc`=@shortdesc,`typeid`=@typeid,sort=@sort,imageurl=@imageurl where id=@id";
            
            MySqlParameter[] parameters = new MySqlParameter[]
            {
                new MySqlParameter("@typename",entity.TypeName),
                new MySqlParameter("@parentid",entity.ParentId),
                new MySqlParameter("@shortdesc",entity.ShortDesc),
                new MySqlParameter("@typeid",entity.TypeId),
                new MySqlParameter("@sort",entity.Sort),
                new MySqlParameter("@imageurl",entity.ImageUrl), 
                new MySqlParameter("@id",entity.Id), 
            };

            return MySqlHelper.ExecuteNonQuery(MySqlHelper.ConnectionString, sql, parameters);
        }
    }
}
