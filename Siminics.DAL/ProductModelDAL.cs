using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BaseLibrary.MySqlConfig;
using MySqlHelper = BaseLibrary.MySqlConfig.MySqlHelper;
using MySql.Data.MySqlClient;
using Siminics.Model;

namespace Siminics.DAL
{
    public class ProductModelDAL:DbBase<ProductModelEntity>
    {
        public ProductModelDAL()
        {
            base.TableName = "productmodel";
            base.ColumnKey = "modelid";
        }


        public int Edit(ProductModelEntity entity)
        {
            string sql = @"update productmodel SET  `productid` = @productid,`modelname` = @modelname,`apply` = @apply,`desc` = @desc,`downurl` = @downurl,`sort` = @sort
WHERE `modelid` = @modelid";

            MySqlParameter[] parameters=new MySqlParameter[]
            {
                new MySqlParameter("@productid",entity.productid),
                new MySqlParameter("@modelname",entity.modelname),
                new MySqlParameter("@apply",entity.apply),
                new MySqlParameter("@desc",entity.desc),
                new MySqlParameter("@downurl",entity.downurl),
                new MySqlParameter("@sort",entity.sort),
                new MySqlParameter("@modelid",entity.ModelId),
            };

            int row = MySqlHelper.ExecuteNonQuery(MySqlHelper.ConnectionString, sql, parameters);
            if (row > 0)
            {
                sql = string.Format("delete from modelsource where modelid={0}",entity.ModelId);
                MySqlHelper.ExecuteNonQuery(MySqlHelper.ConnectionString, sql, null);

                sql = "insert modelsource(imageurl,modelid,typeid) values(@imageurl,@modelid,@typeid)";

                foreach (var image in entity.Images)
                {
                    parameters = new MySqlParameter[]
                    {
                    new MySqlParameter("@imageurl", image.imageurl),
                    new MySqlParameter("@modelid", entity.ModelId),
                    new MySqlParameter("@typeid", image.TypeId)
                    };

                    MySqlHelper.ExecuteNonQuery(MySqlHelper.ConnectionString, sql, parameters);
                }
            }

            return 1;
        }

        public IList<ModelSourceModel> GetImages(int modelId)
        {
            string sql = string.Format("SELECT * FROM `modelsource` where modelId={0} order by sourceid asc", modelId);
            return MySqlHelper.ExecuteObjects<ModelSourceModel>(sql, null);
        }

        public IList<ModelSourceModel> GetImages(IList<int> modelId)
        {
            string sql= string.Format("SELECT * FROM `modelsource` where modelId in ({0}) order by sourceid asc", string.Join(",",modelId));
            return MySqlHelper.ExecuteObjects<ModelSourceModel>(sql, null);
        }

    }
}
