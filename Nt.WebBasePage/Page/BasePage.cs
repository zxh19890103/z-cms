using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using Nt.Framework;
using Nt.BLL;
using Nt.BLL.Mail;
using Nt.Model;
using Nt.Model.SettingModel;
using System.Web.UI.WebControls;
using System.Text;
using System.Text.RegularExpressions;
using System.Data;
using System.IO;
using Nt.Model.View;
using Nt.DAL;
using Nt.DAL.Helper;

namespace Nt.Web
{
    /// <summary>
    /// BasePage 的摘要说明
    /// </summary>
    public class BasePage : Page
    {
        #region Counter
        protected int _counter = 0;
        #endregion

        #region Service
        protected NavigationService _naviService;
        protected MailSendService _mailSender = null;
        #endregion

        #region props

        /// <summary>
        /// 网站基本信息
        /// </summary>
        private WebsiteInfoSettings _webSiteInfo;
        public WebsiteInfoSettings WebsiteInfo
        {
            get
            {
                if (_webSiteInfo == null)
                {
                    _webSiteInfo = SettingService.GetSettingModel<WebsiteInfoSettings>(
                        NtConfig.CurrentLanguageModel.LanguageCode);
                }
                return _webSiteInfo;
            }
        }

        /// <summary>
        /// 是否使用导航数据表中的标题、关键词、描述
        /// </summary>
        public bool UseNavigationAsSeo { get; set; }

        string _pageTitle;
        /// <summary>
        /// 网站标题
        /// </summary>
        public string PageTitle
        {
            get
            {
                if (UseNavigationAsSeo || string.IsNullOrEmpty(_pageTitle))
                {
                    if (string.IsNullOrEmpty(CurrentNaviItem.MetaTitle))
                    {
                        return WebsiteInfo.WebsiteName;
                    }
                    return CurrentNaviItem.MetaTitle + "-" + WebsiteInfo.WebsiteName;
                }
                return _pageTitle + "-" + WebsiteInfo.WebsiteName;
            }
            set { _pageTitle = value; }
        }

        string _keywords;
        /// <summary>
        ///  关键词
        /// </summary>
        public virtual string Keywords
        {
            get
            {
                if (UseNavigationAsSeo || string.IsNullOrEmpty(_keywords))
                {
                    if (string.IsNullOrEmpty(CurrentNaviItem.MetaKeywords))
                    {
                        return WebsiteInfo.Keywords;
                    }
                    return CurrentNaviItem.MetaKeywords;
                }
                return _keywords;
            }
            set { _keywords = value; }
        }

        string _description;
        /// <summary>
        /// 描述
        /// </summary>
        public virtual string Description
        {
            get
            {
                if (UseNavigationAsSeo || string.IsNullOrEmpty(_description))
                {
                    if (string.IsNullOrEmpty(CurrentNaviItem.MetaDescription))
                    {
                        return WebsiteInfo.Description;
                    }
                    return CurrentNaviItem.MetaDescription;
                }
                return _description;
            }
            set { _description = value; }
        }

        DataTable _naviData;
        /// <summary>
        /// 生成导航的
        /// </summary>
        public DataTable NavigationData
        {
            get
            {
                if (_naviData == null)
                    _naviData = CommonFactory.GetList("Nt_Navigation",
                        string.Format("Language_Id={0} ", NtConfig.CurrentLanguage), "DisplayOrder");
                return _naviData;
            }
        }

        Nt_Navigation _currentNaviItem;
        /// <summary>
        /// 当前导航项数据
        /// </summary>
        public Nt_Navigation CurrentNaviItem
        {
            get
            {
                if (_currentNaviItem == null)
                {
                    _currentNaviItem = new Nt_Navigation();
                    foreach (DataRow r in NavigationData.Rows)
                    {
                        if ((int)r["id"] == ChannelNo)
                        {
                            _currentNaviItem.Path = (string)r["Path"];
                            _currentNaviItem.Id = (int)r["Id"];
                            _currentNaviItem.Language_Id = (int)r["Language_Id"];
                            _currentNaviItem.MetaDescription = (string)r["MetaDescription"];
                            _currentNaviItem.MetaKeywords = (string)r["MetaKeywords"];
                            _currentNaviItem.MetaTitle = (string)r["MetaTitle"];
                            _currentNaviItem.Name = (string)r["Name"];
                            _currentNaviItem.Parent = (int)r["Parent"];
                            _currentNaviItem.HtmlPath = (string)r["HtmlPath"];
                            _currentNaviItem.DisplayOrder = (int)r["DisplayOrder"];
                            _currentNaviItem.Display = (bool)r["Display"];
                            _currentNaviItem.Depth = (int)r["Depth"];
                            _currentNaviItem.Crumbs = (string)r["Crumbs"];
                            _currentNaviItem.AnchorTarget = (string)r["AnchorTarget"];
                            break;
                        }
                    }
                }
                return _currentNaviItem;
            }
        }

        /// <summary>
        /// 频道ID
        /// </summary>
        public int ChannelNo { get; set; } //channel id,0 means home

        public int SubChannelNo { get; set; }//二级导航
        public int TriChannelNo { get; set; }//三级导航

        /// <summary>
        /// 页面类型
        /// </summary>
        public virtual int PageType
        {
            get { return NtConfig.INDEX; }
        }

