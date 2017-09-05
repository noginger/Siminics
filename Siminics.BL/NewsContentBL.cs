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
    public class NewsContentBL
    {
        public ListBag<int, List<NewsContentModel>> GetItems(DbQueryParamters paramters)
        {
            return new NewsContentDAL().GetItems(paramters);
        }

        public List<NewsContentModel> GetItems(DbQueryParamtersNoPage paramters)
        {
            return new NewsContentDAL().GetItems(paramters);
        }

        public NewsContentModel GetItem(int id)
        {
            return new NewsContentDAL().GetItem(id);
        }

        public OperationResult Add(NewsContentModel entity)
        {
            int row = new NewsContentDAL().Add(entity);
            if (row > 0)
            {
                return new OperationResult(OperationResultType.Success,"操作成功");
            }

            return new OperationResult(OperationResultType.Success, "操作失败,请重试");
        }

        public OperationResult Edit(NewsContentModel entity)
        {
            int row = new NewsContentDAL().Edit(entity);
            if (row > 0)
            {
                return new OperationResult(OperationResultType.Success, "操作成功");
            }

            return new OperationResult(OperationResultType.Success, "操作失败,请重试");
        }

        public int Delete(int id)
        {
            return new NewsContentDAL().Delete(id);
        }
    }
}
