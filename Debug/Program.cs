using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Data.SqlServerCe;

namespace Debug
{
    public class Program
    {

        static int c = 0;

        [STAThread]
        public static void Main(string[] args)
        {
            string connString = @"Data Source=C:\Users\Administrator\Documents\compact.sdf;Password=zxh_19890103;Persist Security Info=True";
            SqlCeConnection conn = new SqlCeConnection(connString);
            conn.Open();

            //AsyncCallback call = new AsyncCallback(haha);
            //call.Invoke(null);
            //c = 30;
            //Console.WriteLine(Regex.IsMatch("/a/b/201409080000001.c", @"^/(\w+/)+\d{14}(.jpg|.png)$"));
            //Console.WriteLine(Regex.Replace("xxxxanzixadsads", "anzi", "", RegexOptions.IgnoreCase));
            //Console.ReadKey();
        }

        static void haha(IAsyncResult r)
        {
            System.Threading.Thread t = new System.Threading.Thread(m);
            System.Threading.Thread.Sleep(2 * 1000);
            t.Start();
        }

        static void m()
        {
            
            while (true)
            {
                way();
            }
        }

        static void way()
        {
            c++;
            Console.WriteLine(c);
            System.Threading.Thread.Sleep(2 * 1000);
        }

        public static string WrapLink(string input, string word, string href)
        {
            if (input.Length == 0
                || word.Length == 0
                || input.Length < word.Length)
                return input;

            StringBuilder sb = new StringBuilder(input);
            int p = 0;//当前位置，索引
            int next = 0;//下一个索引
            int len = word.Length;//链接词的长度
            int maxIndex = 0;//最大索引值
            string tagBegin = string.Format("<a title=\"{1}\" href=\"{0}\">", href, word);//开始标签
            int tagBeginLen = tagBegin.Length;//开始标签的长度
            string tagEnd = "</a>";//结束标签
            int tagEndLen = 4;//结束标签的长度
            int j = 0;
            bool success = true;//一个值，指示当前匹配是否成功
            bool breakable = false;//一个值，指示是否该退出循环
            while (true)
            {
                maxIndex = sb.Length - 1;
                success = true;
                j = 0;
                for (int i = 0; i < len; i++)
                {
                    if (i + p > maxIndex)//如果指定超出字符串的最大索引，则指示可以退出循环
                        breakable = true;
                    else
                    {
                        if (sb[i + p] != word[j])
                        {
                            success = false;
                            break;
                        }
                        j++;
                    }
                }
                if (breakable) break;
                //进一步确认当前匹配是否有效
                if (success)
                {
                    next = p + len;
                    if (next + 3 <= maxIndex
                       && sb[next] == '<'
                       && sb[next + 1] == '/'
                       && sb[next + 2] == 'a'
                       && sb[next + 3] == '>')
                    {
                        next += tagEndLen;
                    }
                    else
                    {
                        sb.Insert(p, tagBegin);
                        next += tagBeginLen;
                        sb.Insert(next, tagEnd);
                        next += tagEndLen;
                    }
                }
                else
                {
                    next = p + 1;
                }
                p = next;
            }
            return sb.ToString();
        }
        
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
    }
}