        string _currentNaviName = string.Empty;
        /// <summary>
        /// 当前频道名称
        /// </summary>
        public string CurrentNaviName
        {
            get
            {
                if (string.IsNullOrEmpty(_currentNaviName))
                {
                    _currentNaviName = "UnKnown";
                    foreach (DataRow r in NavigationData.Rows)
                    {
                        if (Convert.ToInt32(r["ID"]) == ChannelNo)
                        {
                            _currentNaviName = r["Name"].ToString();
                            break;
                        }
                    }
                }
                return _currentNaviName;
            }
        }

        /// <summary>
        /// Banners
        /// </summary>
        private DataView _banners = null;
        public DataView Banners
        {
            get
            {
                if (_banners == null)
                {
                    FetchAllCommonData();
                }
                return _banners;
            }
        }

        /// <summary>
        /// Sliders 没有内容时为null，使用之前先判断一下
        /// </summary>
        private DataView _sliders = null;
        public DataView Sliders
        {
            get
            {
                if (_sliders == null)
                {
                    FetchAllCommonData();
                }
                return _sliders;
            }
        }

        /// <summary>
        /// Links
        /// </summary>
        private DataView _links = null;
        public DataView Links
        {
            get
            {
                if (_links == null)
                {
                    FetchAllCommonData();
                }
                return _links;
            }
        }

        /// <summary>
        /// Scripts
        /// </summary>
        private DataView _scripts = null;
        /// <summary>
        /// 脚本
        /// </summary>
        public DataView Scripts
        {
            get
            {
                if (_scripts == null)
                {
                    FetchAllCommonData();
                }
                return _scripts;
            }
        }

        private DataView _searchKeyWords = null;
        /// <summary>
        /// 搜索关键词
        /// </summary>
        public DataView SearchKeywords
        {
            get
            {
                if (_searchKeyWords == null)
                {
                    FetchAllCommonData();
                }
                return _searchKeyWords;
            }
        }

        /// <summary>
        /// 当前页的静态页
        /// </summary>
        public virtual string HtmlPageUrl
        {
            get
            {
                return NtConfig.CurrentChannelRootUrl + "index.html";
            }
        }

        #endregion

        #region override

        protected override void OnPreInit(EventArgs e)
        {
            base.OnPreInit(e);
            if (WebsiteInfo.SetWebsiteOff)
            {
                if (File.Exists(MapPath("/close.html")))
                    Server.Transfer("/close.html");
                else
                {
                    Response.ContentType = "text/html";
                    Response.Write("<div style=\"margin:0 auto;font-size:20px;font-weight:bold;\">网站已经关闭</div>");
                    Response.End();
                }
            }
            if (!NtConfig.HasMutiLanguage)
                return;
        }

        protected override void OnUnload(EventArgs e)
        {
            base.OnUnload(e);
            NtConfig.EmptyStaticResources();
            if (_searchKeyWords != null) _searchKeyWords.Dispose();
            if (_scripts != null) _scripts.Dispose();
            if (_links != null) _links.Dispose();
            if (_sliders != null) _sliders.Dispose();
            if (_banners != null) _banners.Dispose();
            if (_naviData != null) _naviData.Dispose();
            if (_dataContainer != null) _dataContainer.Dispose();
            if (_newsOnHomePage != null) _newsOnHomePage.Dispose();
            if (_productOnHomePage != null) _productOnHomePage.Dispose();
            if (_courseOnHomePage != null) _courseOnHomePage.Dispose();

            //关闭所有的数据库链接

            //SqlHelper.GetConnection().Close();
        }

        protected override void OnLoad(EventArgs e)
        {
            TrySkipToMobileSite();
            _mailSender = new MailSendService();
            _naviService = new NavigationService();
            _sqlBuilder = new StringBuilder();
            _pictureService = new PictureService();
            InitCommonData();
        }

        protected virtual void InitCommonData() { }

        /// <summary>
        /// 是否是移动设备请求
        /// </summary>
        bool IsMobileDevice
        {
            get
            {
                bool flag = false;
                string agent = Request.UserAgent;
                string[] keywords = { "Android", "iPhone", "iPod", "iPad", "Windows Phone", "MQQBrowser" };
                //排除Window 桌面系统 和 苹果桌面系统
                if (!agent.Contains("Windows NT") && !agent.Contains("Macintosh"))
                {
                    foreach (string item in keywords)
                    {
                        if (agent.Contains(item))
                        {
                            flag = true;
                            break;
                        }
                    }
                }
                return flag;
            }
        }

        /// <summary>
        /// 跳转到手机网站
        /// </summary>
        public void TrySkipToMobileSite()
        {
            if (Htmlizer.Instance.IsRunning)
                return;
            string mUrl = NtConfig.MobileSiteUrl;
            //没有手机网站
            if (string.IsNullOrEmpty(mUrl)) { }
            else
            {
                string requestUrl = Request.Url.AbsolutePath.ToLower();
                if (mUrl.StartsWith("/"))
                {
                    if (!requestUrl.Contains(mUrl)
                        && IsMobileDevice)
                    {
                        Response.Redirect(mUrl);
                    }
                }
                else if (Regex.IsMatch(mUrl, "^http[s]?://"))
                {
                    if (IsMobileDevice)
                    {
                        Response.Redirect(mUrl);
                    }
                }
            }
        }

