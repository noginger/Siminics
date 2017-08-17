using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
/* 待调整把String 分离到Types */

namespace Cee.Tools
{
    /// <summary>
    /// String辅助类
    /// </summary>
    public class StringHelper
    {
        /// <summary>
        /// Convert To String.
        /// </summary>
        /// <param name="args">The args.</param>
        /// <returns>Converted string.</returns>
        public static string ConvertToString(object[] args)
        {
            if (args == null || args.Length == 0)
                return string.Empty;

            StringBuilder buffer = new StringBuilder();
            foreach (object arg in args.Where(arg => arg != null))
            {
                buffer.Append(arg.ToString());
            }
            return buffer.ToString();
        }

        /// <summary>
        /// Convert Bool To Yes/No
        /// </summary>
        /// <param name="args"> </param>
        /// <returns>Converted String</returns>
        public static string ConvertBoolToYesNo(bool args)
        {
            return args ? "Yes" : "No";
        }

        /// <summary>
        /// If null returns empty string.
        /// Else, returns original.
        /// </summary>
        /// <param name="text">Text to check.</param>
        /// <returns>Original text or empty string.</returns>
        public static string GetOriginalOrEmptyString(string text)
        {
            return text ?? string.Empty;
        }

        /// <summary>
        /// Returns the defaultval if the val string is null or empty.
        /// Returns the val string otherwise.
        /// </summary>
        /// <param name="val">Val string.</param>
        /// <param name="defaultVal">Default value.</param>
        /// <returns>Val string or default value if val is null or empty.</returns>
        public static string GetDefaultStringIfEmpty(string val, string defaultVal)
        {
            return string.IsNullOrEmpty(val) ? defaultVal : val;
        }

        /// <summary>
        /// Parses a delimited list of items into a string[].
        /// </summary>
        /// <param name="delimitedText">"1,2,3,4,5,6"</param>
        /// <param name="delimeter">','</param>
        /// <returns>String array with list of items in string.</returns>
        public static string[] ToStringArray(string delimitedText, char delimeter)
        {
            if (string.IsNullOrEmpty(delimitedText))
                return null;

            string[] tokens = delimitedText.Split(delimeter);
            return tokens;
        }

        /// <summary>
        /// Convert to delimited text to a dictionary.
        /// </summary>
        /// <param name="delimitedText">"1,2,3,4,5"</param>
        /// <param name="delimeter">','</param>
        /// <returns>Dictionary with delimited text and tokens.</returns>
        public static IDictionary<string, string> ToMap(string delimitedText, char delimeter)
        {
            IDictionary<string, string> items = new Dictionary<string, string>();
            string[] tokens = delimitedText.Split(delimeter);

            // Check
            foreach (string token in tokens)
            {
                items[token] = token;
            }
            return new Dictionary<string, string>(items);
        }

        #region 字符串截取
        /// <summary>
        /// 字符串截取
        /// </summary>
        /// <param name="text">要截取字符串</param>
        /// <param name="startIndex">起始位置</param>
        /// <param name="maxChars">截取长度</param>
        /// <returns>返回已截取字符串</returns>
        public static string GetSubString(string text, int startIndex, int maxChars)
        {
            return GetSubString(text, startIndex, maxChars);
        }

        /// <summary>
        /// 字符串截取
        /// </summary>
        /// <param name="text">要截取字符串</param>
        /// <param name="startIndex">起始位置</param>
        /// <param name="maxChars">截取长度</param>
        /// <param name="replaceChar">替换字符</param>
        /// <returns>返回已截取字符串</returns>
        public static string GetSubString(string text, int startIndex, int maxChars, string replaceChar)
        {
            string myResult = text;

            if (maxChars > 0 && !string.IsNullOrEmpty(text))
            {
                byte[] bsSrcString = Encoding.Default.GetBytes(text);

                //当字符串长度大于起始位置
                if (bsSrcString.Length > startIndex)
                {
                    int endIndex = bsSrcString.Length;

                    //当要截取的长度在字符串的有效长度范围内
                    if (bsSrcString.Length > (startIndex + maxChars))
                    {
                        endIndex = maxChars + startIndex;
                    }
                    else
                    {
                        //当不在有效范围内时,只取到字符串的结尾
                        maxChars = bsSrcString.Length - startIndex;
                        replaceChar = "";
                    }

                    int nRealLength = maxChars;
                    int[] anResultFlag = new int[maxChars];
                    byte[] bsResult = null;

                    int nFlag = 0;
                    for (int i = startIndex; i < endIndex; i++)
                    {
                        if (bsSrcString[i] > 127)
                        {
                            nFlag++;
                            if (nFlag == 3)
                            {
                                nFlag = 1;
                            }
                        }
                        else
                        {
                            nFlag = 0;
                        }

                        anResultFlag[i] = nFlag;
                    }

                    if ((bsSrcString[endIndex - 1] > 127) && (anResultFlag[maxChars - 1] == 1))
                    {
                        nRealLength = maxChars + 1;
                    }

                    bsResult = new byte[nRealLength];

                    Array.Copy(bsSrcString, startIndex, bsResult, 0, nRealLength);

                    myResult = Encoding.Default.GetString(bsResult);

                    myResult = myResult + replaceChar;
                }
            }

            return myResult;
        }

