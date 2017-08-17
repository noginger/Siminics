using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cee.Tools.Basic
{
    /// <summary>
    /// 正则表达式
    /// </summary>
    public class RegexPatterns
    {
        /// <summary>
        /// 不为空 除制表符、空格、换行符
        /// </summary>
        public const string UnNullableOrEmtpy = @"\S";
        /// <summary>
        /// 不为空 除制表符、空格、换行符
        /// </summary>
        public const string UnNullableOrEmtpyPassword = @"\S{6,20}";
        /// <summary>
        /// 按字母顺序A-Z/a-z.
        /// </summary>
        public const string Alpha = @"^[a-zA-Z]*$";

        /// <summary>
        /// 按大写字母顺序A-Z.
        /// </summary>
        public const string AlphaUpperCase = @"^[A-Z]*$";

        /// <summary>
        /// 按大写字母顺序a-z.
        /// </summary>
        public const string AlphaLowerCase = @"^[a-z]*$";

        /// <summary>
        /// 按数字、字母顺序
        /// </summary>
        public const string AlphaNumeric = @"^[a-zA-Z0-9]*$";

        /// <summary>
        /// 按数字、字母顺序和空格.
        /// </summary>
        public const string AlphaNumericSpace = @"^[a-zA-Z0-9 ]*$";

        /// <summary>
        /// a-zA-Z0-9 \-.
        /// </summary>
        public const string AlphaNumericSpaceDash = @"^[a-zA-Z0-9 \-]*$";

        /// <summary>
        /// a-zA-Z0-9 \-_.
        /// </summary>
        public const string AlphaNumericSpaceDashUnderscore = @"^[a-zA-Z0-9 \-_]*$";

        /// <summary>
        /// a-zA-Z0-9\. \-_.
        /// </summary>
        public const string AlphaNumericSpaceDashUnderscorePeriod = @"^[a-zA-Z0-9\. \-_]*$";

        /// <summary>
        /// 正整数
        /// </summary>
        public const string intege1 = @"^[1-9]\d*$";

        /// <summary>
        /// 负整数
        /// </summary>
        public const string intege2 = @"^-[1-9]\d*$";

        /// <summary>
        /// 正整数或0
        /// </summary>
        public const string intege3 = @"^\d+$";

        /// <summary>
        /// Numeric regex.
        /// </summary>
        public const string Numeric = @"^\-?[0-9]*\.?[0-9]*$";

        /// <summary>
        /// Numeric regex.
        /// </summary>
        public const string Integer = @"^\-?[0-9]*$";

        /// <summary>
        /// Ssn regex.
        /// </summary>
        public const string SocialSecurity = @"\d{3}[-]?\d{2}[-]?\d{4}";

        /// <summary>
        /// 浮点数 保留两位小数
        /// </summary>
        public const string decimal2 = @"^\-?\d{1,8}(\.\d{1,2})?$";

        /// <summary>
        /// 浮点数 保留两位小数 不包括0
        /// </summary>
        public const string decimal3 = @"^\-?(([1-9]\d{0,7}(\.\d{1,2})?)|([0]\.\d{1,2}))$";

        /// <summary>
        /// 浮点数 保留两位小数 >0
        /// </summary>
        public const string decimal4 = @"^(([1-9]\d{0,7}(\.\d{1,2})?)|([0]\.[1-9])|([0]\.[0][1-9])|([0]\.[0-9][1-9]))$";

        /// <summary>
        /// 浮点数 保留两位小数
        /// </summary>
        public const string decimal5 = @"^\d{1,8}(\.\d{1,2})?$";

        /// <summary>
        ///邮箱 regex.
        /// </summary>
        public const string Email = @"^([0-9a-zA-Z]+[-._+&])*[0-9a-zA-Z]+@([-0-9a-zA-Z]+[.])+[a-zA-Z]{2,6}$";

        /// <summary>
        /// Url地址 regex.
        /// </summary>
        public const string Url = @"^^(ht|f)tp(s?)\:\/\/[0-9a-zA-Z]([-.\w]*[0-9a-zA-Z])*(:(0-9)*)*(\/?)([a-zA-Z0-9\-\.\?\,\'\/\\\+&%\$#_=]*)?$";

        /// <summary>
        /// 邮政编码 regex.
        /// </summary>
        public const string ZipCode = @"\d{6}";

        /// <summary>
        /// 电话号码的函数(包括验证国内区号,国际区号,分机号)
        /// </summary>
        public const string Tel = @"^(([0\+]\d{2,3}-)?(0\d{2,3})-)?(\d{7,8})(-(\d{3,}))?$";

        /// <summary>
        /// 移动电话
        /// </summary>
        public const string Mobile = "^(13[0-9]{9}|15[012356789][0-9]{8}|18[0-9][0-9]{8}|147[0-9]{8}$)";

        /// <summary>
        /// IP地址 regex.
        /// </summary>
        public const string Ip = @"^((2[0-4]\d|25[0-5]|[01]?\d\d?)\.){3}(2[0-4]\d|25[0-5]|[01]?\d\d?)$";

        /// <summary>
        /// 身份证 regex
        /// </summary>
        public const string IdCard = @"^(11|12|13|14|15|21|22|23|31|32|33|34|35|36|37|41|42|43|44|45|46|50|51|52|53|54|61|62|63|64|65|71|81|82|91)(\d{13}|\d{15}[\dx])$";

        /// <summary>
        /// 中文 regex
        /// </summary>
        public const string Chinese = "^[\\u4E00-\\u9FA5\\uF900-\\uFA2D]+$";
    }
}
