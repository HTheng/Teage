using Lucene.Net.Analysis;
using Lucene.Net.Analysis.PanGu;
using PanGu;
using PanGu.HighLight;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Web;
using System.Web.Caching;

namespace Teage.Common
{
    public class WebCommon
    {
        /// <summary>
        /// 二次Md5加密
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static string GetTwoMd5(string str)
        {
            return Md5String(Md5String(str));
        }

        /// <summary>
        /// 字符串MD5运算.
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static string Md5String(string str)
        {
            MD5 md5 = MD5.Create();
            byte[] buffer = System.Text.Encoding.UTF8.GetBytes(str);
            byte[] newBuffer = md5.ComputeHash(buffer);
            StringBuilder sb = new StringBuilder();
            foreach (byte b in newBuffer)
            {
                sb.Append(b.ToString("x2"));
            }
            return sb.ToString();
        }
        /// <summary>
        /// 生成随机验证码(纯数字)
        /// </summary>
        /// <returns></returns>
        public static string GetRandNumber(int length)
        {
            System.Random random = new System.Random();
            int number;
            char code;
            string randomCode = "";
            for (int i = 0; i < length; i++)
            {
                number = random.Next();
                code = (char)('0' + (char)(number % 10));
                randomCode += code.ToString();
            }
            return randomCode;
        }
        /// <summary>
        /// 获取路径
        /// </summary>
        /// <param name="time"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        public static string GetPath(DateTime time, int id)
        {
            return "/StaticPage/" + time.Year + "/" + time.Month + "/" + time.Day + "/" + id + ".html";
        }
        /// <summary>
        /// 截取字符串
        /// </summary>
        /// <returns></returns>
        public static string CutString(string str, int length)
        {
            if (str.Length > length)
            {
                return str.Substring(0, length) + "...";
            }
            else
            {
                return str;
            }
        }
        /// <summary>
        /// 创建分页
        /// </summary>
        /// <param name="pageIndex"></param>
        /// <param name="pageCount"></param>
        /// <returns></returns>
        public static string CreatePage(int pageIndex, int pageCount)
        {
            if (pageCount == 1)
            {
                return string.Empty;
            }
            //计算起始位置
            int start = pageIndex - 5;
            start = start < 1 ? 1 : start;
            //计算终止位置 
            int end = start + 9;
            end = end > pageCount ? pageCount : end;
            StringBuilder sb = new StringBuilder();
            for (int i = start; i <= end; i++)
            {
                if (i == pageIndex)
                {
                    sb.Append(i);
                }
                else
                {
                    sb.Append(string.Format("<a href='?pageIndex={0}'>{0}</a>", i));
                }
            }
            return sb.ToString();
        }
        /// <summary>
        /// 使用盘古进行分词
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public static string[] PanguSpilt(string key)
        {
            Analyzer analyzer = new PanGuAnalyzer();
            TokenStream tokenStream = analyzer.TokenStream("", new StringReader(key));
            Lucene.Net.Analysis.Token token = null;
            List<string> list = new List<string>();
            while ((token = tokenStream.Next()) != null)
            {
                // Console.WriteLine(token.TermText());
                list.Add(token.TermText());
            }
            return list.ToArray();
        }
        /// <summary>
        /// 创建HtmlFormatter,参数为高亮的前后缀
        /// </summary>
        /// <param name="keywords"></param>
        /// <param name="content"></param>
        /// <returns></returns>
        public static string CreateHeightLight(string keywords, string content)
        {
            SimpleHTMLFormatter simpleHTMLFormatter =
                        new SimpleHTMLFormatter("<font color=\"red\">", "</font>");
            //创建Highlighter ，输入HTMLFormatter 和盘古分词对象Semgent
            Highlighter highlighter =
             new Highlighter(simpleHTMLFormatter,
             new Segment());
            //设置每个摘要段的字符数
            highlighter.FragmentSize = 150;
            //获取最匹配的摘要段
            return highlighter.GetBestFragment(keywords, content);
        }
        /// <summary>
        /// 获取路径
        /// </summary>
        /// <param name="time"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        public static string GetDir(DateTime time, int id)
        {
            return "/StaticPage/" + time.Year + "/" + time.Month + "/" + time.Day + "/" + id + ".html";
        }
        /// <summary>
        /// 跳转到登录页面，但是要记录当前访问页的Url
        /// </summary>
        public static void GoPage()
        {
            HttpContext.Current.Response.Redirect("/Login/Index");
        }
        /// <summary>
        /// 将UBB编码替换成标准的Html编码
        /// </summary>
        /// <param name="argString"></param>
        /// <returns></returns>
        public static string Decode(string argString)
        {
            string str = argString;
            if (str != "")
            {
                Regex regex;
                bool state = true;
                str = str.Replace("&", "&amp");
                str = str.Replace(">", "&gt");
                str = str.Replace("<", "&lt");
                str = str.Replace("\"", "&quot");
                str = Regex.Replace(str, @"\[br\]", "<br />", RegexOptions.IgnoreCase);
                string[,] tRegexAry = {
          {@"\[p\]([^\[]*?)\[\/p\]", "$1<br />"},
          {@"\[b\]([^\[]*?)\[\/b\]", "<b>$1</b>"},
          {@"\[i\]([^\[]*?)\[\/i\]", "<i>$1</i>"},
          {@"\[u\]([^\[]*?)\[\/u\]", "<u>$1</u>"},
          {@"\[ol\]([^\[]*?)\[\/ol\]", "<ol>$1</ol>"},
          {@"\[ul\]([^\[]*?)\[\/ul\]", "<ul>$1</ul>"},
          {@"\[li\]([^\[]*?)\[\/li\]", "<li>$1</li>"},
          {@"\[code\]([^\[]*?)\[\/code\]", "<div class=\"ubb_code\">$1</div>"},
          {@"\[quote\]([^\[]*?)\[\/quote\]", "<div class=\"ubb_quote\">$1</div>"},
          {@"\[color=([^\]]*)\]([^\[]*?)\[\/color\]", "<font style=\"color: $1\">$2</font>"},
          {@"\[hilitecolor=([^\]]*)\]([^\[]*?)\[\/hilitecolor\]", "<font style=\"background-color: $1\">$2</font>"},
          {@"\[align=([^\]]*)\]([^\[]*?)\[\/align\]", "<div style=\"text-align: $1\">$2</div>"},
          {@"\[url=([^\]]*)\]([^\[]*?)\[\/url\]", "<a href=\"$1\">$2</a>"},
          {@"\[img\]([^\[]*?)\[\/img\]", "<img src=\"$1\" />"}
        };
                while (state)
                {
                    state = false;
                    for (int i = 0; i < tRegexAry.GetLength(0); i++)
                    {
                        regex = new Regex(tRegexAry[i, 0], RegexOptions.IgnoreCase);
                        if (regex.Match(str).Success)
                        {
                            state = true;
                            str = Regex.Replace(str, tRegexAry[i, 0], tRegexAry[i, 1], RegexOptions.IgnoreCase);
                        }
                    }
                }
            }
            return str;
        }
        /// <summary>
        /// 对时间差进行处理，3天5小时20分钟 ->3*24+5+20/60
        /// </summary>
        /// <param name="time"></param>
        /// <returns></returns>
        public static string GetTimeSpan(TimeSpan time)
        {
            if (time.TotalDays >= 365)
            {
                return Math.Floor(time.TotalDays / 365) + "年前";
            }
            else if (time.TotalDays >= 30)
            {
                return Math.Floor(time.TotalDays / 30) + "月前";
            }
            else if (time.TotalHours >= 24)
            {
                return Math.Floor(time.TotalHours) + "天前";
            }
            else if (time.TotalHours >= 1)
            {
                return Math.Floor(time.TotalHours) + "小时前";
            }
            else if (time.TotalMinutes >= 1)
            {
                return Math.Floor(time.TotalMinutes) + "分钟前";
            }
            else
            {
                return "刚刚";
            }
        }
    }
}