        /// <summary>
        /// 静态页跳转
        /// </summary>
        public virtual void TryRedirectHtmlPage()
        {
            //首页跳转
            string htmlPath = HtmlPageUrl;
            if (WebsiteInfo.SetHtmlOn
                && !Htmlizer.Instance.IsRunning
                && File.Exists(MapPath(htmlPath)))
            {
                Server.Transfer(htmlPath);
            }
        }

        #endregion

        #region fetch muti once

        bool _allCommonDataFetched = false;
        /// <summary>
        /// 获取Slider，Searchkeyword，banner，links，javascript
        /// </summary>
        void FetchAllCommonData()
        {
            if (_allCommonDataFetched)
                return;
            bool hasAddedSql = false;
            string addSql = "";
            string phy_path = MapPath(string.Format("/App_Data/Txt/slider-data-{0}.txt",
                       NtConfig.CurrentLanguageModel.LanguageCode));
            if (File.Exists(phy_path))
            {
                string sliderids = File.ReadAllText(phy_path);
                if (!string.IsNullOrEmpty(sliderids))
                {
                    addSql = string.Format(
                        "select * from nt_picture where display=1 and id in ({0}) order by displayorder desc;",
                        sliderids);
                    hasAddedSql = true;
                }
            }

            string sql = string.Format("select * from view_banner {0};\r\n select * from view_link {0};\r\n select * from Nt_JavaScript where display=1 order by DisplayOrder desc;\r\n select * from Nt_SearchKeyword {0}; \r\n {1}\r\n",
                "where Display=1 And Language_Id=" + NtConfig.CurrentLanguage + " order by DisplayOrder desc", addSql);
            DataSet ds = SqlHelper.ExecuteDataset(sql);
            _banners = ds.Tables[0].DefaultView;
            _links = ds.Tables[1].DefaultView;
            _scripts = ds.Tables[2].DefaultView;
            _searchKeyWords = ds.Tables[3].DefaultView;
            if (hasAddedSql)
                _sliders = ds.Tables[4].DefaultView;
            _allCommonDataFetched = true;
        }

        DataSet _dataContainer;
        StringBuilder _sqlBuilder;

        /// <summary>
        /// 数据容器
        /// </summary>
        public DataSet DataContainer
        {
            get
            {
                if (_dataContainer == null)
                    ExecuteSql();
                return _dataContainer;
            }
        }

        /// <summary>
        /// 执行sql语句获取数据
        /// </summary>
        public void ExecuteSql()
        {
            if (_sqlBuilder.Length > 0)
            {
                _dataContainer = SqlHelper.ExecuteDataset(_sqlBuilder.ToString());
                _sqlBuilder.Remove(0, _sqlBuilder.Length);
            }
        }

        /// <summary>
        /// 获取用于提取列表数据的sql语句
        /// </summary>
        /// <returns></returns>
        string GetFetchSql(string table, string filter, string orderby, int pageindex, int pagesize)
        {
            string sql = string.Empty;
            if (pageindex == 1)
            {
                sql = string.Format("Select Top {0} * From [{1}] Where {2} Order by {3} ", pagesize, table, filter, orderby);
            }
            else
            {
                string subSelect = string.Format("Select Top {0} ID From [{1}] Where {2} Order By {3}",
                    (pageindex - 1) * pagesize, table, filter, orderby);
                sql = string.Format("Select Top {0} * From [{1}] Where {2} And ID Not in ({3}) Order by {4}", pagesize, table, filter, subSelect, orderby);
            }
            return sql;
        }

        public void FetchNewsOnHomePage(int sortID, int displayItemsCount)
        {
            string filter = " Display=1 And Language_Id=" +
                   NtConfig.CurrentLanguage;
            if (sortID > 0)
            {
                filter += " And CategoryCrumbs like '%," + sortID + ",%' ";
            }
            _sqlBuilder.Append(GetFetchSql("View_News", filter,
                NtConfig.CommonOrderBy, 1, displayItemsCount));
        }

        public void FetchProductOnHomePage(int sortID, int displayItemsCount, bool isDownload)
        {
            string filter = " Display=1 And IsDownloadable=" +
                    (isDownload ? "1" : "0")
                    + " And Language_Id=" + NtConfig.CurrentLanguage;
            if (sortID > 0)
            {
                filter += " And CategoryCrumbs like '%," + sortID + ",%' ";
            }
            _sqlBuilder.Append(GetFetchSql("View_Product", filter,
              NtConfig.CommonOrderBy, 1, displayItemsCount));
        }

        public void FetchCourseOnHomePage(int sortID, int displayItemsCount)
        {
            string filter = " Display=1 And Language_Id=" +
                    NtConfig.CurrentLanguage;
            if (sortID > 0)
            {
                filter += " And CategoryCrumbs like '%," + sortID + ",%' ";
            }
            _sqlBuilder.Append(GetFetchSql("View_course", filter,
              NtConfig.CommonOrderBy, 1, displayItemsCount));
        }

        #endregion

        #region 输出内容，广告，js，搜索词，友情链接

        private View_Content _content = null;
        public View_Content Content { get { return _content; } }

        /// <summary>
        /// 通用内容
        /// </summary>
        /// <returns></returns>
        public bool FetchContent(int id, bool forceReget)
        {
            if (_content == null || forceReget)
            {
                _content = CommonFactory.GetById<View_Content>(id);
                if (_content == null)
                    throw new Exception("没有ID为" + id + "的Content实例.");
                _content.Text = CommonUtility.TextAreaToHtml(_content.Text);
            }
            return true;
        }

