using System; 
using System.Web;
using System.Web.Caching;
using System.Collections;

namespace Cee.Tools.Cache
{
    /// <summary>
    /// 缓存操作类
    /// </summary>
    public sealed class CacheUtils
    {
        #region 将目标对象存储到缓存中

        #region 重载1
        /// <summary>
        /// 将目标对象存储到缓存中
        /// </summary>
        /// <typeparam name="T">目标对象的类型</typeparam>
        /// <param name="key">缓存项的键名</param>
        /// <param name="target">目标对象</param>
        public static void SetCache<T>(string key, T target)
        {
            HttpRuntime.Cache.Insert(key, target);
        }
        #endregion

        #region 依赖重载
        /// <summary>
        /// 写入缓存【文件依赖】
        /// </summary>
        /// <typeparam name="T">目标对象的类型</typeparam>
        /// <param name="key">缓存项的键名</param>
        /// <param name="target">目标对象</param>
        /// <param name="dependencyFilePath">依赖的文件绝对路径,当该文件更改时,则将该项移出缓存</param>
        public static void SetCache<T>(string key, T target, string dependencyFilePath)
        {
            //创建缓存依赖
            CacheDependency dependency = new CacheDependency(dependencyFilePath);
            HttpRuntime.Cache.Insert(key, target, dependency);
        }

        /// <summary>
        /// 写入缓存【数据库依赖】
        /// </summary>
        public static void SetCacheDependency<T>(string key, T target, SqlCacheDependency dependency)
        {
            HttpRuntime.Cache.Insert(key, target, dependency, System.Web.Caching.Cache.NoAbsoluteExpiration, System.Web.Caching.Cache.NoSlidingExpiration);
        }
        #endregion

        #region 重载3
        /// <summary>
        /// 写入缓存【设置小时】
        /// </summary>
        public static void SetCache<T>(string key, T target, CacheEnum.TimeType time,int number)
        {
            DateTime dtm;
            switch (time)
            {
                case CacheEnum.TimeType.Minutes:
                    dtm = DateTime.Now.AddMinutes(number);
                    break;
                case CacheEnum.TimeType.Hours:
                    dtm = DateTime.Now.AddHours(number);
                    break;
                case CacheEnum.TimeType.Days:
                    dtm = DateTime.Now.AddDays(number);
                    break;
                case CacheEnum.TimeType.Month:
                    dtm = DateTime.Now.AddMonths(number);
                    break;
                default:
                    dtm = DateTime.Now.AddMinutes(number);
                    break;
            }
            HttpRuntime.Cache.Insert(key, target, null, dtm, System.Web.Caching.Cache.NoSlidingExpiration, CacheItemPriority.Default, null);
        }
        /// <summary>
        /// 写入缓存【自上次访问后 ? 分钟过期】
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>ss
        /// <param name="target"></param>
        /// <param name="minute"></param>
        public static void SaveCacheMinuteSliding<T>(string key, T target, int minute)
        {
            HttpRuntime.Cache.Insert(key, target, null, DateTime.MaxValue, TimeSpan.FromMinutes(minute));
        }
        #endregion

        #endregion

        #region 获取缓存中的目标对象
        /// <summary>
        /// 获取缓存中的目标对象
        /// </summary>
        /// <typeparam name="T">目标对象的类型</typeparam>
        /// <param name="key">缓存项的键名</param>
        public static T GetCache<T>(string key)
        {
            //获取缓存中的目标对象
            return ConvertHelper.ConvertTo<T>(HttpRuntime.Cache.Get(key));
        }
        #endregion

        #region 删除指定缓存
        /// <summary>
        /// 删除指定缓存
        /// </summary>
        /// <param name="key"></param>
        public static void Remove(string key)
        {
            //if (null != HttpRuntime.Cache[key])
            HttpRuntime.Cache.Remove(key);  
        }

        /// <summary>
        /// 删除指定缓存
        /// </summary>
        /// <param name="keys"></param>
        public static void RemoveAll(ICollection keys)
        {
            foreach (object keyItem in keys)
            {
                string key = (string)keyItem;
                HttpRuntime.Cache.Remove(key);
            }
        }


        #endregion

        #region 检测目标对象是否存储在缓存中
        /// <summary>
        /// 检测目标对象是否存储在缓存中
        /// </summary>
        /// <param name="key">缓存项的键名</param>
        public static bool Contains(string key)
        {
            object entry = HttpRuntime.Cache.Get(key);
            return entry != null;
        }
        #endregion

    }
}
