using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Web.UI.WebControls;

namespace Nt.Framework
{
    public class NtPager
    {
        #region Props

        private bool _initialized = false;
        public bool Initialized
        {
            get { return _initialized; }
        }

        /// <summary>
        /// 需要外部传入
        /// </summary>
        private int _total_records = 0;
        public int TotalRecords
        {
            get { return _total_records; }
            set
            {
                _total_records = value;
                InitPager();
                _initialized = true;
            }
        }

        private int _page_index = 1;
        public int PageIndex
        {
            get
            {
                return _page_index;
            }
        }

        private int _page_count = 0;
        public int PageCount
        {
            get
            {
                return _page_count;
            }
        }


        public int PageSize
        {
            get { return Convert.ToInt32(ConfigurationManager.AppSettings["admin-pageSize"]); }
        }

        private int _home_page;
        public int HomePage
        {
            get
            {
                return _home_page;
            }
        }

        private int _next_page;
        public int NextPage
        {
            get
            {
                return _next_page;
            }
        }

        private int _pre_page;
        public int PrePage
        {
            get
            {
                return _pre_page;
            }
        }

        private int _end_page;
        public int EndPage
        {
            get
            {
                return _end_page;
            }
        }

        private IList<ListItem> _pager = null;
        public IList<ListItem> Pager
        {
            get { return _pager; }
        }

        #endregion

        /// <summary>
        /// 初始化页码
        /// </summary>
        protected void InitPager()
        {
            _page_count = NtUtility.GetPageCount(TotalRecords, PageSize);
            if (!Int32.TryParse(System.Web.HttpContext.Current.Request["Page"], out _page_index))
                _page_index = 1;
            if (_page_count <= 1)
            {
                _home_page = Int32.MinValue;
                _pre_page = Int32.MinValue;
                _end_page = Int32.MinValue;
                _next_page = Int32.MinValue;
            }
            else
            {
                if (_page_index <= 1)
                {
                    _home_page = Int32.MinValue;
                    _pre_page = Int32.MinValue;
                    _end_page = _page_count;
                    _next_page = _page_index + 1;
                }
                else if (_page_index < _page_count)
                {
                    _home_page = 1;
                    _pre_page = _page_index - 1;
                    _end_page = _page_count;
                    _next_page = _page_index + 1;
                }
                else
                {
                    _home_page = 1;
                    _pre_page = _page_index - 1;
                    _end_page = Int32.MinValue;
                    _next_page = Int32.MinValue;
                }
            }

            _pager = new List<ListItem>();
            for (int i = 1; i <= _page_count; i++)
            {
                var item = new ListItem(i.ToString(), i.ToString());
                if (_page_index == i)
                    item.Selected = true;
                _pager.Add(item);
            }
        }

    }
}
