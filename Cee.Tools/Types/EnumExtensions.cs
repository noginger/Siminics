
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;


namespace Cee.Tools.Types
{
    /// <summary>
    ///     枚举扩展方法类
    /// </summary>
    public static class EnumExtensions
    {
        /// <summary>
        ///     获取枚举项的Description特性的描述文字
        /// </summary>
        /// <param name="enumeration"> </param>
        /// <returns> </returns>
        public static string ToDescription(this Enum enumeration)
        {
            Type type = enumeration.GetType();
            MemberInfo[] members = type.GetMember(enumeration.CastTo<string>());
            if (members.Length > 0)
            {
                return members[0].ToDescription();
            }
            return enumeration.CastTo<string>();
        }

        /// <summary>
        ///     获取设置EnumDescription枚举项的Description特性的描述文字
        /// </summary>
        /// <param name="myEnumDescription"></param>
        /// <returns></returns>
        public static string ToDesc(this Enum myEnumDescription)
        {
            Type type = myEnumDescription.GetType();
            FieldInfo fd = type.GetField(myEnumDescription.ToString());

            if (fd == null)
                return "";

            object[] attrs = fd.GetCustomAttributes(typeof (EnumDescription), false);
            string strDescription = string.Empty;
            foreach (EnumDescription attr in attrs)
            {
                strDescription = attr.EnumDisplayText;
            }

            return strDescription;
        }
    }
}