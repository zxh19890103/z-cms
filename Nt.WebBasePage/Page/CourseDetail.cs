using Nt.DAL;
using Nt.Model;
using Nt.Model.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Nt.Web
{
    public class CourseDetail : DetailPageWithCategory<View_Course>
    {
        public override void Seo()
        {
            PageTitle = Model.Title;
            Description = Model.MetaDescription;
            Keywords = Model.MetaKeywords;
        }

        public override void TryGetModel()
        {
            base.TryGetModel();
            Rating();
            SortID = Model.CourseCategory_Id;
            Crumbs = CommonFactoryAsTree.GetCrumbs<Nt_CourseCategory>(Model.CourseCategory_Id);
            Model.Short = CommonUtility.TextAreaToHtml(Model.Short);
        }
        
        public override int PageType
        {
            get
            {
                return NtConfig.COURSE;
            }
        }
    }
}
