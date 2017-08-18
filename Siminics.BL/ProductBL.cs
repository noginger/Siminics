using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BaseLibrary.Common;
using Siminics.DAL;
using Siminics.Model;

namespace Siminics.BL
{
    public class ProductBL
    {
        public ListBag<int, List<ProductModelEntity>> GetItems(DbQueryParamters paramters)
        {
            return new ProductModelDAL().GetItems(paramters);
        }


        public List<ProductModel> GetItems(DbQueryParamtersNoPage paramters)
        {
            return new ProductDAL().GetItems(paramters);
        }


        public OperationResult AddModel(ProductModelEntity entity)
        {
            try
            {
                int row = new ProductDAL().Add(entity);
                if (row > 0)
                {
                    return new OperationResult(OperationResultType.Success, "操作成功");
                }
            }
            catch (Exception ex)
            {
                return new OperationResult(OperationResultType.Failed, "操作失败,"+ex.Message);
            }

            return new OperationResult(OperationResultType.Failed, "操作失败，请重试");
        }
    }
}