        private Nt_Picture _advert = null;
        public Nt_Picture Advert { get { return _advert; } }

        /// <summary>
        /// 获取广告
        /// </summary>
        /// <param name="id"></param>
        /// <param name="forceReget"></param>
        /// <returns></returns>
        public bool FetchAdvert(int id, bool forceReget)
        {
            if (_advert == null || forceReget)
            {
                _advert = CommonFactory.GetById<Nt_Picture>(id);
                if (_advert == null)
                {
                    throw new Exception("没有ID为" + id + "的Advert.");
                }
            }
            return true;
        }

        /// <summary>
        /// 输出js脚本
        /// </summary>
        public void WriteJavaScript()
        {
            foreach (DataRowView r in Scripts)
            {
                Response.Write(r["Script"]);
            }
        }

        /// <summary>
        /// 输出搜索关键词{keyword}
        /// </summary>
        /// <param name="template">Html模板</param>
        public void WriteSearchWords(string template)
        {
            foreach (DataRowView sw in SearchKeywords)
            {
                Response.Write(template.Replace("{keyword}", sw["KeyWord"].ToString()));
            }
        }

        /// <summary>
        /// 输出友情链接 {img},{url},{text}
        /// </summary>
        /// <param name="template">Html模板</param>
        public void WriteFriendLink(string template)
        {
            foreach (DataRowView l in Links)
            {
                Response.Write(template
                    .Replace("{img}", l["PictureUrl"].ToString())
                    .Replace("{url}", l["Url"].ToString())
                    .Replace("{text}", l["Text"].ToString()));
            }
        }

        /// <summary>
        /// 输出banners   {img},{url},{text}
        /// </summary>
        /// <param name="template"></param>
        public void WriteBanners(string template)
        {
            foreach (DataRowView b in Banners)
            {
                Response.Write(template
                    .Replace("{img}", b["PictureUrl"].ToString())
                    .Replace("{url}", b["Url"].ToString())
                    .Replace("{text}", b["Text"].ToString()));
            }
        }

        #endregion

        #region fetch

        /// <summary>
        /// 首页显示新闻
        /// </summary>
        private DataView _newsOnHomePage = null;
        public DataView NewsOnHomePage { get { return _newsOnHomePage; } }
        public bool FetchNewsOnHomePage(int sortID, int displayItemsCount, bool forceReget)
        {
            if (_newsOnHomePage == null || forceReget)
            {
                string filter = " Display=1 And Language_Id=" +
                    NtConfig.CurrentLanguage;
                if (sortID > 0)
                {
                    filter += " And CategoryCrumbs like '%," + sortID + ",%' ";
                }
                _newsOnHomePage = CommonFactory.GetList("View_News", filter,
                    NtConfig.CommonOrderBy, 1, displayItemsCount).DefaultView;
            }
            return _newsOnHomePage != null && _newsOnHomePage.Count > 0;
        }


        ProductSettings _settings;
        PictureService _pictureService;
        /// <summary>
        /// 首页显示产品
        /// </summary>
        private DataView _productOnHomePage = null;
        public DataView ProductOnHomePage { get { return _productOnHomePage; } }
        public bool FetchProductOnHomePage(int sortID, int displayItemsCount, bool forceReget, bool isDownload)
        {
            if (_productOnHomePage == null || forceReget)
            {
                string filter = " Display=1 And IsDownloadable=" +
                    (isDownload ? "1" : "0")
                    + " And Language_Id=" + NtConfig.CurrentLanguage;
                if (sortID > 0)
                {
                    filter += " And CategoryCrumbs like '%," + sortID + ",%' ";
                }
                _productOnHomePage = CommonFactory.GetList("View_Product", filter,
                    NtConfig.CommonOrderBy, 1, displayItemsCount).DefaultView;
                HandleProductData(_productOnHomePage);
            }

            return _productOnHomePage != null && _productOnHomePage.Count > 0;
        }

        /// <summary>
        /// 处理产品数据
        /// </summary>
        /// <param name="d"></param>
        public void HandleProductData(DataView d)
        {
            if (d == null) return;
            if (_settings == null)
                _settings = SettingService.GetSettingModel<ProductSettings>();

            if (_settings.EnableThumbOnHomePage)
            {
                foreach (DataRowView r in d)
                {
                    r["ThumbnailUrl"] = _pictureService
                    .GetThumbnailUrl(r["ThumbnailUrl"].ToString(),
                    _settings.ThumbOnHomePageHeight, _settings.ThumbOnHomePageWidth,
                    _settings.ThumbOnHomePageMode);
                }
            }
        }

        /// <summary>
        /// 首页课程
        /// </summary>
        private DataView _courseOnHomePage = null;
        public DataView CourseOnHomePage { get { return _courseOnHomePage; } }
        public bool FetchCourseOnHomePage(int sortID, int displayItemsCount, bool forceReget)
        {
            if (_courseOnHomePage == null || forceReget)
            {
                string filter = " Display=1 And Language_Id=" + NtConfig.CurrentLanguage;
                if (sortID > 0)
                {
                    filter += " And CategoryCrumbs like '%," + sortID + ",%' ";
                }
                _courseOnHomePage = CommonFactory.GetList("View_Course", filter,
                    NtConfig.CommonOrderBy, 1, displayItemsCount).DefaultView;
            }

            return _courseOnHomePage != null && _courseOnHomePage.Count > 0;
        }

