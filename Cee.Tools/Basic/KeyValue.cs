using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cee.Tools.Basic
{
    /// <summary>
    /// 键值对
    /// </summary>
    /// <typeparam name="TKey"></typeparam>
    /// <typeparam name="TValue"></typeparam>
    public class KeyValue<TKey, TValue>
    {
        /// <summary>
        /// 键
        /// </summary>
        public TKey Key { get; set; }

        /// <summary>
        /// 值
        /// </summary>
        public TValue Value { get; set; }

        /// <summary>
        /// 初始化键值对
        /// </summary>
        /// <param name="key">键</param>
        /// <param name="val">值</param>
        public KeyValue(TKey key, TValue val)
        {
            Key = key;
            Value = val;
        }

        /// <summary>
        /// 重写ToString格式
        /// </summary>
        /// <returns>String representation of this instance.</returns>
        public override string ToString()
        {
            return string.Format("{0}:{1}", Key, Value);
        }
    }
}