        #endregion

        #region 拼接字符串
        /// <summary>
        /// 拼接字符串
        /// </summary>
        /// <param name="categoryIds"></param>
        /// <returns></returns>
        public static string SpliceString(IList<int> categoryIds)
        {
            return string.Join(",", categoryIds); 
        }
        #endregion

        #region 替换JSON特殊字符
        /// <summary>
        /// 替换JSON特殊字符
        /// </summary>
        /// <param name="str">替换字符</param>
        /// <returns></returns>
        public static string ReplaceJson(string str)
        {
            CharEnumerator eS = str.GetEnumerator();
            StringBuilder sb = new StringBuilder();

            while (eS.MoveNext())
            {
                switch (eS.Current)
                {
                    case '\"':
                        sb.Append("\\\""); break;
                    case '\\':
                        sb.Append("\\\\"); break;
                    case '/':
                        sb.Append("\\/"); break;
                    case '\b':
                        sb.Append("\\b"); break;
                    case '\f':
                        sb.Append("\\f"); break;
                    case '\n':
                        sb.Append("\\n"); break;
                    case '\r':
                        sb.Append("\\r"); break;
                    case '\t':
                        sb.Append("\\t"); break;
                    default:
                        sb.Append(eS.Current); break;
                }
            }

            return sb.ToString();
        }
        #endregion

        #region 替换,恢复html中的特殊字符
        /// <summary>
        /// 替换，恢复html中的特殊字符
        /// </summary>
        /// <param name="text">需要进行替换的文本。</param>
        /// <returns>替换完的文本</returns>
        public static string HtmlEncode(string text)
        {
            text = text.Replace(">", "&gt;");
            text = text.Replace("<", "&lt;");
            text = text.Replace(" ", "&nbsp;");
            text = text.Replace(" ", "&nbsp;");
            text = text.Replace("\"", "&quot;");
            text = text.Replace("\'", "'");
            text = text.Replace("\n", "<br/> ");
            return text;
        }

        /// <summary>
        /// 恢复html中的特殊字符
        /// </summary>
        /// <param name="text">需要恢复的文本。</param>
        /// <returns>恢复好的文本。</returns>
        public static string HtmlDiscode(string text)
        {
            text = text.Replace("&gt;", ">");
            text = text.Replace("&lt;", "<");
            text = text.Replace("&nbsp;", " ");
            text = text.Replace("&nbsp;", " ");
            text = text.Replace("&quot;", "\"");
            text = text.Replace("'", "\'");
            text = text.Replace("<br/> ", "\n");
            return text;
        }
        #endregion