        #endregion

        #region 导航

        /// <summary>
        /// 输出导航
        /// </summary>
        /// <param name="outerTag">导航最外围的标签，例如ul</param>
        /// <param name="liTag">列表标签，一般为li</param>
        /// <param name="currentStyle">当前频道的样式，class="current"</param>
        /// <param name="liTemplate">导航循环项的模板，关键替换字 {text},{url}</param>
        /// <param name="depth">递归的深度，0表示一级，1表示递归到二级...</param>
        public void RenderMenu(string outerTag, string liTag, string currentStyle, string liTemplate, int depth, object wrapAttrs)
        {
            _counter = 0;
            if (_counter > depth)
                return;
            if (wrapAttrs != null)
                WriteBeginTag(outerTag, wrapAttrs);
            else
                WriteBeginTag(outerTag);
            foreach (DataRow item in NavigationData.Select("[Parent]=0"))
            {
                if (item["Display"].ToString() == "False")
                    continue;
                if (!string.IsNullOrEmpty(currentStyle)
                    && ChannelNo == Convert.ToInt32(item["Id"]))
                    WriteBeginTag(liTag, new { _class = currentStyle });
                else
                    WriteBeginTag(liTag);
                string li = liTemplate
                    .Replace("{text}", item["Name"].ToString())
                    .Replace("{target}", item["AnchorTarget"].ToString());

                string path = string.Empty;
                if (WebsiteInfo.SetHtmlOn)
                {
                    path = string.Format("/html/{0}.html", item["ID"]);//导航静态文件的绝对路径
                }
                else
                {
                    path = item["Path"].ToString();
                    if (!(path.StartsWith("/")
                        || path.StartsWith("http://", StringComparison.OrdinalIgnoreCase)
                        || path.StartsWith("https://", StringComparison.OrdinalIgnoreCase)))
                    {
                        path = NtConfig.CurrentTemplatesPath + path;
                    }
                }
                li = li.Replace("{url}", path);
                Response.Write(li);
                RenderSubMenu(item["Id"], outerTag, liTag, currentStyle, liTemplate, depth);
                WriteEndTag(liTag);
            }
            WriteEndTag(outerTag);
        }

        /// <summary>
        /// 递归函数
        /// </summary>
        private void RenderSubMenu(object pid, string outerTag, string liTag, string currentStyle, string liTemplate, int depth)
        {
            _counter++;
            if (_counter > depth)
            {
                _counter--;
                return;
            }
            DataRow[] data = NavigationData.Select("[Parent]=" + pid);
            if (data != null && data.Count() < 1)
            {
                _counter--;
                return;
            }
            WriteBeginTag(outerTag);
            foreach (var item in data)
            {
                if (item["Display"].ToString() == "False")
                    continue;
                if (!string.IsNullOrEmpty(currentStyle)
                    && ChannelNo == Convert.ToInt32(item["Id"]))
                    WriteBeginTag(liTag, new { _class = currentStyle });
                else
                    WriteBeginTag(liTag);
                string li = liTemplate
                  .Replace("{text}", item["Name"].ToString())
                  .Replace("{target}", item["AnchorTarget"].ToString());

                string path = string.Empty;
                if (WebsiteInfo.SetHtmlOn)
                {
                    path = string.Format("/html/{0}.html", item["ID"]);//导航静态文件的绝对路径
                }
                else
                {
                    path = item["Path"].ToString();
                    if (!(path.StartsWith("/")
                       || path.StartsWith("http://", StringComparison.OrdinalIgnoreCase)
                       || path.StartsWith("https://", StringComparison.OrdinalIgnoreCase)))
                    {
                        path = NtConfig.CurrentTemplatesPath + path;
                    }
                }
                li = li.Replace("{url}", path);

                Response.Write(li);
                RenderSubMenu(item["Id"], outerTag, liTag, currentStyle, liTemplate, depth);
                WriteEndTag(liTag);
            }
            WriteEndTag(outerTag);
            _counter--;
        }

        #endregion

        #region Config html on left menu here,the data is from Nt_navigation

        /// <summary>
        /// 侧边导航
        /// </summary>
        /// <param name="pid">父级id</param>
        /// <param name="outerTag">导航最外围的标签，例如ul</param>
        /// <param name="liTag">列表标签，一般为li</param>
        /// <param name="currentStyle">当前频道的样式，class="current"</param>
        /// <param name="liTemplate">导航循环项的模板，关键替换字 {text},{url}</param>
        /// <param name="depth">递归的深度，0表示一级，1表示递归到二级...</param>
        public void RenderLeftMenu(int pid, string outerTag, string liTag, string currentStyle, string liTemplate, int depth, object wrapAttrs)
        {
            _counter = 0;
            if (_counter > depth)
                return;
            DataRow[] data = NavigationData.Select("[Parent]=" + pid);
            if (data != null && data.Count() < 1)
                return;
            if (wrapAttrs != null)
                WriteBeginTag(outerTag, wrapAttrs);
            else
                WriteBeginTag(outerTag);
            foreach (var item in data)
            {
                if (item["Display"].ToString() == "False")
                    continue;
                if (SubChannelNo == Convert.ToInt32(item["Id"]) && !string.IsNullOrEmpty(currentStyle))
                    WriteBeginTag(liTag, new { _class = currentStyle });
                else
                    WriteBeginTag(liTag);
                string li = liTemplate
                   .Replace("{text}", item["Name"].ToString())
                   .Replace("{target}", item["AnchorTarget"].ToString());

                string path = string.Empty;
                if (WebsiteInfo.SetHtmlOn)
                {
                    path = string.Format("/html/{0}.html", item["ID"]);//导航静态文件的绝对路径
                }
                else
                {
                    path = item["Path"].ToString();
                    if (!(path.StartsWith("/")
                       || path.StartsWith("http://", StringComparison.OrdinalIgnoreCase)
                       || path.StartsWith("https://", StringComparison.OrdinalIgnoreCase)))
                    {
                        path = NtConfig.CurrentTemplatesPath + path;
                    }
                }
                li = li.Replace("{url}", path);

                Response.Write(li);
                RenderSubMenu(item["Id"], outerTag, liTag, currentStyle, liTemplate, depth);
                WriteEndTag(liTag);
            }
            WriteEndTag(outerTag);
        }

