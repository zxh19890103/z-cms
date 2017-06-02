using Nt.BLL;
using Nt.Model;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Nt.Web
{
    /// <summary>
    /// ListPage 的摘要说明
    /// </summary>
    public abstract class ListPage<M> : BasePage
        where M : BaseViewModel, new()
    {
        #region Props

        protected BaseService<M> _service;
        /// <summary>
        /// 服务类
        /// </summary>
        public BaseService<M> Service
        {
            get
            {
                if (_service == null)
                {
                    _service = NtEngine.GetService<M>();
                    _service.LanguageID = NtConfig.CurrentLanguage;
                }
                return _service;
            }
            set { _service = value; }
        }

        /// <summary>
        /// 是否检索到记录
        /// </summary>
        public bool NoRecord { get; set; }

        string _filter = "Display=1";
        /// <summary>
        /// 筛选条件
        /// </summary>
        public virtual string Filter { get { return _filter; } set { _filter = value; } }

        string _orderBy = NtConfig.CommonOrderBy;
        /// <summary>
        /// 排序
        /// </summary>
        public virtual string OrderBy { get { return _orderBy; } set { _orderBy = value; } }

        #region Pager
        /// <summary>
        /// 存放页码数据
        /// </summary>
        public List<ListItem> Pager { get; set; }
        int _pageSize = 12;
        /// <summary>
        /// 每页显示的记录条数
        /// </summary>
        public int PageSize { get { return _pageSize; }  set { _pageSize = value; } }

        int _pageIndex = 1;
        /// <summary>
        /// 当前页码
        /// </summary>
        public virtual int PageIndex
        {
            get
            {
                if (!Int32.TryParse(Request["Page"], out _pageIndex))
                {
                    _pageIndex = 1;
                }
                return _pageIndex;
            }
        }

        int _recordCount = 0;
        /// <summary>
        /// 记录的总条数
        /// </summary>
        public int RecordCount
        {
            get { return _recordCount; }
            set
            {
                NoRecord = value == 0;
                _recordCount = value;
                CalculatePager();
            }
        }

        /// <summary>
        /// 页面的个数
        /// </summary>
        public int PageCount { get; set; }

        int _pagerItemCount = 5;
        /// <summary>
        /// 每页显示的页码个数
        /// </summary>
        public int PagerItemCount { get { return _pagerItemCount; } set { _pagerItemCount = value; } }
        /// <summary>
        /// 第一页
        /// </summary>
        public int FirstPageNo { get; set; }
        /// <summary>
        /// 上一页
        /// </summary>
        public int PrePageNo { get; set; }
        /// <summary>
        /// 下一页
        /// </summary>
        public int NextPageNo { get; set; }
        /// <summary>
        /// 最后一页
        /// </summary>
        public int EndPageNo { get; set; }

        #endregion


        public override string HtmlPageUrl
        {
            get
            {
                return string.Format("/html/{0}/{1}_{2}.html", PageType, PageType, PageIndex);
            }
        }

        /// <summary>
        /// 列表数据
        /// </summary>
        public DataTable DataList { get; set; }

        #endregion

        #region methods

        /// <summary>
        /// 尝试获取列表数据
        /// </summary>
        /// <returns>true 有结果，false 无结果</returns>
        public virtual bool TryGetList()
        {
            RecordCount = Service.GetRecordsCount(Filter);
            if (!NoRecord)
            {
                DataList = Service.GetList(PageIndex, PageSize, OrderBy, Filter);
            }
            else
            {
                DataList = Service.GetList(PageIndex, 0, OrderBy, Filter);
            }
            return !NoRecord;
        }

        /// <summary>
        /// 获取列表页记录的显示条数
        /// </summary>
        /// <param name="prefix">news,product,download,course,job</param>
        protected void HandlePageSize(string prefix)
        {
            if (PageSize == 12)
            {
                string v = ConfigurationManager.AppSettings[prefix + ".list.pageSize"];
                if (v != null)
                    PageSize = Convert.ToInt32(v);
            }
            if (PagerItemCount == 5)
            {
                string v = ConfigurationManager.AppSettings[prefix + ".list.pager.count"];
                if (v != null)
                    PagerItemCount = Convert.ToInt32(v);
            }
        }

        /// <summary>
        /// 添加过滤条件
        /// </summary>
        /// <param name="addedFilter"></param>
        public void AddFilter(string addedFilter)
        {
            if (string.IsNullOrEmpty(Filter))
            {
                Filter += addedFilter;
            }
            else
                Filter += " And " + addedFilter;
        }

        /// <summary>
        /// 添加排序字段
        /// </summary>
        /// <param name="addedOrderby"></param>
        public void AddOrderBy(string addedOrderby)
        {
            if (string.IsNullOrEmpty(OrderBy))
            {
                OrderBy = addedOrderby;
            }
            else
            {
                OrderBy += ", " + addedOrderby;
            }
        }

        protected override void OnUnload(EventArgs e)
        {
            base.OnUnload(e);
            if (DataList != null) DataList.Dispose();
        }

        bool _pagerCalculated = false;
        /// <summary>
        /// 计算页码
        /// </summary>
        protected void CalculatePager()
        {
            if (_pagerCalculated)
                return;
            Pager = new List<ListItem>();
            _pagerCalculated = true;
            if (RecordCount == 0)
                return;
            PageCount = RecordCount % PageSize == 0 ? RecordCount / PageSize : RecordCount / PageSize + 1;

            if (PageIndex > PageCount)
                _pageIndex = PageCount;

            int[] IntPager = CommonUtility.GenPager(PageIndex, PageSize, PageCount, PagerItemCount);

            FirstPageNo = PageIndex < 2 ? 0 : 1;
            PrePageNo = PageIndex < 2 ? 0 : (PageIndex - 1);

            for (int i = 0; i < IntPager.Length; i++)
            {
                var pagerItem = new ListItem();
                if (PageIndex == IntPager[i])
                {
                    pagerItem.Value = "0";
                    pagerItem.Selected = true;
                }
                pagerItem.Text = IntPager[i].ToString();
                Pager.Add(pagerItem);
            }

            EndPageNo = PageIndex >= PageCount ? 0 : PageCount;
            NextPageNo = PageIndex >= PageCount ? 0 : PageIndex + 1;
        }

        /// <summary>
        /// Pager,统一 class="pager-item"
        /// </summary>
        /// <param name="currentStyle">当前选中项的样式,采用默认的列表样式li a</param>
        public virtual void RenderPager(string currentStyle)
        {
            foreach (var item in Pager)
            {
                if (item.Selected)
                    Response.Write(
                        string.Format("<li class=\"pager-item {0}\"><a href=\"javascript:;\">{1}</a></li>", currentStyle, item.Text));
                else
                    Response.Write(
                        string.Format("<li class=\"pager-item\"><a href=\"{0}\">{1}</a></li>",
                        GetPagerUrl(item.Value), item.Text));
            }
        }

        /// <summary>
        /// Pager,统一 class="pager-item"
        /// </summary>
        /// <param name="liTemplate">{url},{text},{class}</param>
        /// <param name="currentStyle">当前选中项的样式</param>
        public virtual void RenderPager(string liTemplate, string currentStyle)
        {
            foreach (var item in Pager)
            {
                if (item.Selected)
                    Response.Write(liTemplate
                        .Replace("{url}", GetPagerUrl(item.Value))
                        .Replace("{text}", item.Text)
                        .Replace("{class}", string.IsNullOrEmpty(currentStyle) ? "pager-item" : "pager-item " + currentStyle));
                else
                    Response.Write(liTemplate
                        .Replace("{url}", GetPagerUrl(item.Value))
                        .Replace("{text}", item.Text)
                        .Replace("{class}", "pager-item"));
            }
        }


        /// <summary>
        ///  输出列表
        /// {FieldName} 
        /// {FieldName,num} 截取字符串
        /// {FieldName,format} 日期
        /// {url,hrefbase} 链接
        /// </summary>
        /// <param name="liTemplate">模板</param>
        public virtual void RenderList(string liTemplate)
        {
            if (NoRecord)
                return;
            Type typeOfM = typeof(M);
            string[] props = typeOfM.GetProperties()
                .Select(x => x.Name).ToArray();
            string li = string.Empty;
            string pattern = string.Empty;
            string replacement = string.Empty;
            foreach (DataRow r in DataList.Rows)
            {
                li = liTemplate;
                foreach (string p in props)
                {
                    pattern = "{" + p + "}";
                    replacement = r[p].ToString();
                    li = Regex.Replace(li, pattern, replacement, RegexOptions.IgnoreCase);
                }
                Response.Write(li);
            }
        }

        #endregion

        #region Bind

        /// <summary>
        /// 将数据源绑定到Repeater控件
        /// </summary>
        /// <param name="data">数据源</param>
        /// <param name="repeaterId">Repeater控件的ID</param>
        public void NtBind(object data, string repeaterId)
        {
            if (data != null)
            {
                Repeater repeater = this.FindControl(repeaterId) as Repeater;
                if (repeater != null)
                {
                    repeater.DataSource = data;
                    repeater.DataBind();
                }
            }
        }

        /// <summary>
        /// 使用默认的RepeaterID值对数据源进行绑定
        /// </summary>
        /// <param name="data">数据源</param>
        public void NtBind(object data)
        {
            NtBind(data, "XRepeater");
        }

        /// <summary>
        /// 将数据绑定到指定的Repeater控件上
        /// </summary>
        /// <param name="data"></param>
        /// <param name="repeater"></param>
        public void NtBind(object data, Repeater repeater)
        {
            repeater.DataSource = data;
            repeater.DataBind();
        }

        /// <summary>
        /// 使用默认的RepeaterID(XRepeater)对数据源进行绑定
        /// </summary>
        public void NtBind()
        {
            NtBind(DataList, "XRepeater");
        }

        #endregion

        #region url config

        private string _detailPagePath;
        /// <summary>
        /// 详细页的文件的名称  XXXDetail.aspx
        /// </summary>
        public string DetailPagePath
        {
            get
            {
                if (string.IsNullOrEmpty(_detailPagePath))
                {
                    string thisPath = Request.Url.AbsolutePath;
                    _detailPagePath = thisPath.Substring(0, thisPath.Length - 5) + "Detail.aspx";
                }
                return _detailPagePath;
            }
            set { _detailPagePath = value; }
        }

        /// <summary>
        /// 获取详细页的链接路径,使用默认的详细页的文件名
        /// </summary>
        /// <param name="container">RepeaterItem</param>
        /// <returns>链接路径</returns>
        public string GetDetailUrl(RepeaterItem container)
        {
            return GetDetailUrl(container, DetailPagePath);
        }

        /// <summary>
        /// 使用提供的hrefBase(newsdetail.aspx)来获取详细页的路径
        /// </summary>
        /// <param name="container">RepeaterItem</param>
        /// <param name="hrefBase">newsdetail.aspx</param>
        /// <returns></returns>
        public string GetDetailUrl(RepeaterItem container, string hrefBase)
        {
            object id = DataBinder.Eval(container.DataItem, "ID");
            string url = "";
            if (WebsiteInfo.SetHtmlOn)
            {
                url = string.Format("/html/{0}/{1}.html", DetailPageType, id);
            }
            else
            {
                if (hrefBase.StartsWith("/"))
                    url = hrefBase + "?ID=" + id;
                else
                    url = NtConfig.CurrentTemplatesPath + hrefBase + "?ID=" + id;
            }
            return url;
        }

        /// <summary>
        /// 获取翻页的链接
        /// </summary>
        /// <param name="page">当前页码</param>
        /// <returns></returns>
        public virtual string GetPagerUrl(object page)
        {
            int int_page = Convert.ToInt32(page);
            if (int_page == 0)
                return "javascript:;";
            if (WebsiteInfo.SetHtmlOn)
            {
                return string.Format("/html/{0}/{0}_{1}.html", PageType, page);
            }
            return CommonUtility.GetPagerUrl(int_page);
        }

        public int DetailPageType
        {
            get
            {
                switch (PageType)
                {
                    case NtConfig.COURSE_LIST:
                        return NtConfig.COURSE;
                    case NtConfig.NEWS_LIST:
                        return NtConfig.NEWS;
                    case NtConfig.PRODUCT_LIST:
                        return NtConfig.PRODUCT;
                    case NtConfig.JOB_LIST:
                        return NtConfig.JOB;
                    case NtConfig.DOWNLOAD_LIST:
                        return NtConfig.DOWNLOAD;
                    default:
                        throw new Exception("所提供的页面类型为非列表型!");
                }
            }
        }

        #endregion

    }
}