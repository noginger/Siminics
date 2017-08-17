using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cee.Tools.Types
{
    public static class ArrayTypes
    {
        public static string Join<T>(this IList<T> array, string joinstr = ",")
        {
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            for (int i = 0; i < array.Count; i++)
            {
                sb.Append(array[i].ToString());
                if (i < array.Count - 1)
                {
                    sb.Append(joinstr);
                }
            }
            return sb.ToString();
        }

        /// <summary>
        /// 合并2个数组
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        public static string[] MergeArray(string[] a, string[] b)
        {
            ArrayList ary = new ArrayList();
            foreach (string s1 in a)
            {
                ary.Add(s1);
            }
            foreach (string s2 in from s2 in b let flag = a.All(t => t != s2) where flag select s2)
            {
                ary.Add(s2);
            }
            var c1 = (string[])ary.ToArray(typeof(string));

            return c1;
        }

        /// <summary>
        /// 过滤重复数组
        /// </summary>
        /// <param name="arys"></param>
        /// <returns></returns>
        public static int[] FilterArray(int[] arys)
        {
            ArrayList arrayList = new ArrayList();

            foreach (int t in arys.Where(t => !arrayList.Contains(t)))
            {
                arrayList.Add(t);
            }

            int[] result = new int[arrayList.Count];

            for (int i = 0; i < arrayList.Count; i++)
            {
                result[i] = ConvertHelper.StrToInt(arrayList[i], 0);
            }

            return result;
        }
    }
}
