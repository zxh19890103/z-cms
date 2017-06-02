using Nt.DAL;
using Nt.Model;
using Nt.Model.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Nt.Web
{
    public class Course:ListPageWithCategory<View_Course>
    {

        public override bool TryGetList()
        {
            HandlePageSize("course");
            Crumbs = CommonFactoryAsTree.GetCrumbs<Nt_CourseCategory>(SortID);
            return base.TryGetList();
        }

        public override int PageType
        {
            get
            {
                return NtConfig.COURSE_LIST;
            }
        }
    }
}