        /// <summary>
        /// outerTag="ul"  liTag="li" currentStyle="current"
        /// </summary>
        public void RenderLeftMenu(int pid, string liTemplate, int depth)
        {
            RenderLeftMenu(pid, "ul", "li", "current", liTemplate, depth, null);
        }

        /// <summary>
        /// liTemplate="&lt;a href=\"{url}\"&gt;{text}&lt;/a"&gt;
        /// </summary>
        public void RenderLeftMenu(int pid, int depth)
        {
            RenderLeftMenu(pid, "<a href=\"{url}\">{text}</a>", depth);
        }

        /// <summary>
        /// outerTag="ul", liTag="li", currentStyle="", string liTemplate, int depth, wrapAttrs=null
        /// </summary>
        public void RenderMenu(string liTemplate, int depth)
        {
            RenderMenu("ul", "li", "", liTemplate, depth, null);
        }

        /// <summary>
        /// liTemplate="&lt;a href=\"{url}\"&gt;{text}&lt;/a"&gt;
        /// </summary>
        public void RenderMenu(int depth)
        {
            RenderMenu("<a href=\"{url}\">{text}</a>", depth);
        }

        #endregion

        #region WriteBeginTag
        /// <summary>
        /// write begin tag
        /// </summary>
        /// <param name="tagName">tag's name</param>
        public void WriteBeginTag(string tagName)
        {
            if (string.IsNullOrEmpty(tagName))
                return;
            Response.Write("<");
            Response.Write(tagName);
            Response.Write(">");
        }

        /// <summary>
        /// write begin tag with attributes
        /// </summary>
        /// <param name="tagName">tag's name</param>
        /// <param name="attrs">attributes</param>
        public void WriteBeginTag(string tagName, object attrs)
        {
            if (string.IsNullOrEmpty(tagName))
                return;
            Response.Write("<");
            Response.Write(tagName);
            foreach (var p in attrs.GetType().GetProperties())
            {
                if (p.Name.StartsWith("_"))
                    Response.Write(string.Format(" {0}=\"{1}\"", p.Name.Remove(0, 1), p.GetValue(attrs, null)));
                else
                    Response.Write(string.Format(" {0}=\"{1}\"", p.Name, p.GetValue(attrs, null)));
            }
            Response.Write(">");
        }

        /// <summary>
        /// write end tag
        /// </summary>
        /// <param name="tagName">tag's name</param>
        public void WriteEndTag(string tagName)
        {
            Response.Write("</");
            Response.Write(tagName);
            Response.Write(">");
        }

        public void GotoErrorPage(string msg)
        {
            Response.Redirect(NtConfig.CurrentTemplatesPath, true);
        }

        #endregion

        #region html url
        /// <summary>
        /// 获取详细页
        /// </summary>
        /// <param name="id">id</param>
        /// <param name="type">调用类型</param>
        /// <param name="hrefBase">url的基部（模版地址）</param>
        /// <returns></returns>
        public virtual string GetDetailUrl(object id, int type, string hrefBase)
        {
            int int_id = (Int32)id;
            if (WebsiteInfo.SetHtmlOn)
            {
                if (int_id > 0)
                    return string.Format("/html/{0}/{1}.html", type, id);
                else
                    return "javascript:;";
            }
            else
            {
                if (int_id <= 0)
                    return "javascript:;";
                if (string.IsNullOrEmpty(hrefBase))
                    throw new Exception("既然没有开启静态设置,那么您所提供的链接字符串hrefBase就不能为空!");
                if (hrefBase.StartsWith("/"))
                    return hrefBase + "?ID=" + id;
                else
                    return NtConfig.CurrentChannelRootUrl + hrefBase + "?ID=" + id;
            }
        }

