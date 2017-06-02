using Nt.BLL;
using Nt.DAL;
using Nt.DAL.Helper;
using Nt.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI.WebControls;

namespace Nt.Web
{
    /// <summary>
    /// ListPageWithCategory 的摘要说明
    /// </summary>
    public class ListPageWithCategory<M> : ListPage<M>, IPageWithCategory
        where M : BaseViewModel, new()
    {
        #region Props

        int _sortId;
        /// <summary>
        /// 类别id
        /// </summary>
        public int SortID
        {
            get
            {
                /*if property SortID is not specified,we retain it from Request QueryString
                 */
                if (_sortId == 0)
                {
                    if (!Int32.TryParse(Request.QueryString["SortID"], out _sortId))
                    {
                        object sortobj = SqlHelper.ExecuteScalar(string.Format("Select Top 1 ID From [{0}]",
                            CategoryTableName));
                        if (sortobj != null)
                        {
                            _sortId = Convert.ToInt32(sortobj);
                        }
                        else
                        {
                            GotoErrorPage("参数错误");
                        }
                    }
                }
                return _sortId;
            }
            set { _sortId = value; }
        }

        string _currentCategoryName;
        /// <summary>
        /// 当前类别名
        /// </summary>
        public string CurrentCategoryName
        {
            get
            {
                if (string.IsNullOrEmpty(_currentCategoryName))
                {
                    foreach (DataRow item in Categories.Rows)
                    {
                        if (item["ID"].ToString() == SortID.ToString())
                        {
                            _currentCategoryName = item["Name"].ToString();
                            break;
                        }
                    }
                }
                return _currentCategoryName;
            }
            set { _currentCategoryName = value; }
        }

        string _categoryTableName = string.Empty;
        /// <summary>
        /// 类别表名
        /// </summary>
        public string CategoryTableName
        {
            get
            {
                if (string.IsNullOrEmpty(_categoryTableName))
                {
                    string tabname = typeof(M).Name;
                    if (tabname.StartsWith("View_"))
                    {
                        _categoryTableName = "Nt_" + tabname.Substring(5) + "Category";//View_
                    }
                    else
                    {
                        _categoryTableName = tabname + "Category";//Nt_
                    }
                }
                return _categoryTableName;
            }
        }

        DataTable _categories;
        /// <summary>
        /// 存放全部分类信息的表
        /// </summary>
        public DataTable Categories
        {
            get
            {
                if (_categories == null)
                {
                    _categories = CommonFactory.GetList(CategoryTableName, "Display=1", "DisplayOrder");
                }
                return _categories;
            }
        }

        /// <summary>
        /// Crumbs
        /// </summary>
        List<ListItem> _crumbs = null;
        /// <summary>
        /// 吗，面包屑导航数据
        /// </summary>
        public virtual List<ListItem> Crumbs
        {
            get
            {
                return _crumbs;
            }
            set
            {
                if (value == null)
                    return;
                if (_crumbs == null)
                    _crumbs = new List<ListItem>();
                foreach (var item in value)
                    _crumbs.Add(item);
            }
        }

        public override string HtmlPageUrl
        {
            get
            {
                return string.Format("/html/{0}/{1}_{2}.html", PageType, SortID, PageIndex);
            }
        }

        #endregion

        #region Config Html on Left Catalog Here
        /// <summary>
        /// 描画产品类别的树形层次
        /// </summary>
        /// <param name="sortid">产品类别id</param>
        /// <param name="outerTag">最表层标签</param>
        /// <param name="liTag">循环标签</param>
        /// <param name="liTemplate">项模板  {text},{value}</param>
        /// <param name="depth">递归深度</param>
        public void RenderCatalog(int sortid, string outerTag, string liTag, string currentStyle, string liTemplate, int depth, object wrapAttrs)
        {
            _counter = 0;
            if (_counter > depth)
                return;
            if (wrapAttrs != null)
                WriteBeginTag(outerTag, wrapAttrs);
            else
                WriteBeginTag(outerTag);
            DataRow[] data = Categories.Select("[Parent]=" + sortid);
            foreach (var item in data)
            {
                if (item["Display"].ToString() == "False") continue;
                if (!string.IsNullOrEmpty(currentStyle)
                    && (SortID == Convert.ToInt32(item["ID"])))
                    WriteBeginTag(liTag, new { _class = currentStyle });
                else
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
                RenderSubCatalog(Convert.ToInt32(item["ID"]), outerTag, liTag, currentStyle, liTemplate, depth);
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
        void RenderSubCatalog(int sortid, string outerTag, string liTag, string currentStyle, string liTemplate, int depth)
        {
            _counter++;
            if (_counter > depth)
            {
                _counter--;
                return;
            }
            DataRow[] data = Categories.Select("[Parent]=" + sortid);
            if (data != null && data.Count() < 1)
            {
                _counter--;
                return;
            }

            foreach (var item in data)
            {
                if (item["Display"].ToString() == "False") continue;
                if (!string.IsNullOrEmpty(currentStyle)
                    && (SortID == Convert.ToInt32(item["ID"])))
                    WriteBeginTag(liTag, new { _class = currentStyle });
                else
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
                RenderSubCatalog(Convert.ToInt32(item["ID"]), outerTag, liTag, currentStyle, liTemplate, depth);
                WriteEndTag(liTag);
            }
            _counter--;
        }

        protected override void OnUnload(EventArgs e)
        {
            base.OnUnload(e);
            if (_categories != null) _categories.Dispose();
        }

        #endregion

        #region methods
        /// <summary>
        /// 点击
        /// </summary>
        public void Rating()
        {
            SqlHelper.ExecuteNonQuery(
                string.Format("Update [{0}] Set Clickrate=Clickrate+1 Where ID={1}", CategoryTableName, SortID));
        }

        /// <summary>
        /// 获取列表数据
        /// </summary>
        /// <returns></returns>
        public override bool TryGetList()
        {
            AddFilter(" CategoryCrumbs like '%," + SortID + ",%' ");
            if (base.TryGetList())
            {
                Rating();
                return true;
            }
            return false;
        }

        /// <summary>
        /// 设置默认的SortID参数，如果通过Request.QueryString获取不到该值
        /// </summary>
        /// <param name="sortId"></param>
        public void SetDefaultSortIDIfRequestFailed(int sortId)
        {
            if (string.IsNullOrEmpty(Request.QueryString["SortID"]))
            {
                SortID = sortId;
            }
            else
            {
                SortID = Convert.ToInt32(Request.QueryString["SortID"]);
            }
        }

        /// <summary>
        /// 输出面包屑
        /// </summary>
        /// <param name="separator">间隔符，一般为:&gt;</param>
        /// <param name="template">{value},{text}</param>
        public virtual void WriteCrumbs(string separator, string template)
        {
            if (Crumbs == null)
                return;
            int i = 0;
            foreach (var item in Crumbs)
            {
                if (WebsiteInfo.SetHtmlOn)
                {
                    string url = System.Text.RegularExpressions.Regex.Replace(template,
                        @"href=""([^""]+)""", string.Format("href=\"/html/{0}/{1}_1.html\"", PageType, item.Value));
                    Response.Write(url.Replace("{text}", item.Text));
                }
                else
                {
                    Response.Write(template
                    .Replace("{text}", item.Text.ToString())
                    .Replace("{value}", item.Value.ToString()));
                }
                if (i < Crumbs.Count - 1)
                    Response.Write(separator);
                i++;
            }
        }

        /// <summary>
        /// 输出面包屑 使用默认的模板
        /// </summary>
        /// <param name="separator">间隔符，一般为:&gt;</param>
        public virtual void WriteCrumbs(string separator)
        {
            string path = Request.Url.LocalPath;
            string template = "<a href=\"" + path + "?SortID={value}\">{text}</a>";
            WriteCrumbs(separator, template);
        }

        /// <summary>
        /// 输出面包屑 使用默认的模板及间隔符&gt;
        /// </summary>
        public virtual void WriteCrumbs()
        {
            WriteCrumbs("&gt;");
        }

        /// <summary>
        /// 获取翻页的链接
        /// </summary>
        /// <param name="page">当前页码</param>
        /// <returns></returns>
        public override string GetPagerUrl(object page)
        {
            int int_page = Convert.ToInt32(page);
            if (int_page == 0)
                return "javascript:;";
            if (WebsiteInfo.SetHtmlOn)
                return string.Format("/html/{0}/{1}_{2}.html", PageType, SortID, page);
            return CommonUtility.GetPagerUrl(SortID, int_page);
        }

        #endregion

    }
}