using Nt.BLL.Helper;
using Nt.DAL.Helper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Compilation;
using System.Web.UI;
using System.Xml;

namespace Nt.Web
{
    /// <summary>
    /// Htmlizer 的摘要说明
    /// </summary>
    public class Htmlizer
    {
        #region singleton
        private static Htmlizer _instance = new Htmlizer();
        static readonly object padlock = new object();
        public static Htmlizer Instance
        {
            get
            {
                return _instance;
            }
        }

        #endregion

        #region fields

        XmlDocument _xmlDoc;
        XmlNode _root;
        private int _currentType = 0;
        private object _currentParameters;
        private string _currentTemplatePath;
        private string _currentDirPath;
        private int _currentID;
        private int _currentPage = 0;
        private bool _isRunning = false;
        private int _interval = 5;//请求的时间间隔
        private int _counter = 0;
        private int _collectCircle = 50;//回收周期
        private int _total = 0;

        #endregion

        #region Props

        /// <summary>
        /// 计数
        /// </summary>
        public int Total { get { return _total; } }

        /// <summary>
        /// 消息
        /// </summary>
        public string Message { get; set; }

        /// <summary>
        /// html file's path
        /// </summary>
        public string HtmlPath
        {
            get
            {
                return HtmlDirPath + HtmlFileName;
            }
        }

        /// <summary>
        /// current html file's name
        /// </summary>
        public string HtmlFileName
        {
            get
            {
                switch (_currentType)
                {
                    case 0:
                        return _currentID + ".html";
                    case 1:
                        return "index.html";
                    case 2:
                    case 3:
                    case 4:
                    case 5:
                    case 6:
                    case 7:
                        return _currentID + ".html";
                    case 8:
                    case 9:
                    case 10:
                    case 11:
                    case 12:
                        return _currentID + "_" + _currentPage + ".html";
                    default:
                        return "unknown.html";
                }
            }
        }

        /// <summary>
        /// current html directory's path
        /// </summary>
        public string HtmlDirPath
        {
            get
            {
                switch (_currentType)
                {
                    case 0:
                        return "/html/";
                    case 1:
                        int pos = _currentTemplatePath.LastIndexOf('/');
                        string rootUrl = _currentTemplatePath.Substring(0, pos + 1);
                        _currentDirPath = rootUrl;
                        return _currentDirPath;
                    case 2:
                    case 3:
                    case 4:
                    case 5:
                    case 6:
                    case 7:
                    case 8:
                    case 9:
                    case 10:
                    case 11:
                    case 12:
                        _currentDirPath = "/" + _currentType + "/";
                        return "/html" + _currentDirPath;
                    default:
                        throw new Exception(_currentType + "不是合法的页面类型");
                }
            }
        }

        /// <summary>
        /// current query string
        /// </summary>
        public string QueryString
        {
            get
            {
                if (_currentParameters != null)
                {
                    string queryString = "";
                    Type type = _currentParameters.GetType();
                    foreach (var item in type.GetProperties())
                    {
                        queryString += item.Name + "=" + item.GetValue(_currentParameters, null);
                        queryString += "&";
                    }
                    if (queryString != "")
                        queryString += "fix=end";
                    return queryString;
                }
                return "";
            }
        }

        /// <summary>
        /// current request url
        /// </summary>
        public string RequestUrl
        {
            get
            {
                if (_currentType != NtConfig.NAVI
                     || !Regex.IsMatch(_currentTemplatePath, "^http[s]?://", RegexOptions.IgnoreCase))
                {
                    if (string.IsNullOrEmpty(QueryString))
                        return WebHelper.CurrentRootUrl + _currentTemplatePath;
                    return WebHelper.CurrentRootUrl + _currentTemplatePath + "?" + QueryString;
                }
                else
                {
                    return _currentTemplatePath;
                }
            }
        }

        /// <summary>
        /// 当前静态化的类别
        /// </summary>
        public int CurrenType
        {
            get { return _currentType; }
            set
            {
                if (_isRunning) return;
                _currentType = value;
            }
        }

