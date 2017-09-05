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
    public class NewsContentDAL:DbBase<NewsContentModel>
    {
        public NewsContentDAL()
        {
            base.TableName = "newscontent";
            base.ColumnKey = "newsid";
        }

        public int Add(NewsContentModel entity)
        {
            string sql = "insert newscontent(`typeid`,`title`,`content`,`imageurl`,`createtime`,`shortdesc`,`productid`,`sort`) values(@typeid,@title,@content,@imageurl,@createtime,@shortdesc,@productid,@sort)";
            MySqlParameter[] parameters=new MySqlParameter[]
            {
                new MySqlParameter("@typeid",entity.typeid),
                new MySqlParameter("@title",entity.title),
                new MySqlParameter("@content",entity.content),
                new MySqlParameter("@imageurl",entity.imageurl),
                new MySqlParameter("@createtime",entity.createtime),
                new MySqlParameter("@shortdesc",entity.shortdesc),
                new MySqlParameter("@productid",entity.ProductId),
                new MySqlParameter("@sort",entity.Sort)
            };

            return MySqlHelper.ExecuteNonQuery(MySqlHelper.ConnectionString, sql, parameters);
        }

        public int Edit(NewsContentModel entity)
        {
            string sql = "update newscontent set typeid=@typeid,title=@title,content=@content,imageurl=@imageurl,shortdesc=@shortdesc,productid=@productid,sort=@sort where newsid=@newsid";
            MySqlParameter[] parameters = new MySqlParameter[]
            {
                new MySqlParameter("@typeid",entity.typeid),
                new MySqlParameter("@title",entity.title),
                new MySqlParameter("@content",entity.content),
                new MySqlParameter("@imageurl",entity.imageurl),
                new MySqlParameter("@newsid",entity.NewsId),
                new MySqlParameter("@shortdesc",entity.shortdesc),
                new MySqlParameter("@productid",entity.ProductId),
                new MySqlParameter("@sort",entity.Sort)
            };

            return MySqlHelper.ExecuteNonQuery(MySqlHelper.ConnectionString, sql, parameters);
        }
    }
}
