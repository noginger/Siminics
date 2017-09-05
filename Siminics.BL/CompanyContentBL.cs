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
    public class CompanyContentBL
    {
        public ListBag<int, List<CompanyContentModel>> GetItems(DbQueryParamters paramters)
        {
            return new CompanyContentDAL().GetItems(paramters);
        }

        public List<CompanyContentModel> GetItems(DbQueryParamtersNoPage paramters)
        {
            return new CompanyContentDAL().GetItems(paramters);
               
        }

        public CompanyContentModel GetItem(int id)
        {
            return new CompanyContentDAL().GetItem(id);
        }

        public OperationResult Add(CompanyContentModel entity)
        {
            int row = new CompanyContentDAL().Add(entity);

            if (row > 0)
            {
                return new OperationResult(OperationResultType.Success,"操作成功");
            }

            return new OperationResult(OperationResultType.Failed, "操作失败,请重试");
        }

        public OperationResult Edit(CompanyContentModel entity)
        {
            int row = new CompanyContentDAL().Edit(entity);

            if (row > 0)
            {
                return new OperationResult(OperationResultType.Success, "操作成功");
            }

            return new OperationResult(OperationResultType.Failed, "操作失败,请重试");
        }

        public int Delete(int id)
        {
            return new CompanyContentDAL().Delete(id);
        }
    }
}