        /// <summary>
        /// 请求的时间间隔(秒)
        /// </summary>
        public int Interval
        {
            get { return _interval; }
            set { _interval = value; }
        }

        /// <summary>
        /// 是否正在进行静态化
        /// </summary>
        public bool IsRunning
        {
            get { return _isRunning; }
        }

        /// <summary>
        /// 是否删除静态化文件
        /// </summary>
        public bool IsDeHtmlize { get; set; }

        /// <summary>
        /// 回收周期
        /// </summary>
        public int CollectCircle
        {
            get { return _collectCircle; }
            set { _collectCircle = value; }
        }

        #endregion

        #region primary and base
        /// <summary>
        /// 初始化
        /// </summary>
        void Initialize()
        {
            Message = "空闲";
            _counter = 0;
            _total = 0;
            _xmlDoc = new XmlDocument();
            _xmlDoc.RemoveAll();
            _xmlDoc.Load(WebHelper.MapPath("/App_Data/html.config"));
            _root = _xmlDoc.SelectSingleNode("htmlConfig");
        }

        /// <summary>
        /// 结束
        /// </summary>
        void End()
        {
            _xmlDoc = null;
            _root = null;
            _total = 0;
            _counter = 0;
        }

        void Htmlize(string validUrl, string targetPath)
        {
            _counter++;
            try
            {
                Message = string.Format("正在生成{1},文件路径{0}...", targetPath, PageType);
                HttpWebRequest request = HttpWebRequest.Create(validUrl) as System.Net.HttpWebRequest;
                request.Method = "GET";
                using (WebResponse response = request.GetResponse())
                {
                    Stream data = response.GetResponseStream();
                    string phy_path = WebHelper.MapPath(targetPath);
                    FileStream html = File.Create(phy_path);
                    int i = 0;
                    while ((i = data.ReadByte()) != -1)
                    {
                        html.WriteByte((byte)i);
                    }
                    html.Flush();
                    html.Close();
                }
                _total++;
            }
            catch (Exception ex)
            {
                Message = ex.Message;
                throw ex;
            }
            if (_counter >= CollectCircle)
            {
                System.GC.Collect();
                _counter = 0;
            }
            System.Threading.Thread.Sleep(_interval * 1000);//间隔_interval秒钟再进行下一个请求
        }

        #endregion

        #region html

        void HtmlizeIndex()
        {
            if (CurrenType != 1) return;
            XmlNodeList ns = _root.SelectNodes("add[@type=1]");
            foreach (XmlNode n in ns)
            {
                _currentTemplatePath = n.Attributes["path"].Value;
                _currentParameters = null;
                Htmlize(RequestUrl, HtmlPath);
            }
        }

        void HtmlizeDetailWithCategory()
        {
            if (CurrenType < 2 || CurrenType > 5) return;
            string tablePart = string.Empty;

            switch (CurrenType)
            {
                case NtConfig.NEWS:
                    tablePart = "News";
                    break;
                case NtConfig.PRODUCT:
                    tablePart = "Product";
                    break;
                case NtConfig.COURSE:
                    tablePart = "Course";
                    break;
                case NtConfig.DOWNLOAD:
                    tablePart = "Product";
                    break;
                default:
                    break;
            }

            XmlNodeList ns = _root.SelectNodes("add[@type=" + CurrenType + "]");
            if (!Directory.Exists(WebHelper.MapPath(HtmlDirPath)))
            {
                Directory.CreateDirectory(WebHelper.MapPath(HtmlDirPath));
            }

            foreach (XmlNode n in ns)
            {
                string rootSortId = n.Attributes["rootSortId"].Value;
                _currentTemplatePath = n.Attributes["path"].Value;
                FileInfo aspx = new FileInfo(WebHelper.MapPath(_currentTemplatePath));
                List<object> rows = new List<object>();
                using (SqlDataReader rs = SqlHelper.ExecuteReader(SqlHelper.GetConnection(), CommandType.Text,
                        "Select ID,EditDate From View_" + tablePart + " Where CategoryCrumbs like '%," + rootSortId + ",%' "))
                {
                    while (rs.Read())
                    {
                        rows.Add(rs[0]);
                        rows.Add(rs[1]);
                    }
                }

                for (int i = 0; i < rows.Count; )
                {
                    _currentID = (Int32)rows[i];
                    _currentParameters = new { id = rows[i++] };
                    DateTime editDate = (DateTime)rows[i++];
                    var path = WebHelper.MapPath(HtmlPath);
                    if (File.Exists(path))
                    {
                        FileInfo html = new FileInfo(path);
                        if (aspx.LastWriteTime > html.LastWriteTime
                            || editDate > html.LastWriteTime)
                        {
                            Htmlize(RequestUrl, HtmlPath);
                        }
                    }
                    else
                    {
                        Htmlize(RequestUrl, HtmlPath);
                    }
                }
            }
        }

