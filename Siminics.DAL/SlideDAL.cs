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
    public class SlideDAL:DbBase<SlideModel>
    {
        public SlideDAL()
        {
            base.TableName = "slide";
            base.ColumnKey = "id";
        }

        public int Add(SlideModel entity)
        {
            string sql = "insert slide(`name`,position,link,title,imageurl,sort) values(@name,@position,@link,@title,@imageurl,@sort)";

            MySqlParameter[] parameters=new MySqlParameter[]
            {
                new MySqlParameter("@name",entity.name),
                new MySqlParameter("@position",entity.position),
                new MySqlParameter("@link",entity.link),
                new MySqlParameter("@title",entity.title),
                new MySqlParameter("@imageurl",entity.imageurl),
                new MySqlParameter("@sort",entity.Sort),
            };

            return MySqlHelper.ExecuteNonQuery(MySqlHelper.ConnectionString, sql, parameters);
        }

        public int Edit(SlideModel entity)
        {
            string sql = "update slide set `name`=@name,position=@position,link=@link,title=@title,imageurl=@imageurl,sort=@sort where id=@id";
            MySqlParameter[] parameters = new MySqlParameter[]
            {
                new MySqlParameter("@name",entity.name),
                new MySqlParameter("@position",entity.position),
                new MySqlParameter("@link",entity.link),
                new MySqlParameter("@title",entity.title),
                new MySqlParameter("@imageurl",entity.imageurl),
                new MySqlParameter("@id",entity.id),
                new MySqlParameter("@sort",entity.Sort),
            };

            return MySqlHelper.ExecuteNonQuery(MySqlHelper.ConnectionString, sql, parameters);

        }
    }
}
