using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace Nt.Web
{
    /// <summary>
    /// IPageWithCategory 的摘要说明
    /// </summary>
    public interface IPageWithCategory
    {
        int SortID { get; set; }
        string CurrentCategoryName { get; set; }

        DataTable Categories { get; }

        void RenderCatalog(int sortid, string outerTag, string liTag, string currentStyle, string liTemplate, int depth, object wrapAttrs);
    }
}