using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cee.Tools
{
    /// <summary>
    /// 日期操作类
    /// </summary>
    public class TimeHelper
    {
        /// <summary>
        /// 日期转换为Long
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        public static long TimeToLong(DateTime date)
        {
            DateTime dtStart = TimeZone.CurrentTimeZone.ToLocalTime(new DateTime(1970, 1, 1));
            TimeSpan toNow = date.Subtract(dtStart);
            string timeStamp = toNow.Ticks.ToString();
            if (timeStamp == "0") return 0;
            timeStamp = timeStamp.Substring(0, timeStamp.Length - 7);

            return long.Parse(timeStamp);
        }

      

        /// <summary>
        /// long转换为日期
        /// </summary>
        /// <param name="timeStamp"></param>
        /// <returns></returns>
        public static DateTime LongToTime(long timeStamp)
        {
            DateTime dtStart = TimeZone.CurrentTimeZone.ToLocalTime(new DateTime(1970, 1, 1));
            long lTime = long.Parse(timeStamp.ToString() + "0000000");
            TimeSpan toNow = new TimeSpan(lTime);
            DateTime dtResult = dtStart.Add(toNow);

            return dtResult;
        }
    }
}
