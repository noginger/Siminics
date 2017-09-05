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
    public class SlideBL
    {
        public ListBag<int, List<SlideModel>> GetItems(DbQueryParamters paramters)
        {
            return new SlideDAL().GetItems(paramters);
        }

        public List<SlideModel> GetItems(DbQueryParamtersNoPage paramters)
        {
            return new SlideDAL().GetItems(paramters);
        }

        public SlideModel GetItem(int id)
        {
            return new SlideDAL().GetItem(id);
        }

        public int Delete(int id)
        {
            return new SlideDAL().Delete(id);
        }

        public OperationResult Add(SlideModel model)
        {
            int row = new SlideDAL().Add(model);

            if (row > 0)
            {
                return new OperationResult(OperationResultType.Success,"操作成功");
            }

            return new OperationResult(OperationResultType.Failed, "操作失败");
        }

        public OperationResult Edit(SlideModel model)
        {
            int row = new SlideDAL().Edit(model);

            if (row > 0)
            {
                return new OperationResult(OperationResultType.Success, "操作成功");
            }

            return new OperationResult(OperationResultType.Failed, "操作失败");
        }
    }
}
