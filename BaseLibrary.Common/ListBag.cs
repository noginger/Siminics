using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseLibrary.Common
{
    /// <summary>
    /// List包
    /// </summary>
    /// <typeparam name="TRecordCount">总记录数</typeparam>
    /// <typeparam name="TList">数据集合</typeparam>
    public class ListBag<TRecordCount, TList>
    {
        /// <summary>
        /// 记录总数
        /// </summary>
        public TRecordCount RecordCount { get; set; }

        /// <summary>
        /// 数据集合
        /// </summary>
        public TList DataSource { get; set; }

        /// <summary>
        /// 初始化键值对
        /// </summary>
        /// <param name="recordCount">键</param>
        /// <param name="list">值</param>
        public ListBag(TRecordCount recordCount, TList list)
        {
            RecordCount = recordCount;
            DataSource = list;
        }
    }
}