        /// <summary>
        /// 静态化二级页
        /// </summary>
        void HtmlizeSinglePage()
        {
            if (CurrenType != NtConfig.SINGLEPAGE) return;
            XmlNodeList ns = _root.SelectNodes("add[@type=6]");
            foreach (XmlNode n in ns)
            {
                string ids = n.Attributes["ids"].Value;
                _currentTemplatePath = n.Attributes["path"].Value;
                if (!Directory.Exists(WebHelper.MapPath(HtmlDirPath)))
                {
                    Directory.CreateDirectory(WebHelper.MapPath(HtmlDirPath));
                }

                foreach (var item in ids.Split(','))
                {
                    _currentID = Convert.ToInt32(item);
                    _currentParameters = new { id = item };
                    Htmlize(RequestUrl, HtmlPath);
                }
            }
        }

        /// <summary>
        /// 静态化招聘
        /// </summary>
        void HtmlizeJob()
        {
            if (CurrenType != NtConfig.JOB) return;
            XmlNodeList ns = _root.SelectNodes("add[@type=7]");
            foreach (XmlNode n in ns)
            {
                //这个是必须的，因为不同的语言招聘详细页模板不一样
                //但是作为新闻或产品等，由于分类中已经区分了语言问题，所以不再在查询的时候去过滤语言
                string lang = n.Attributes["lang"].Value;
                _currentTemplatePath = n.Attributes["path"].Value;

                if (!Directory.Exists(WebHelper.MapPath(HtmlDirPath)))
                {
                    Directory.CreateDirectory(WebHelper.MapPath(HtmlDirPath));
                }

                List<object> ids = new List<object>();
                using (SqlDataReader rs = SqlHelper.ExecuteReader(SqlHelper.GetConnection(), CommandType.Text,
                    "Select ID From Nt_Job Where language_id=" + lang))
                {
                    while (rs.Read())
                    {
                        ids.Add(rs[0]);
                    }
                }

                foreach (var item in ids)
                {
                    _currentID = (Int32)item;
                    _currentParameters = new { id = item };
                    Htmlize(RequestUrl, HtmlPath);
                }
            }
        }

        /// <summary>
        /// 按导航静态化
        /// </summary>
        void HtmlizeByNavigation()
        {
            if (CurrenType != NtConfig.NAVI) return;
            int lang = 0;
            string prefix = string.Empty;
            List<object> list = new List<object>();
            using (SqlDataReader rs = SqlHelper.ExecuteReader(SqlHelper.GetConnection(), CommandType.Text,
                    "Select Path,ID,Language_Id From Nt_Navigation "))
            {
                while (rs.Read())
                {
                    list.Add(rs[0]);
                    list.Add(rs[1]);
                    list.Add(rs[2]);
                }
            }

            for (int i = 0; i < list.Count; )
            {
                _currentTemplatePath = (string)list[i++];
                _currentID = (Int32)list[i++];
                lang = (Int32)list[i++];
                if (!_currentTemplatePath.StartsWith("/")
                    || Regex.IsMatch(_currentTemplatePath, "^http[s]?://", RegexOptions.IgnoreCase))
                {
                    prefix = NtConfig.LangCodeIDMappings[lang];
                    _currentTemplatePath = prefix + _currentTemplatePath;
                }
                _currentParameters = null;
                Htmlize(RequestUrl, HtmlPath);
            }
        }

