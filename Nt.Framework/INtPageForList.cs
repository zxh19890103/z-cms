using Nt.BLL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Web.UI.WebControls;

namespace Nt.Framework
{
    public interface INtPageForList
    {
        NtPager Pager { get; }

        bool NeedPagerize { get; set; }

        DataTable DataSource { get; set; }

        Repeater Repeater { get; set; }
    }
}