        /// <summary>
        /// 二级页面
        /// </summary>
        /// <param name="id">id</param>
        /// <param name="hrefBase">url的基部</param>
        /// <returns></returns>
        public virtual string GetSinglePageUrl(object id, string hrefBase)
        {
            return GetDetailUrl(id, NtConfig.SINGLEPAGE, hrefBase);
        }
        /// <summary>
        /// 二级页面 默认/page.aspx
        /// </summary>
        /// <param name="id">id</param>
        /// <returns></returns>
        public virtual string GetSinglePageUrl(object id)
        {
            return GetDetailUrl(id, NtConfig.SINGLEPAGE, "/page.aspx");
        }
        /// <summary>
        /// 新闻详细页
        /// </summary>
        /// <param name="id">id</param>
        /// <param name="hrefBase">url的基部</param>
        /// <returns></returns>
        public virtual string GetNewsUrl(object id, string hrefBase)
        {
            return GetDetailUrl(id, NtConfig.NEWS, hrefBase);
        }
        public virtual string GetNewsUrl(object id)
        {
            return GetDetailUrl(id, NtConfig.NEWS, "/newsdetail.aspx");
        }
        /// <summary>
        /// 产品详细页
        /// </summary>
        /// <param name="id">id</param>
        /// <param name="hrefBase">url的基部</param>
        /// <returns></returns>
        public virtual string GetProductUrl(object id, string hrefBase)
        {
            return GetDetailUrl(id, NtConfig.PRODUCT, hrefBase);
        }
        /// <summary>
        /// 产品详细页 默认地址  productdetail.aspx
        /// </summary>
        /// <param name="id">id</param>
        /// <returns></returns>

        public virtual string GetProductUrl(object id)
        {
            return GetDetailUrl(id, NtConfig.PRODUCT, "/productdetail.aspx");
        }

        /// <summary>
        /// 产品详细页
        /// </summary>
        /// <param name="id">id</param>
        /// <param name="hrefBase">url的基部</param>
        /// <returns></returns>
        public virtual string GetDownloadUrl(object id, string hrefBase)
        {
            return GetDetailUrl(id, NtConfig.DOWNLOAD, hrefBase);
        }
        /// <summary>
        /// 产品详细页 默认地址  productdetail.aspx
        /// </summary>
        /// <param name="id">id</param>
        /// <returns></returns>

        public virtual string GetDownloadUrl(object id)
        {
            return GetDetailUrl(id, NtConfig.PRODUCT, "/downloaddetail.aspx");
        }

        /// <summary>
        /// Job 详细页
        /// </summary>
        /// <param name="id">id</param>
        /// <param name="hrefBase">url的基部</param>
        /// <returns></returns>
        public virtual string GetJobUrl(object id, string hrefBase)
        {
            return GetDetailUrl(id, NtConfig.JOB, hrefBase);
        }

        public virtual string GetJobUrl(object id)
        {
            return GetDetailUrl(id, NtConfig.JOB, "/jobdetail.aspx");
        }

        public virtual string GetCourseUrl(object id, string hrefBase)
        {
            return GetDetailUrl(id, NtConfig.COURSE, hrefBase);
        }

        public virtual string GetCourseUrl(object id)
        {
            return GetDetailUrl(id, NtConfig.COURSE, "/coursedetail.aspx");
        }

        /// <summary>
        /// 获取列表页链接
        /// </summary>
        /// <param name="sortId">类别id</param>
        /// <param name="type">页面类型</param>
        /// <param name="hrefBase">url的基部</param>
        /// <returns></returns>
        public virtual string GetListUrl(object sortId, int type, string hrefBase, int page)
        {
            if (WebsiteInfo.SetHtmlOn)
            {
                return string.Format("/html/{0}/{1}_{2}.html", type, sortId, page);
            }
            else
            {
                if (string.IsNullOrEmpty(hrefBase))
                    throw new Exception("既然没有开启静态设置,那么您所提供的链接字符串hrefBase就不能为空!");
                if (hrefBase.StartsWith("/"))
                    return string.Format("{1}?SortID={0}&Page={2}", sortId, hrefBase, page);
                else
                    return string.Format("{0}?SortID={1}&Page={2}", NtConfig.CurrentChannelRootUrl + hrefBase, sortId, page);
            }
        }
        /// <summary>
        /// 新闻列表页  序号：1---（列表新闻首页）
        /// </summary>
        /// <param name="sortId">类别id</param>
        /// <param name="hrefBase">url的基部</param>
        /// <returns></returns>
        public string GetNewsListUrl(object sortId, string hrefBase)
        {
            return GetListUrl(sortId, NtConfig.NEWS_LIST, hrefBase, 1);
        }
        /// <summary>
        /// 新闻列表页 默认: " /news.aspx"   序号：1---（列表新闻首页）
        /// </summary>
        /// <param name="sortId">类别id</param>
        /// <returns></returns>
        public string GetNewsListUrl(object sortId)
        {
            return GetListUrl(sortId, NtConfig.NEWS_LIST, "/news.aspx", 1);
        }
        /// <summary>
        /// 产品列表页  序号：1---（列表新闻首页）
        /// </summary>
        /// <param name="sortId">类别id</param>
        /// <param name="hrefBase">url的基部</param>
        /// <returns></returns>
        public string GetProductListUrl(object sortId, string hrefBase)
        {
            return GetListUrl(sortId, NtConfig.PRODUCT_LIST, hrefBase, 1);
        }
        /// <summary>
        /// 产品默认列表页 product.aspx 序号：1---（列表新闻首页）
        /// </summary>
        /// <param name="sortId">类别id</param>
        /// <returns></returns>
        public string GetProductListUrl(object sortId)
        {
            return GetListUrl(sortId, NtConfig.PRODUCT_LIST, "/product.aspx", 1);
        }