        #endregion

        #region 列表页静态化

        /// <summary>
        /// 静态化归档化类型列表页面
        /// </summary>
        /// <param name="type">类型</param>
        void HtmlizeListWithCategory()
        {
            if (CurrenType != NtConfig.NEWS_LIST
                && CurrenType != NtConfig.PRODUCT_LIST
                && CurrenType != NtConfig.COURSE_LIST
                && CurrenType != NtConfig.DOWNLOAD_LIST)
            {
                return;
            }

            int pageCount = 0;
            int recCount = 0;
            int pageSize = 12;
            string tabPreFix = "";

            switch (CurrenType)
            {
                case NtConfig.NEWS_LIST:
                    pageSize = NtConfig.NewsListPageSize;
                    tabPreFix = "News";
                    break;
                case NtConfig.PRODUCT_LIST:
                    pageSize = NtConfig.ProductListPageSize;
                    tabPreFix = "Product";
                    break;
                case NtConfig.COURSE_LIST:
                    pageSize = NtConfig.CourseListPageSize;
                    tabPreFix = "Course";
                    break;
                case NtConfig.DOWNLOAD_LIST:
                    pageSize = NtConfig.DownloadListPageSize;
                    tabPreFix = "Product";
                    break;
                default:
                    break;
            }

            XmlNodeList ns = _root.SelectNodes("add[@type=" + CurrenType + "]");
            foreach (XmlNode n in ns)
            {
                string rootSortId = n.Attributes["rootSortId"].Value;
                _currentTemplatePath = n.Attributes["path"].Value;

                if (!Directory.Exists(WebHelper.MapPath(HtmlDirPath)))
                {
                    Directory.CreateDirectory(WebHelper.MapPath(HtmlDirPath));
                }

                string sql = "Select ID From Nt_" + tabPreFix + "Category where crumbs like '%," + rootSortId + ",%' ";

                List<object> ids = new List<object>();
                using (SqlDataReader rs = SqlHelper
                    .ExecuteReader(SqlHelper.GetConnection(),
                    CommandType.Text,
                    sql))
                {
                    while (rs.Read())
                    {
                        ids.Add(rs[0]);
                    }
                }

                foreach (var item in ids)
                {
                    _currentID = Convert.ToInt32(item);
                    string filter = " Display=1  And categorycrumbs like '%," + _currentID + ",%' ";

                    recCount = Nt.DAL.CommonFactory.GetRecordCount("View_" + tabPreFix,
                        filter);
                    pageCount = recCount % pageSize == 0 ? recCount / pageSize : recCount / pageSize + 1;

                    for (int i = 1; i <= pageCount; i++)
                    {
                        _currentPage = i;
                        _currentParameters = new { sortid = item, page = i };
                        Htmlize(RequestUrl, HtmlPath);
                    }
                }
            }
        }

        /// <summary>
        /// 静态化非归档化列表页面,目前这个方法适用于有语言字段的表
        /// </summary>
        /// <param name="type">页面类型值</param>
        void HtmlizeList()
        {
            if (CurrenType != NtConfig.JOB_LIST)
                return;

            int pageCount = 0;
            int recCount = 0;
            int pageSize = 12;
            string tabPreFix = "";

            switch (CurrenType)
            {
                case NtConfig.JOB_LIST:
                    pageSize = NtConfig.JobListPageSize;
                    tabPreFix = "Job";
                    break;
                default:
                    break;
            }

            XmlNodeList ns = _root.SelectNodes("add[@type=" + CurrenType + "]");
            foreach (XmlNode n in ns)
            {
                string lang = n.Attributes["lang"].Value;
                _currentTemplatePath = n.Attributes["path"].Value;

                if (!Directory.Exists(WebHelper.MapPath(HtmlDirPath)))
                {
                    Directory.CreateDirectory(WebHelper.MapPath(HtmlDirPath));
                }

                _currentID = CurrenType;
                string filter = "Display=1 And Language_Id=" + lang;
                recCount = Nt.DAL.CommonFactory.GetRecordCount("Nt_" + tabPreFix, filter);
                pageCount = recCount % pageSize == 0 ? recCount / pageSize : recCount / pageSize + 1;
                for (int i = 1; i <= pageCount; i++)
                {
                    _currentPage = i;
                    _currentParameters = new { page = i };
                    Htmlize(RequestUrl, HtmlPath);
                }
            }
        }

