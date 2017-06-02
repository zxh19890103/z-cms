using Nt.BLL;
using Nt.BLL.Helper;
using Nt.DAL;
using Nt.DAL.Helper;
using Nt.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;

namespace Nt.Web
{
    /// <summary>
    /// DetailPageWithCategory 的摘要说明
    /// </summary>
    public class DetailPageWithCategory<M> : DetailPage<M>, IPageWithCategory
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
                return _sortId;
            }
            set
            {
                _sortId = value;
            }
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
        /// 存储类别信息的数据表
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

        private string _listPagePath = string.Empty;
        /// <summary>
        /// 列表页路径
        /// </summary>
        public string ListPagePath
        {
            get
            {
                if (string.IsNullOrEmpty(_listPagePath))
                {
                    string thisPath = Request.Url.AbsolutePath;
                    if (thisPath.EndsWith("detail.aspx", StringComparison.OrdinalIgnoreCase))
                    {
                        _listPagePath = thisPath.Substring(0, thisPath.Length - 11) + ".aspx";
                    }
                }
                return _listPagePath;
            }
            set
            {
                _listPagePath = value;
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
                        @"href=""([^""]+)""", string.Format("href=\"/html/{0}/{1}_1.html\"", ListPageType, item["Id"]));
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

        #endregion

        #region 上一页和下一页

        /// <summary>
        /// 上一页和下一页（好用的，仅仅适用于新闻和产品，课程，下载）
        /// </summary>
        public virtual void CalculateNextAndPrevious()
        {
            string order = NtConfig.CommonOrderBy + ", id desc";
            string filter = " categorycrumbs like '%," + SortID + ",%'";
            CalculateNextAndPrevious(order, filter);
        }

        #endregion

        #region 输出面包屑导航

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
                         @"href=""([^""]+)""", string.Format("href=\"/html/{0}/{1}_1.html\"", ListPageType, item.Value));
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
        /// 使用常用模板
        /// </summary>
        /// <param name="separator"></param>
        public virtual void WriteCrumbs(string separator)
        {
            WriteCrumbs(separator, "<a href=\"" + ListPagePath + "?sortid={value}\">{text}</a>");
        }

        public virtual void WriteCrumbs()
        {
            WriteCrumbs("&gt;", "<a href=\"" + ListPagePath + "?sortid={value}\">{text}</a>");
        }

        #endregion


    }
}