using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using Cee.Tools.Web;

namespace BaseLibrary.Common
{
    public class Pager
    {
        public static string CreatePreviousPage(int pageSize, int totalCount)
        {
            int pageIndex = WebUtils.GetQueryInt("p", 1);

            if (pageIndex <= 0)
                pageIndex = 1;
            if (pageSize <= 0)
                pageSize = 15;

            Hashtable hashtable = new Hashtable();

            string strUrl = HttpContext.Current.Request.Url.ToString();
            string strUrlPath = HttpContext.Current.Request.Url.AbsolutePath;
            if (strUrl.IndexOf("?", System.StringComparison.Ordinal) >= 0)
            {
                string queryValue = strUrl.Substring(strUrl.IndexOf("?", System.StringComparison.Ordinal) + 1);
                strUrl = strUrl.Substring(0, strUrl.IndexOf("?", System.StringComparison.Ordinal));

                string[] arrQuery = queryValue.Split('&');

                foreach (string qry in arrQuery)
                {
                    if (qry.Trim().ToLower().StartsWith("p="))
                        continue;

                    string[] arrValue = qry.Trim().Split('=');

                    if (arrValue.Length == 1)
                        hashtable.Add(qry.Trim(), "");
                    else
                    {
                        hashtable.Add(arrValue[0], qry.Substring(arrValue[0].Length + 1));
                    }
                }
            }

            strUrl += "?";
            if (hashtable.Count > 0)
            {
                strUrl = hashtable.Cast<DictionaryEntry>().Aggregate(strUrl, (current, dr) => current + (dr.Key + "=" + dr.Value + "&"));
            }

            strUrl += "p=";

            StringBuilder sb = new StringBuilder();

            int nPageCount = (int)Math.Ceiling((double)totalCount / (double)pageSize);

            if (pageIndex > nPageCount && nPageCount > 0)
                HttpContext.Current.Response.Redirect(strUrl + nPageCount.ToString(), true);

            if (totalCount > 0)
            {
                sb.Append(pageIndex > 1
                              ? string.Format("<a href=\"{0}\" class=\"pleft\">上一页</a>",
                                              strUrl + Convert.ToString(pageIndex - 1))
                              : "<a class=\"pre\">上一页</a>");

                sb.Append(pageIndex < nPageCount
                              ? string.Format("<a href=\"{0}\" class=\"pright\">下一页</a>",
                                              strUrl + Convert.ToString(pageIndex + 1))
                              : "<a class=\"next\">下一页</a>");
            }


            return sb.ToString();
        }
    }
}
