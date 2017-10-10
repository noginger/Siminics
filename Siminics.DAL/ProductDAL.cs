using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BaseLibrary.MySqlConfig;
using MySql.Data;
using MySql.Data.MySqlClient;
using Siminics.Model;
using MySqlHelper = BaseLibrary.MySqlConfig.MySqlHelper;

namespace Siminics.DAL
{
    public class ProductDAL:DbBase<ProductModel>
    {
        public ProductDAL()
        {
            base.TableName = "product";
            base.ColumnKey = "productId";
        }

        public int AddType(string typeName,int sort,string imageurl)
        {
            string sql = "insert product(productname,sort,imageurl) values(@productname,@sort,@imageurl)";
            MySqlParameter[] parameters = new MySqlParameter[]
            {
                new MySqlParameter("@productname",typeName),
                new MySqlParameter("@sort",sort),
                new MySqlParameter("@imageurl",imageurl), 
            };

            return MySqlHelper.ExecuteNonQuery(MySqlHelper.ConnectionString,sql,parameters);
        }

        public int EditType(ProductModel entity)
        {
            string sql = "update product set productname=@productname,sort=@sort,imageurl=@imageurl where productid=@productid";
            MySqlParameter[] parameters=new MySqlParameter[]
            {
                new MySqlParameter("@productname",entity.productname),
                new MySqlParameter("@sort",entity.sort),
                new MySqlParameter("@productid",entity.ProductId),  
                new MySqlParameter("@imageurl",entity.ImageUrl),  
            };
            return MySqlHelper.ExecuteNonQuery(MySqlHelper.ConnectionString,sql,parameters);
        }

        public int Add(ProductModelEntity entity)
        {
            string sql = @"INSERT INTO `productmodel` (`productid`,`modelname`,`apply`,`desc`,`downurl`,`sort`) VALUES(@productid,@modelname,@apply,@desc,@downurl,@sort);select @@identity";
            MySqlParameter[] parameters=new MySqlParameter[]
            {
                new MySqlParameter("@productid",entity.productid),
                new MySqlParameter("@modelname",entity.modelname),
                new MySqlParameter("@apply",entity.apply),
                new MySqlParameter("@desc",entity.desc),
                new MySqlParameter("@downurl",entity.downurl),
                new MySqlParameter("@sort",entity.sort), 
            };

            int modelId = MySqlHelper.ExecuteScalar<int>(MySqlHelper.ConnectionString,sql, parameters);

            sql = "insert modelsource(imageurl,modelid,typeid) values(@imageurl,@modelid,@typeid)";

            foreach (var image in entity.Images)
            {
                parameters = new MySqlParameter[]
                {
                    new MySqlParameter("@imageurl", image.imageurl),
                    new MySqlParameter("@modelid", modelId),
                    new MySqlParameter("@typeid", image.TypeId)
                };

                MySqlHelper.ExecuteNonQuery(MySqlHelper.ConnectionString,sql,parameters);
            }

            return modelId;
        }
    }
}
