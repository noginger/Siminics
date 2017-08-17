using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseLibrary.Common
{
    /// <summary>
    ///     数据查询参数对象，提供给数据访问层、业务逻辑层、UI 层使用
    /// </summary>
    public class DbQueryParamters
    {
        public DbQueryParamters()
        {
            TableName = "";
            ColumnKey = "";
            ColumnFields = new string[] { };
            PageSize = 15;
            PageIndex = 1;
            Condition = string.Empty;
            Join = string.Empty;
            GroupBy = string.Empty;
            OrderBy = string.Empty;
            JoinTable = string.Empty;
            JoinKey = string.Empty;
        }
        /// <summary>
        /// 表名
        /// </summary>
        public string TableName { get; set; }

        /// <summary>
        /// 主键
        /// </summary>
        public string ColumnKey { get; set; }
        /// <summary>
        ///     表字段集合
        /// </summary>
        public string[] ColumnFields { get; set; }

        /// <summary>
        ///     页码
        /// </summary>
        public int PageSize { get; set; }

        /// <summary>
        ///     页索引
        /// </summary>
        public int PageIndex { get; set; }

        /// <summary>
        ///     查询条件
        /// </summary>
        public string Condition { get; set; }

        /// <summary>
        /// 升/降排序
        /// 格式：Order By UserId Desc,CreateDate ASC
        /// </summary>
        public string OrderBy { get; set; }

        /// <summary>
        /// 分组
        /// 格式：Group By UserId Desc,CreateDate ASC
        /// </summary>
        public string GroupBy { get; set; }

        /// <summary>
        ///     是否统计总数
        /// </summary>
        public bool IsCount { get; set; }

        public string Join { get; set; }

        /// <summary>
        /// 使用Join格式查询列表时，关联的表名
        /// </summary>
        public string JoinTable { get; set; }
        /// <summary>
        /// 使用join格式查询列表时，关联的主键，多个主键用逗号分隔
        /// </summary>
        public string JoinKey { get; set; }

        /// <summary>
        /// 是否相同主键
        /// </summary>
        public bool IsSameColumnKey { get; set; }
    }
}
