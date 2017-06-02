using Nt.DAL;
using Nt.DAL.Helper;
using Nt.Model;
using Nt.Model.View;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace Nt.Web
{
    public class NewsDetail : DetailPageWithCategory<View_News>
    {
        public override void Seo()
        {
            PageTitle = Model.Title;
            Description = Model.MetaDescription;
            Keywords = Model.MetaKeyWords;
        }

        public override void TryGetModel()
        {
            base.TryGetModel();
            Rating();
            SortID = Model.NewsCategory_Id;
            Crumbs = CommonFactoryAsTree.GetCrumbs<Nt_NewsCategory>(Model.NewsCategory_Id);
            Model.Short = CommonUtility.TextAreaToHtml(Model.Short);
        }

        public override int PageType
        {
            get
            {
                return NtConfig.NEWS;
            }
        }
    }
}
