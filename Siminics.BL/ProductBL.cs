using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BaseLibrary.Common;
using BaseLibrary.MySqlConfig;
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

        public ListBag<int, List<ProductModel>> GetProductItems(DbQueryParamters paramters)
        {
            return new ProductDAL().GetItems(paramters);
        }

        public ProductModel GetProductItem(int id)
        {
            return new ProductDAL().GetItem(id);
        }

        public int DeleteType(int id)
        {
            return new ProductDAL().Delete(id);
        }

        public int Delete(int id)
        {
            return new ProductModelDAL().Delete(id);
        }

        public List<ProductModel> GetItems(DbQueryParamtersNoPage paramters)
        {
            return new ProductDAL().GetItems(paramters);
        }

        public List<ProductModelEntity> GetModelItems(DbQueryParamtersNoPage paramters)
        {
            return new ProductModelDAL().GetItems(paramters);
        }

        public IList<ModelSourceModel> GetModelImages(IList<int> modelIds)
        {
            return new ProductModelDAL().GetImages(modelIds);
        }

        public ProductModelEntity GetItem(int id)
        {
            ProductModelEntity entity = new ProductModelDAL().GetItem(id);
            if (entity != null)
            {
                entity.Images = new ProductModelDAL().GetImages(id);
            }

            return entity;
        }

        public OperationResult Add(string typeName,int sort)
        {
            int row = new ProductDAL().AddType(typeName,sort);
            if (row > 0)
            {
                return new OperationResult(OperationResultType.Success, "操作成功");
            }

            return new OperationResult(OperationResultType.Failed, "操作失败，请重试");
        }

        public OperationResult EditType(ProductModel entity)
        {
            int row = new ProductDAL().EditType(entity);
            if (row > 0)
            {
                return new OperationResult(OperationResultType.Success, "操作成功");
            }

            return new OperationResult(OperationResultType.Failed, "操作失败，请重试");
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
                return new OperationResult(OperationResultType.Failed, "操作失败," + ex.Message);
            }

            return new OperationResult(OperationResultType.Failed, "操作失败，请重试");
        }


        public OperationResult EditModel(ProductModelEntity entity)
        {
            try
            {
                int row = new ProductModelDAL().Edit(entity);
                if (row > 0)
                {
                    return new OperationResult(OperationResultType.Success, "操作成功");
                }
            }
            catch (Exception ex)
            {
                return new OperationResult(OperationResultType.Failed, "操作失败," + ex.Message);
            }
            return new OperationResult(OperationResultType.Failed, "操作失败");
        }
        
    }
}
