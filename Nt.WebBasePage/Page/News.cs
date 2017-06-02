using Nt.DAL;
using Nt.Model;
using Nt.Model.View;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Nt.Web
{
    public class News : ListPageWithCategory<View_News>
    {
        public override bool TryGetList()
        {
            HandlePageSize("news");
            Crumbs = CommonFactoryAsTree.GetCrumbs<Nt_NewsCategory>(SortID);
            bool f = base.TryGetList();
            if (!f)
                return false;
            foreach (DataRow r in DataList.Rows)
            {
                if (r["FirstPicture"].ToString() == ""
                    && ConfigurationManager.AppSettings["news.first.picture.url.if.not.found"] != null)
                {
                    r["FirstPicture"] = ConfigurationManager.AppSettings["news.first.picture.url.if.not.found"];
                }
            }
            return f;
        }

        public override int PageType
        {
            get
            {
                return NtConfig.NEWS_LIST;
            }
        }
    }
}
