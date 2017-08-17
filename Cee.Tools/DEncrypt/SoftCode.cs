using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cee.Tools.DEncrypt
{
    public class SoftCode
    {


        #region 获取软件注册码

        /// <summary>
        /// 获取软件注册码
        /// </summary>
        /// <param name="userid">用户Id</param>
        /// <param name="companyname">公司名称</param>
        /// <returns>注册码</returns>
        public static string GetSoftRegCode(int userid, string companyname)
        {
            //软件序号
            string sn = "Era" + userid.ToString();

            DateTime RegDate = DateTime.Parse("2011-1-1");
            DateTime EndDate = RegDate.AddYears(200);

            return GetSoftRegCode(sn, RegDate, string.Empty, companyname, 10, EndDate);
        }

        /// <summary>
        /// 软件注册码算法
        /// </summary>
        /// <param name="sn">软件序号</param>
        /// <param name="regDate">注册时间</param>
        /// <param name="checkCode">验证码</param>
        /// <param name="companyName">公司名称</param>
        /// <param name="userCount">用户数</param>
        /// <param name="endDate">结束时间</param>
        /// <returns>注册码</returns>
        public static string GetSoftRegCode(string sn, DateTime regDate, string checkCode, string companyName, int userCount, DateTime endDate)
        {
            sn += regDate.Year.ToString() + "-" + regDate.Month.ToString() + "-" + regDate.Day.ToString();
            if (checkCode.Length >= 5)
                sn += checkCode.Substring(0, 5);
            sn += System.Web.HttpUtility.UrlEncode(companyName, System.Text.Encoding.GetEncoding("gbk")).ToUpper();
            sn += userCount.ToString();
            if (checkCode.Length >= 5)
                sn += checkCode.Substring(5);
            sn += endDate.Year.ToString() + "-" + endDate.Month.ToString() + "-" + endDate.Day.ToString();

            //将sn倒序
            string cdKey = string.Empty;
            for (int i = 0; i < sn.Length; i++)
                cdKey = sn[i] + cdKey;
            cdKey = System.Web.Security.FormsAuthentication.HashPasswordForStoringInConfigFile(cdKey, "MD5").Substring(3, 16);

            cdKey = cdKey.Substring(0, 4) + "-" + cdKey.Substring(4, 4) + "-" + cdKey.Substring(8, 4) + "-" + cdKey.Substring(12, 4);

            cdKey = cdKey.ToUpper();

            return cdKey;
        }
        #endregion

        #region 验证注册码V3版
        /// <summary>
        /// 软件注册码算法V3版
        /// </summary>
        /// <param name="userId">用户编号</param>
        /// <param name="companyName">公司名称</param>
        /// <returns>注册码</returns>
        public static string GetSoftRegCodeV3(int userId, string companyName)
        {
            string proName = "ypzd";
            string sn = "Era" + userId.ToString();

            //组合产品名、序号、企业名称
            sn = proName + sn + System.Web.HttpUtility.UrlEncode(companyName, System.Text.Encoding.GetEncoding("gbk")).ToUpper();

            string cdKey = string.Empty;

            //MD5加密取前16位
            cdKey = System.Web.Security.FormsAuthentication.HashPasswordForStoringInConfigFile(sn, "MD5").Substring(0, 16);

            //每四位加一个空格
            cdKey = cdKey.Substring(0, 4) + "-" + cdKey.Substring(4, 4) + "-" + cdKey.Substring(8, 4) + "-" + cdKey.Substring(12, 4);

            //cdKey = cdKey.ToUpper();

            return cdKey;
        }
        #endregion
    }
}
