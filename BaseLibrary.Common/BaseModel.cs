using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseLibrary.Common
{
    /// <summary>
    ///     数据模型基类，提供模型统一定义属性
    /// </summary>
    [Serializable]
    public abstract class BaseModel
    {
        protected BaseModel(){
        }

        //其他自定义属性
        public Dictionary<string, object> AutoProperties { get; set; }

        ///// <summary>
        ///// 创建日期
        ///// </summary>
        //public int CreateDate { get; set; }
    }

    [Serializable]
    public partial class BaseEntityModel : BaseModel
    {
        /// <summary>
        ///     版本控制标识，可用作并发处理
        /// </summary>
        [ConcurrencyCheck]
        [Timestamp]
        public byte[] Timestamp { get; set; }

        /// <summary>
        ///     记录数据集合总数
        /// </summary>
        public int RecordCount { get; set; }
    }

    public partial class BaseMvcModel : BaseModel
    {
        public virtual int Id { get; set; }
    }
}
