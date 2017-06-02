using Nt.BLL.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;

namespace Nt.Web
{
    /// <summary>
    /// NtUtility 的摘要说明
    /// </summary>
    public static class CommonUtility
    {
        public static bool ContainsUrl(string text)
        {
            Regex r = new Regex(@"http://(\w)*");
            return r.IsMatch(text);
        }

        /// <summary>
        /// 过滤敏感词汇
        /// </summary>
        /// <param name="text"></param>
        /// <param name="sensitiveWords"></param>
        /// <returns></returns>
        public static string FilterSensitiveWords(string text, string sensitiveWords)
        {
            if (string.IsNullOrEmpty(sensitiveWords) || string.IsNullOrEmpty(text))
                return text;
            string[] words = sensitiveWords
                .Split(new string[] { "\r\n" }, StringSplitOptions.RemoveEmptyEntries);
            foreach (var item in words)
            {
                text = text.Replace(item, "***");
            }
            return text;
        }

        /// <summary>
        /// 生成页码数据
        /// </summary>
        /// <param name="pageIndex">当前页码</param>
        /// <param name="pageSize">页面大小</param>
        /// <param name="pageCount">总页数</param>
        /// <param name="viewCount">可见的页码数</param>
        /// <returns>包含可见页码的数据</returns>
        public static int[] GenPager(int pageIndex, int pageSize, int pageCount, int viewCount)
        {
            if (pageIndex < 1)
                pageIndex = 1;
            if (pageIndex > pageCount)
                pageIndex = pageCount;

            int[] list = null;
            int p = 0;
            if (pageCount <= viewCount)
            {
                list = new int[pageCount];
                for (int i = 1; i <= pageCount; i++)
                {
                    list[p++] = i;
                }
                p = 0;
            }
            else
            {
                list = new int[viewCount];
                int v = (viewCount + 1) % 2;
                int b = viewCount / 2;
                int sup = pageIndex + b - v;
                int sub = pageIndex - b;
                if (sup >= viewCount && sup <= pageCount)
                {
                    for (int i = sub; i <= sup; i++)
                    {
                        list[p++] = i;
                    }
                }
                else if (sup < viewCount)
                {
                    for (int i = 1; i <= viewCount; i++)
                    {
                        list[p++] = i;
                    }
                }
                else
                {
                    for (int i = pageCount - viewCount + 1; i <= pageCount; i++)
                    {
                        list[p++] = i;
                    }
                }
            }
            return list;
        }

        /// <summary>
        /// 删除文本中的Html
        /// </summary>
        /// <param name="Htmlstring">带有Html的文本</param>
        /// <returns></returns>
        public static string RemoveHTML(string Htmlstring)
        {
            //删除脚本  
            Htmlstring = Regex.Replace(Htmlstring, @"<script[^>]*?>.*?</script>", "", RegexOptions.IgnoreCase);
            //删除HTML  
            Htmlstring = Regex.Replace(Htmlstring, @"<(.[^>]*)>", "", RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, @"([\r\n])[\s]+", "", RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, @"-->", "", RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, @"<!--.*", "", RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, @"&(quot|#34);", "\"", RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, @"&(amp|#38);", "&", RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, @"&(lt|#60);", "<", RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, @"&(gt|#62);", ">", RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, @"&(nbsp|#160);", "   ", RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, @"&(iexcl|#161);", "\xa1", RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, @"&(cent|#162);", "\xa2", RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, @"&(pound|#163);", "\xa3", RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, @"&(copy|#169);", "\xa9", RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, @"&#(\d+);", "", RegexOptions.IgnoreCase);

            Htmlstring.Replace("<", "");
            Htmlstring.Replace(">", "");
            Htmlstring.Replace("\r\n", "");
            Htmlstring = HttpContext.Current.Server.HtmlEncode(Htmlstring).Trim();

            return Htmlstring;
        }

