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
    public class SurportTypeBL
    {

        public ListBag<int, List<SurportTypeModel>> GetItems(DbQueryParamters paramters)
        {
            return new SurportTypeDAL().GetItems(paramters);
        }

        public List<SurportTypeModel> GetItems(DbQueryParamtersNoPage paramters)
        {
            return new SurportTypeDAL().GetItems(paramters);
        }

        public SurportTypeModel GetItem(int id)
        {
            return new SurportTypeDAL().GetItem(id);
        }

        public OperationResult Add(SurportTypeModel entity)
        {
            int row=new SurportTypeDAL().Add(entity);
            if (row > 0)
            {
                return new OperationResult(OperationResultType.Success,"操作成功");
            }

            return new OperationResult(OperationResultType.Failed,"操作失败，请重试");
        }

        public OperationResult Edit(SurportTypeModel entity)
        {
            int row = new SurportTypeDAL().Edit(entity);
            if (row > 0)
            {
                return new OperationResult(OperationResultType.Success, "操作成功");
            }

            return new OperationResult(OperationResultType.Failed, "操作失败，请重试");
        }

        public int Delete(int id)
        {
            return new SurportTypeDAL().Delete(id);
        }
    }
}
