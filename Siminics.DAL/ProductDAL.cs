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

        public int Add(ProductModelEntity entity)
        {
            string sql = @"INSERT INTO `productmodel` (`productid`,`modelname`,`apply`,`desc`,`downurl`,`sort`) VALUES(@productid,@modelname,@apply,@desc,@downurl,@sort);";
            MySqlParameter[] parameters=new MySqlParameter[]
            {
                new MySqlParameter("@productid",entity.productid),
                new MySqlParameter("@modelname",entity.modelname),
                new MySqlParameter("@apply",entity.apply),
                new MySqlParameter("@desc",entity.desc),
                new MySqlParameter("@downurl",entity.downurl),
                new MySqlParameter("@sort",entity.sort), 
            };

            return MySqlHelper.ExecuteNonQuery(MySqlHelper.ConnectionString,sql, parameters);
        }
    }
}
