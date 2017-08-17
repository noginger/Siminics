using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using Cee.Tools.Cache;
using Cee.Tools.Web;

namespace Cee.Tools.Config
{
    public class Loader
    {
        /// <summary>
        /// load the current config 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static T LoadConfig<T>() where T : class
        {
            return LoadConfig<T>(null);
        }

        /// <summary>
        /// load the current config
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="fileName"></param>
        /// <returns></returns>
        public static T LoadConfig<T>(string fileName) where T : class
        {
            if (string.IsNullOrEmpty(fileName))
                fileName = WebUtils.GetMapPath(string.Concat(typeof(T).Name, ".config"));

            string cacheKey = fileName;

            T obj = CacheUtils.GetCache<T>(cacheKey);

            if (obj == null)
            {
                obj = LoadFromXml<T>(fileName);

                if (obj != null)
                    CacheUtils.SetCache<T>(cacheKey, obj, fileName);
            }

            return obj;
        }

        /// <summary>
        /// load the current config and serializer
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="fileName"></param>
        /// <returns></returns>
        public static T LoadFromXml<T>(string fileName) where T : class
        {
            FileStream fs = null;
            try
            {
                XmlSerializer serializer = new XmlSerializer(typeof(T));
                fs = new FileStream(fileName, FileMode.Open, FileAccess.Read);
                return (T)serializer.Deserialize(fs);
            }
            catch (Exception ex)
            {
                return null;
            }
            finally
            {
                if (fs != null)
                {
                    fs.Close();
                }
            }
        }

        /// <summary>
        /// Config格式化为String对象
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="instance"></param>
        /// <returns></returns>
        public static string ToStringXmlMessage<T>(T instance)
        {
            StringWriter sr = null;
            try
            {
                XmlSerializer xr = new XmlSerializer(typeof(T));
                StringBuilder sb = new StringBuilder();

                sr = new StringWriter(sb);
                xr.Serialize(sr, instance);

                return (sb.ToString());
            }
            catch (Exception ex)
            {
                return string.Empty;
            }
            finally
            {
                if (sr != null)
                {
                    sr.Close();
                }
            }
        }
    }
}
