using Nt.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.UI.WebControls;

namespace Nt.Pages.Shared
{
    public class UcSearchForm : NtUserControl
    {
        List<ListItem> _categories = null;
        public List<ListItem> Categories
        {
            get
            {
                if (!string.IsNullOrEmpty(Request.QueryString[ConstStrings.SEARCH_CATEGORY]))
                {
                    int c = Convert.ToInt32(Request.QueryString[ConstStrings.SEARCH_CATEGORY]);
                    NtUtility.ListItemSelect(_categories, c);
                }
                return _categories;
            }
            set
            {
                _categories = new List<ListItem>();
                _categories.Add(new ListItem("根级", "0"));
                _categories.AddRange(value);
            }
        }

        public string SearchTitle { get { return Request.QueryString[ConstStrings.SEARCH_TITLE]; } }
    }
}
