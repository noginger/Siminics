using System;
using System.Collections;
using System.Linq;
using System.Text;
using System.Web;
using Cee.Tools.Web;

namespace BaseLibrary.Common
{
    public static class ManageMvcPaging
    {
        public static string Create(int pageSize, int totalCount, int pageGroup, string className, bool blnJump)
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
                    {
                        if (!hashtable.ContainsKey(qry.Trim()))
                            hashtable.Add(qry.Trim(), "");

                    }
                    else
                    {
                        if (!hashtable.ContainsKey(arrValue[0].Trim()))
                            hashtable.Add(arrValue[0].Trim(), qry.Substring(arrValue[0].Length + 1));
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
                if (blnJump)
                {
                    sb.Append(string.Format("<section class=\"{0}info\" id=\"pages\">", className));
                    sb.Append(string.Format("<span>共{0}页  到第</span>", nPageCount));
                    sb.Append(string.Format("<span><input type=\"text\" class=\"pageboxa\" value=\"{0}\" size=\"5\" /></span>", pageIndex));
                    sb.Append(string.Format("<span>页</span>"));
                    sb.Append(string.Format("<span><span class=\"gray1\">" + totalCount + "条信息</span></span>"));
                    sb.Append("</section>");
                }

                sb.Append(string.Format("<section class=\"{0}\"  id=\"pages\"><ul>", className));

                sb.Append(pageIndex > 1
                              ? string.Format("<li><a href=\"{0}\" class=\"pleft\">上一页</a></li>",
                                              strUrl + Convert.ToString(pageIndex - 1))
                              : string.Empty);

                int nStart = pageIndex - pageGroup / 2;
                if (nStart < 1)
                    nStart = 1;

                if (nStart + pageGroup > nPageCount)
                    nStart = nPageCount - pageGroup + 1;

                if (nStart < 1)
                    nStart = 1;

                if (pageIndex < pageGroup)
                {
                    for (int i = 1; i < nStart; i++)
                    {
                        sb.Append(string.Format("<li><a href=\"{0}\">{1}</a></li>", strUrl + i, i));
                    }
                }

                if (pageIndex > pageGroup - 1 && nPageCount > pageGroup)
                {
                    sb.Append(string.Format("<li><a href=\"{0}\">1</a></li>", strUrl + 1));
                    sb.Append("<li><a href=\"javascript:;\">…</a></li>");
                }

                for (int i = nStart; i < nStart + pageGroup && i <= nPageCount; i++)
                {
                    sb.Append(i == pageIndex
                                  ? string.Format("<li class=\"thisclass\"><span>{0}</span></li>", i)
                                  : string.Format("<li><a href=\"{0}\">{1}</a></li>", strUrl + i, i));
                }
                if (nPageCount > pageGroup)
                {
                    if (pageIndex <= nPageCount - pageGroup && pageIndex + pageGroup / 2 - 1 < nPageCount ||
                        pageIndex > nPageCount - pageGroup && pageIndex + pageGroup / 2 - 1 < nPageCount)
                    {
                        sb.Append("<li><a href=\"javascript:;\">…</a></li>");
                        sb.Append(string.Format("<li><a href=\"{0}\">{1}</a></li>", strUrl + nPageCount, nPageCount));
                    }
                }

                sb.Append(pageIndex < nPageCount
                              ? string.Format("<li><a href=\"{0}\" class=\"pright\">下一页</a><li>",
                                              strUrl + Convert.ToString(pageIndex + 1))
                              : "");

                sb.Append("</ul><div class=\"clr\"></div></section>");
            }


            return sb.ToString();
        }

        public static string Create(int pageSize, int totalCount, int pageGroup, bool blnJump)
        {
            return Create(pageSize, totalCount, pageGroup, "pages", blnJump);
        }

        public static string Create(int pageSize, int totalCount, bool blnJump)
        {
            return Create(pageSize, totalCount, 5, "pages", blnJump);
        }

        public static string Create(int totalCount, bool blnJump)
        {
            return Create(15, totalCount, blnJump);
        }
    }
}