        /// <summary>
        /// 获取截断的字符串
        /// </summary>
        /// <param name="inputString">原始字符串</param>
        /// <param name="len">保留的长度</param>
        /// <returns>截断的字符串</returns>
        public static string GetSubString(string inputString, int len)
        {
            ASCIIEncoding ascii = new ASCIIEncoding();
            int tempLen = 0;
            string tempString = "";
            byte[] s = ascii.GetBytes(inputString);
            for (int i = 0; i < s.Length; i++)
            {
                if ((int)s[i] == 63)
                {
                    tempLen += 2;
                }
                else
                {
                    tempLen += 1;
                }

                try
                {
                    tempString += inputString.Substring(i, 1);
                }
                catch
                {
                    break;
                }

                if (tempLen > len)
                    break;
            }
            //如果截过则加上半个省略号
            byte[] mybyte = System.Text.Encoding.Default.GetBytes(inputString);
            if (mybyte.Length > len)
                tempString += "…";
            return tempString;
        }

        /// <summary>
        /// 获取页码的链接
        /// </summary>
        /// <param name="sortId">类别id</param>
        /// <param name="page">页码</param>
        /// <returns></returns>
        public static string GetPagerUrl(int sortId, int page)
        {
            if (page < 1)
            { return "javascript:;"; }
            return string.Format(
                "{0}?SortID={1}&Page={2}",
                WebHelper.Request.Url.AbsolutePath,
                sortId, page
                );
        }

        /// <summary>
        /// 获取详细页的页面跳转链接
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static string GetDetailUrl(int id)
        {
            if (id < 1)
            { return "javascript:;"; }
            return string.Format(
                "{0}?ID={1}",
                WebHelper.Request.Url.AbsolutePath,
                id
                );
        }

        /// <summary>
        /// 无类别的情况下使用这个方法获取页码的链接
        /// </summary>
        /// <param name="page">页码</param>
        /// <returns></returns>
        public static string GetPagerUrl(int page)
        {
            if (page < 1)
            { return "javascript:;"; }
            return string.Format(
                "{0}?Page={1}",
                WebHelper.Request.Url.AbsolutePath,
                page
                );
        }

        /// <summary>
        /// 获取文本中的首个英文单词
        /// </summary>
        /// <param name="text">文本</param>
        /// <returns></returns>
        public static string GetFirstWord(string text)
        {
            Regex reg = new Regex(@"^\w+\b");
            Match mat = reg.Match(text);
            if (mat.Success)
                return mat.Value;
            else
                return "NoMatch";
        }

        public static string RemoveFirstWord(string text)
        {
            return Regex.Replace(text, @"^\w+\b", "", RegexOptions.None);
        }

        /// <summary>
        /// 日期格式化
        /// </summary>
        /// <param name="date">日期</param>
        /// <param name="format">格式化字符串</param>
        /// <returns></returns>
        public static string DateTimeFormat(object date, string format)
        {
            if (date == null)
                return DateTime.Now.ToString(format);
            DateTime true_date;
            if (DateTime.TryParse(date.ToString(), out true_date))
            {
                return true_date.ToString(format);
            }
            return DateTime.Now.ToString(format);
        }

        /// <summary>
        /// 获取子字符串
        /// </summary>
        /// <param name="s"></param>
        /// <param name="len"></param>
        /// <returns></returns>
        public static string GetSubStr(this String s, int len)
        {
            return GetSubString(s, len);
        }

        /// <summary>
        /// 将源自textareahtml控件里的文本内容转换为html
        /// </summary>
        /// <param name="contentInTextArea">textareahtml控件里的内容</param>
        /// <returns></returns>
        public static string TextAreaToHtml(string contentInTextArea)
        {
            if (string.IsNullOrEmpty(contentInTextArea))
                return string.Empty;
            return contentInTextArea.Replace("\r\n", "<br/>").Replace(" ","&nbsp;&nbsp;");
        }

    }
}