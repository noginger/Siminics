using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Cee.Tools.Basic;

namespace Cee.Tools
{
    public static partial class Validation
    {
        #region 验证是否为整数
        /// <summary>
        /// 验证是否为整数
        /// </summary>
        /// <param name="number">要验证的数字</param>        
        public static bool IsInt(string number)
        {
            if (string.IsNullOrEmpty(number))
                return true;

            return IsMatch(number, RegexPatterns.Integer);
        }
        #endregion

        #region 验证是否为数字
        /// <summary>
        /// 验证是否为数字
        /// </summary>
        /// <param name="number">要验证的数字</param>        
        public static bool IsNumber(string number)
        {
            if (string.IsNullOrEmpty(number))
                return true;

            return IsMatch(number, RegexPatterns.Numeric);
        }
        #endregion

        #region 验证是否为IP
        /// <summary>
        /// 验证是否为IP
        /// </summary>
        /// <param name="ipAddress">要验证的Ip</param>        
        public static bool IsIP(string ipAddress)
        {
            if (string.IsNullOrEmpty(ipAddress))
                return true;

            //验证
            return IsMatch(ipAddress, RegexPatterns.Ip);
        }
        #endregion

        #region  验证Email是否合法
        /// <summary>
        /// 验证Email是否合法
        /// </summary>
        /// <param name="email">要验证的Email</param>
        public static bool IsEmail(string email)
        {
            if (string.IsNullOrEmpty(email))
                return true;

            email = email.Trim();

            return IsMatch(email, RegexPatterns.Email);
        }
        #endregion

        #region 验证身份证是否合法
        /// <summary>
        /// 验证身份证是否合法
        /// </summary>
        /// <param name="idCard">要验证的身份证</param>        
        public static bool IsIdCard(string idCard)
        {
            //如果为空，认为验证合格
            if (string.IsNullOrEmpty(idCard))
                return true;
            //清除要验证字符串中的空格
            idCard = idCard.Trim();

            //验证
            return IsMatch(idCard, RegexPatterns.IdCard);
        }
        #endregion
    }
}
