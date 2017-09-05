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
    public class LeaveMessageBL
    {
        public ListBag<int, List<LeaveMessageModel>> GetItems(DbQueryParamters paramters)
        {
            return new LeaveMessageDAL().GetItems(paramters);
        }

        public int Add(LeaveMessageModel model)
        {
            try
            {
                return new LeaveMessageDAL().Add(model);
            }
            catch (Exception ex)
            {
                return 0;
            }
        }
    }
}
