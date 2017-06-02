using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Nt.Web
{
    /// <summary>
    /// BaseUserControlWithCategory 的摘要说明
    /// </summary>
    public class BaseUserControlWithCategory : BaseUserControl
    {
        public IPageWithCategory CurrentPageAsWithCategory { get { return Page as IPageWithCategory; } }

        public int SortID
        {
            get
            {
                return CurrentPageAsWithCategory.SortID;
            }
        }

        public void RenderCatalog(int sortid, string outerTag, string liTag,string currentStyle, string liTemplate, int depth, object wrapAttrs)
        {
            CurrentPageAsWithCategory.RenderCatalog(sortid, outerTag, liTag,currentStyle, liTemplate, depth, wrapAttrs);
        }
        
    }
}