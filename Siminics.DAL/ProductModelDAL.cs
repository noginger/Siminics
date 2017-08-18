using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BaseLibrary.MySqlConfig;
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
    }
}
