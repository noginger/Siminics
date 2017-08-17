using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Cee.Tools.Cache
{
    public class CacheEnum
    {
        /// <summary>
        /// 设置缓存保存时间
        /// </summary>
        public enum TimeType
        {
            /// <summary>
            /// 分钟
            /// </summary>
            Minutes = 1,

            /// <summary>
            /// 小时
            /// </summary>
            Hours= 2,

            /// <summary>
            /// 天
            /// </summary>
            Days = 3,

            /// <summary>
            /// 月
            /// </summary>
            Month = 4
        }
    }
}
