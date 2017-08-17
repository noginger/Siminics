using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Cee.Tools
{
    /// <summary>
    /// 数据类型转换公共类
    /// </summary>
    public class ConvertHelper
    {
        #region 将数据转换为指定T类型

        /// <summary>
        /// 将数据转换为指定T类型
        /// </summary>
        /// <typeparam name="T">Type of object whose type is to be converted.</typeparam>
        /// <param name="input">Object whose type is to be converted.</param>
        /// <param name="defaultValue">default value</param>
        /// <returns>Type of converted object.</returns>
        public static T ConvertTo<T>(object input, T defaultValue)
        {
            object result = default(T);
            if (input == null || input == DBNull.Value) return (T)result;

            if (typeof(T) == typeof(int))
                result = System.Convert.ToInt32(input);
            else if (typeof(T) == typeof(long))
                result = System.Convert.ToInt64(input);
            else if (typeof(T) == typeof(string))
                result = System.Convert.ToString(input);
            else if (typeof(T) == typeof(bool))
                result = System.Convert.ToBoolean(input);
            else if (typeof(T) == typeof(double))
                result = System.Convert.ToDouble(input);
            else if (typeof(T) == typeof(DateTime))
                result = System.Convert.ToDateTime(input);
            else
                result = defaultValue;

            return (T)result;
        }

        /// <summary>
        /// 将数据转换为指定T类型
        /// </summary>
        /// <typeparam name="T">转换的目标类型</typeparam>
        /// <param name="data">转换的数据</param>
        public static T ConvertTo<T>(object data)
        {
            //如果数据为空，则返回
            if (data == null)
            {
                return default(T);
            }

            try
            {
                //如果数据是T类型，则直接转换
                if (data is T)
                {
                    return (T)data;
                }

                //如果目标类型是枚举
                if (typeof(T).BaseType == typeof(Enum))
                {
                    return Types.EnumTypes.GetInstance<T>(data);
                }

                //如果数据实现了IConvertible接口，则转换类型
                if (data is IConvertible)
                {
                    return (T)Convert.ChangeType(data, typeof(T));
                }
                else
                {
                    return default(T);
                }
            }
            catch
            {
                return default(T);
            }
        }

        #endregion

        #region 检测数据是否可转换为指定T类型

        /// <summary>
        /// 检测数据是否可转换为指定T类型
        /// </summary>
        /// <param name="val">The value to test for conversion to the type
        /// associated with the property</param>
        /// <returns>True if the object can be converted to a type.</returns>
        public static bool IsConvertTo<T>(string val)
        {
            return IsConvertTo(typeof(T), val);
        }

        #endregion

        #region 检测数据是否可转换为指定类型

        /// <summary>
        /// 检测数据是否可转换为指定类型
        /// </summary>
        /// <param name="type">The property represnting the type to convert 
        /// val to</param>
        /// <param name="val">The value to test for conversion to the type
        /// associated with the property</param>
        /// <returns>True if the object can be converted to a type.</returns>
        public static bool IsConvertTo(Type type, string val)
        {
            // Data could be passed as string value.
            // Try to change type to check type safety.                    
            try
            {
                if (type == typeof(int))
                {
                    int result = 0;
                    if (int.TryParse(val, out result)) return true;

                    return false;
                }
                else if (type == typeof(string))
                {
                    return true;
                }
                else if (type == typeof(double))
                {
                    double d = 0;
                    if (double.TryParse(val, out d)) return true;

                    return false;
                }
                else if (type == typeof(long))
                {
                    long l = 0;
                    if (long.TryParse(val, out l)) return true;

                    return false;
                }
                else if (type == typeof(float))
                {
                    float f = 0;
                    if (float.TryParse(val, out f)) return true;

                    return false;
                }
                else if (type == typeof(bool))
                {
                    bool b = false;
                    if (bool.TryParse(val, out b)) return true;

                    return false;
                }
                else if (type == typeof(DateTime))
                {
                    DateTime d = DateTime.MinValue;
                    if (DateTime.TryParse(val, out d)) return true;

                    return false;
                }
                else if (type.BaseType == typeof(Enum))
                {
                    Enum.Parse(type, val, true);
                }
            }
            catch (Exception)
            {
                return false;
            }

            //Conversion worked.
            return true;
        }

        #endregion

        #region string型转换为bool型

        /// <summary>
        /// string型转换为bool型
        /// </summary>
        /// <param name="strValue">要转换的字符串</param>
        /// <returns>转换后的bool类型结果</returns>
        public static bool StrToBool(string strValue)
        {
            bool blnResult = false;

            if (!string.IsNullOrEmpty(strValue))
            {
                strValue = strValue.Trim();
                blnResult =
                    (((System.String.Compare(strValue, "true", System.StringComparison.OrdinalIgnoreCase) == 0)
                      || (System.String.Compare(strValue, "yes", System.StringComparison.OrdinalIgnoreCase) == 0))
                     || (System.String.Compare(strValue, "1", System.StringComparison.OrdinalIgnoreCase) == 0));
            }

            return blnResult;
        }

        #endregion

        #region string型转换为DateTime型

        /// <summary>
        /// string型转换为时间型
        /// </summary>
        /// <param name="strValue">要转换的字符串</param>
        /// <param name="defaultValue">默认值</param>
        /// <returns>转换后的时间类型结果</returns>
        public static DateTime StrToDateTime(string strValue, DateTime defaultValue)
        {
            if (string.IsNullOrEmpty(strValue))
            {
                return defaultValue;
            }

            DateTime result;

            if (!DateTime.TryParse(strValue, out result))
            {
                result = defaultValue;
            }
            return result;
        }

        #endregion

        #region long型转换为DateTime型

        /// <summary>
        /// long型转换为DateTime型
        /// </summary>
        /// <param name="lngValue">传入的long值</param>
        /// <returns>DateTime值</returns>
        public static DateTime LongToDateTime(long lngValue)
        {
            DateTime nowDate;
            DateTime.TryParse("1970-01-01", out nowDate);
            double addDays = (double)lngValue / (double)(24 * 3600);

            return nowDate.AddDays((double)addDays);
        }

        #endregion

        #region object型转换为decimal型
        /// <summary>
        /// object型转换为decimal型
        /// </summary>
        /// <param name="strValue">要转换的字符串</param>
        /// <returns>decimal值</returns>
        public static decimal StrToDecimal(object strValue)
        {
            if (!Convert.IsDBNull(strValue) && !object.Equals(strValue, null))
            {
                return StrToDecimal(strValue.ToString());
            }
            return 0M;
        }
        #endregion

        #region string型转换为decimal型

        /// <summary>
        /// string型转换为decimal型
        /// </summary>
        /// <param name="strValue">要转换的字符串</param>
        /// <returns>decimal值</returns>
        public static decimal StrToDecimal(string strValue)
        {
            decimal num;
            decimal.TryParse(strValue, out num);
            return num;
        }

        #endregion

        #region string型转换为decimal型
        /// <summary>
        /// string型转换为decimal型
        /// </summary>
        /// <param name="strValue">要转换的字符串</param>
        /// <param name="defaultValue">缺省值</param>
        /// <returns>decimal值</returns>
        public static decimal StrToDecimal(string strValue, decimal defaultValue)
        {
            decimal num;
            return decimal.TryParse(strValue, out num) ? num : defaultValue;
        }

        #endregion

        #region object型转换为double型
        /// <summary>
        /// string型转换为double型
        /// </summary>
        /// <param name="strValue">要转换的字符串</param>
        /// <returns>double值</returns>
        public static double StrToDouble(object strValue)
        {
            if (!Convert.IsDBNull(strValue) && !object.Equals(strValue, null))
            {
                return StrToDouble(strValue.ToString());
            }
            return 0.0;
        }
        #endregion

        #region string型转换为double型
        /// <summary>
        /// string型转换为double型
        /// </summary>
        /// <param name="strValue">要转换的字符串</param>
        /// <returns>double值</returns>
        public static double StrToDouble(string strValue)
        {
            double num;
            double.TryParse(strValue, out num);
            return num;
        }
        #endregion

        #region string型转换为double型
        /// <summary>
        /// string型转换为decimal型
        /// </summary>
        /// <param name="strValue">要转换的字符串</param>
        /// <param name="defaultValue">缺省值</param>
        /// <returns>decimal值</returns>
        public static double StrToDouble(string strValue, double defaultValue)
        {
            double num;
            return double.TryParse(strValue, out num) ? num : defaultValue;
        }

        #endregion

        #region string型转换为long型
        /// <summary>
        /// string型转换为long型
        /// </summary>
        /// <param name="strValue">要转换的字符串</param>
        /// <param name="defaultValue">缺省值</param>
        /// <returns>decimal值</returns>
        public static long StrToLong(string strValue, long defaultValue)
        {
            long num;
            return long.TryParse(strValue, out num) ? num : defaultValue;
        }

        #endregion

        #region string型转换为float型

        /// <summary>
        /// string型转换为float型
        /// </summary>
        /// <param name="strValue">要转换的字符串</param>
        /// <param name="defaultValue">默认值</param>
        /// <returns>float值</returns>
        public static float StrToFloat(object strValue, float defaultValue)
        {
            if ((strValue == null) || (strValue.ToString().Length > 10))
            {
                return defaultValue;
            }

            float result = defaultValue;
            bool isFloat = new Regex(@"^([-]|[0-9])[0-9]*(\.\w*)?$").IsMatch(strValue.ToString());
            if (isFloat)
            {
                result = Convert.ToSingle(strValue);
            }
            return result;
        }
        #endregion

        #region string型转换为int型

        /// <summary>
        /// string型转换为int型
        /// </summary>
        /// <param name="strValue">要转换的字符串</param>
        /// <param name="defValue">缺省值</param>
        /// <returns>转换后的int类型结果</returns>
        public static int StrToInt(object strValue, int defValue)
        {
            if ((strValue == null) || (strValue.ToString() == string.Empty) || (strValue.ToString().Length > 10))
            {
                return defValue;
            }

            string val = strValue.ToString();
            string firstletter = val[0].ToString();

            if (val.Length == 10 && Validation.IsNumber(firstletter) && int.Parse(firstletter) > 1)
            {
                return defValue;
            }
            else if (val.Length == 10 && !Validation.IsNumber(firstletter))
            {
                return defValue;
            }

            int intValue = defValue;
            if (strValue != null)
            {
                bool IsInt = new Regex(@"^([-]|[0-9])[0-9]*$").IsMatch(strValue.ToString());
                if (IsInt)
                {
                    intValue = Convert.ToInt32(strValue);
                }
            }

            return intValue;
        }

        #endregion

        #region Object型转换为货币
        /// <summary>
        /// Object型转换为货币
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static string DecimalToMoney(decimal obj)
        {
            if (obj < 0)
            {
                return "-" + Math.Abs(obj).ToString("C");
            }
            else
            {
                return obj.ToString("C");
            }
        }
        #endregion

        #region Object型转换为数字
        /// <summary>
        /// Object型转换为数字
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static string DecimalToNumber(decimal obj)
        {
            return obj.ToString("N");
        }
        #endregion
    }
}