        #region 过滤HTML代码
        public static string ReplaceHtml(string html)
        {
            System.Text.RegularExpressions.Regex regex1 = new System.Text.RegularExpressions.Regex(@"<script[\s\S]+</script *>", System.Text.RegularExpressions.RegexOptions.IgnoreCase);
            System.Text.RegularExpressions.Regex regex2 = new System.Text.RegularExpressions.Regex(@" href *= *[\s\S]*script *:", System.Text.RegularExpressions.RegexOptions.IgnoreCase);
            System.Text.RegularExpressions.Regex regex3 = new System.Text.RegularExpressions.Regex(@" no[\s\S]*=", System.Text.RegularExpressions.RegexOptions.IgnoreCase);
            System.Text.RegularExpressions.Regex regex4 = new System.Text.RegularExpressions.Regex(@"<iframe[\s\S]+</iframe *>", System.Text.RegularExpressions.RegexOptions.IgnoreCase);
            System.Text.RegularExpressions.Regex regex5 = new System.Text.RegularExpressions.Regex(@"<frameset[\s\S]+</frameset *>", System.Text.RegularExpressions.RegexOptions.IgnoreCase);
            System.Text.RegularExpressions.Regex regex6 = new System.Text.RegularExpressions.Regex(@"\<img[^\>]+\>", System.Text.RegularExpressions.RegexOptions.IgnoreCase);
            System.Text.RegularExpressions.Regex regex7 = new System.Text.RegularExpressions.Regex(@"</p>", System.Text.RegularExpressions.RegexOptions.IgnoreCase);
            System.Text.RegularExpressions.Regex regex8 = new System.Text.RegularExpressions.Regex(@"<p>", System.Text.RegularExpressions.RegexOptions.IgnoreCase);
            System.Text.RegularExpressions.Regex regex9 = new System.Text.RegularExpressions.Regex(@"<[^>]*>", System.Text.RegularExpressions.RegexOptions.IgnoreCase);
            html = regex1.Replace(html, ""); //过滤<script></script>标记 
            html = regex2.Replace(html, ""); //过滤href=javascript: (<A>) 属性 
            html = regex3.Replace(html, " _disibledevent="); //过滤其它控件的on...事件 
            html = regex4.Replace(html, ""); //过滤iframe 
            html = regex5.Replace(html, ""); //过滤frameset 
            html = regex6.Replace(html, ""); //过滤frameset 
            html = regex7.Replace(html, ""); //过滤frameset 
            html = regex8.Replace(html, ""); //过滤frameset 
            html = regex9.Replace(html, "");
            html = html.Replace("&nbsp;", " ");
            html = html.Replace("&#40;", "(");
            html = html.Replace("&#41;", ")");
            html = html.Replace("\n\r", "");
            html = html.Replace("\r\n", "");
            html = html.Replace("\n", "");
            html = html.Replace("\r", "");
            html = html.Replace("</strong>", "");
            html = html.Replace("<strong>", "");
            html = html.Replace("'", "");
            html = html.Replace(" ", "");
            return html;
        }
        #endregion

        #region 替换SQL非法字符
        /// <summary>
        /// 替换SQL非法字符
        /// </summary>
        /// <param name="text">要替换字符串</param>
        /// <returns></returns>
        public static string ReplaceSqlChar(string text)
        {
            return string.IsNullOrEmpty((text)) ? "" : text.Replace("'", "''");
        }

        #endregion

        #region 返回字符串长度
        /// <summary>
        /// 返回字符串长度
        /// </summary>
        /// <remarks>
        /// 1个汉字占2个字节
        /// </remarks>
        /// <param name="text">字符串参数</param>
        /// <returns></returns>
        public static int GetStringLength(string text)
        {
            int result = 0;

            for (int i = 0; i < text.Length; i++)
            {
                byte[] bytLength = Encoding.Default.GetBytes(text.Substring(i, 1));
                if (bytLength.Length > 1)
                    result += 2; //如果长度大于1，是中文，占两个字节，+2
                else
                    result += 1; //如果长度等于1，是英文，占一个字节，+1
            }

            return result;
        }
        #endregion

        #region 去除字符串的所有空格
        /// <summary>
        /// 去除字符串的所有空格。
        /// </summary>
        /// <param name="text">字符串</param>
        /// <returns>字符串</returns>
        public static string StringTrimAll(string text)
        {
            string _text = text;
            if (string.IsNullOrEmpty(_text))
            {
                return " ";
            }
            string returnText = String.Empty;
            char[] chars = _text.ToCharArray();
            for (int i = 0; i < chars.Length; i++)
            {
                if (chars[i].ToString() != string.Empty)
                    returnText += chars[i].ToString();
            }
            return returnText;
        }

        #endregion

        #region 转全角的函数(SBC case)

        /// <summary>
        /// 转全角的函数(SBC case)
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static string ToSBC(string input)
        {
            //半角转全角：
            char[] c = input.ToCharArray();
            for (int i = 0; i < c.Length; i++)
            {
                if (c[i] == 32)
                {
                    c[i] = (char)12288;
                    continue;
                }
                if (c[i] < 127)
                    c[i] = (char)(c[i] + 65248);
            }
            return new string(c);
        }

        #endregion

        #region 转半角的函数(SBC case)

        /// <summary>
        ///  转半角的函数(SBC case)
        /// </summary>
        /// <param name="input">输入</param>
        /// <returns></returns>
        public static string ToDBC(string input)
        {
            char[] c = input.ToCharArray();
            for (int i = 0; i < c.Length; i++)
            {
                if (c[i] == 12288)
                {
                    c[i] = (char)32;
                    continue;
                }
                if (c[i] > 65280 && c[i] < 65375)
                    c[i] = (char)(c[i] - 65248);
            }
            return new string(c);
        }

        #endregion
    }
}