        /// <summary>
        /// 产品列表页  序号：1---（列表新闻首页）
        /// </summary>
        /// <param name="sortId">类别id</param>
        /// <param name="hrefBase">url的基部</param>
        /// <returns></returns>
        public string GetDownloadListUrl(object sortId, string hrefBase)
        {
            return GetListUrl(sortId, NtConfig.DOWNLOAD_LIST, hrefBase, 1);
        }
        /// <summary>
        /// 产品默认列表页 product.aspx 序号：1---（列表新闻首页）
        /// </summary>
        /// <param name="sortId">类别id</param>
        /// <returns></returns>
        public string GetDownloadListUrl(object sortId)
        {
            return GetListUrl(sortId, NtConfig.DOWNLOAD_LIST, "/download.aspx", 1);
        }
        /// <summary>
        /// Job 列表页 （无列表id'）序号：1---（列表新闻首页）
        /// </summary>
        /// <param name="hrefBase">url的基部</param>
        /// <returns></returns>
        public string GetJobListUrl(string hrefBase)
        {
            return GetListUrl(NtConfig.JOB_LIST, NtConfig.JOB_LIST, hrefBase, 1);
        }
        /// <summary>
        ///  job列表页         默认：job.aspx    序号：1---（列表新闻首页）
        /// </summary>
        /// <returns></returns>
        public string GetJobListUrl()
        {
            return GetListUrl(NtConfig.JOB_LIST, NtConfig.JOB_LIST, "/job.aspx", 1);
        }

        public string GetCourseListUrl(object sortId, string hrefBase)
        {
            return GetListUrl(sortId, NtConfig.COURSE_LIST, hrefBase, 1);
        }

        public string GetCourseListUrl(object sortId)
        {
            return GetListUrl(sortId, NtConfig.COURSE_LIST, "/course.aspx", 1);
        }

        #endregion

        #region Config Html on Left Catalog Here News OR Product
        /// <summary>
        /// 描画产品类别的树形层次
        /// </summary>
        /// <param name="sortid">产品类别id</param>
        /// <param name="outerTag">最表层标签</param>
        /// <param name="liTag">循环标签</param>
        /// <param name="liTemplate">项模板  {text},{value}</param>
        /// <param name="depth">递归深度</param>
        public void RenderNOPCatalog(DataTable data, string outerTag, string liTag, string liTemplate, int depth, object wrapAttrs)
        {
            _counter = 0;
            if (_counter > depth)
                return;
            if (wrapAttrs != null)
                WriteBeginTag(outerTag, wrapAttrs);
            else
                WriteBeginTag(outerTag);
            foreach (DataRow item in data.Rows)
            {
                if (item["Display"].ToString() == "False")
                    continue;
                WriteBeginTag(liTag);
                if (WebsiteInfo.SetHtmlOn)
                {
                    string url = System.Text.RegularExpressions.Regex.Replace(liTemplate,
                        @"href=""([^""]+)""", string.Format("href=\"/html/{0}/{1}_1.html\"", PageType, item["Id"]));
                    Response.Write(url);
                }
                else
                {
                    Response.Write(liTemplate
                    .Replace("{text}", item["Name"].ToString())
                    .Replace("{value}", item["Id"].ToString()));
                }
                RenderSubNOPCatalog(data, Convert.ToInt32(item["ID"]), outerTag, liTag, liTemplate, depth);
                WriteEndTag(liTag);
            }
            WriteEndTag(outerTag);
        }

        /// <summary>
        /// 递归函数
        /// </summary>
        /// <param name="sortid">类别ID</param>
        /// <param name="outerTag">外围标签</param>
        /// <param name="liTag">循环标签</param>
        /// <param name="liTemplate">模板</param>
        /// <param name="depth">深度</param>
        void RenderSubNOPCatalog(DataTable data, int sortid, string outerTag, string liTag, string liTemplate, int depth)
        {
            _counter++;
            if (_counter > depth)
            {
                _counter--;
                return;
            }
            DataRow[] subdata = data.Select("[Parent]=" + sortid);
            if (data != null && subdata.Count() < 1)
            {
                _counter--;
                return;
            }

            foreach (DataRow item in subdata)
            {
                WriteBeginTag(liTag);
                if (WebsiteInfo.SetHtmlOn)
                {
                    string url = System.Text.RegularExpressions.Regex.Replace(liTemplate,
                        @"href=""([^""]+)""", string.Format("href=\"/html/{0}/{1}_1.html\"", PageType, item["Id"]));
                    Response.Write(url.Replace("{text}", item["Name"].ToString()));
                }
                else
                {
                    Response.Write(liTemplate
                    .Replace("{text}", item["Name"].ToString())
                    .Replace("{value}", item["Id"].ToString()));
                }
                RenderSubNOPCatalog(data, Convert.ToInt32(item["ID"]), outerTag, liTag, liTemplate, depth);
                WriteEndTag(liTag);
            }
            _counter--;
        }


        #endregion

        #region utility

        /// <summary>
        /// 包含一个文本文件
        /// </summary>
        /// <param name="path">虚拟路径</param>
        public void Include(string path)
        {
            string pathOnDisk = MapPath(path);
            if (File.Exists(pathOnDisk))
            {
                try
                {
                    string c = File.ReadAllText(pathOnDisk);
                    Response.Write(c);
                }
                catch (Exception ex)
                {
                    Response.Write(ex.Message);
                }
            }
        }

        #endregion

    }
}