        #endregion

        /// <summary>
        ///调用此方法之前需要为CurrentType赋值
        /// </summary>
        public void Run()
        {
            if (_isRunning)
            {
                Message = string.Format("正在静态化{0}，请稍后再试!", PageType);
                return;
            }
            if (CurrenType < 0 || CurrenType > 12)
            {
                Message = CurrenType + "不是有效的页面类型标识!";
                return;
            }

            if (IsDeHtmlize)
            {
                var t = new System.Threading.Thread(DeHtmlize);
                t.Start();
            }
            else
            {
                var t = new System.Threading.Thread(BeginHtmlize);
                t.Start();
            }
        }

        void BeginHtmlize()
        {
            _isRunning = true;
            Initialize();
            HtmlizeByNavigation();
            HtmlizeIndex();
            HtmlizeDetailWithCategory();
            HtmlizeSinglePage();
            HtmlizeJob();
            HtmlizeListWithCategory();
            HtmlizeList();
            Message = string.Format("{0}静态化完成,共生成{1}个页面!", PageType, _total);
            End();
            _isRunning = false;
        }

        void DeHtmlize()
        {
            _isRunning = true;
            try
            {
                switch (CurrenType)
                {
                    case NtConfig.NAVI:
                        _total = 0;
                        foreach (string file in Directory.GetFiles(WebHelper.MapPath("/html/"),
                            "*.html", SearchOption.TopDirectoryOnly))
                        {
                            File.Delete(file);
                            _total++;
                        }
                        break;
                    case NtConfig.INDEX:
                        Initialize();
                        XmlNodeList ns = _root.SelectNodes("add[@type=1]");
                        foreach (XmlNode n in ns)
                        {
                            string path = n.Attributes["path"].Value;
                            int pos = path.LastIndexOf('/');
                            string rootUrl = path.Substring(0, pos + 1);
                            string filepath = WebHelper.MapPath(rootUrl + "index.html");
                            if (File.Exists(filepath))
                            {
                                File.Delete(filepath);
                                _total++;
                            }
                        }
                        break;
                    default:
                        string dir = WebHelper.MapPath("/html/" + CurrenType + "/");
                        if (Directory.Exists(dir))
                        {
                            foreach (string file in Directory.GetFiles(dir,
                                      "*.html",
                                      SearchOption.TopDirectoryOnly))
                            {
                                File.Delete(file);
                                _total++;
                            }
                        }
                        break;
                }
            }
            catch (Exception ex)
            {
                Message = ex.Message;
            }
            Message = string.Format("{0}静态文件已经全部删除!共删除{1}个文件!", PageType, _total);
            _isRunning = false;
            End();
        }

        public string PageType
        {
            get
            {
                switch (CurrenType)
                {
                    case 0:
                        return "导航";
                    case 1:
                        return "首页";
                    case 2:
                        return "新闻详细页";
                    case 3:
                        return "产品详细页";
                    case 4:
                        return "下载详细页";
                    case 5:
                        return "课程详细页";
                    case 6:
                        return "二级页面";
                    case 7:
                        return "招聘页";
                    case 8:
                        return "新闻列表页";
                    case 9:
                        return "产品列表页";
                    case 10:
                        return "下载列表页";
                    case 11:
                        return "课程列表页";
                    case 12:
                        return "招聘列表页";
                    default:
                        return "未进行任何生成!";
                }
            }
        }

    }
}