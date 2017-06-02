using Nt.BLL;
using Nt.Framework;
using Nt.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Nt.Pages.Course
{
    public class Category:NtPageForListAsTree<Nt_CourseCategory>
    {
        public override PermissionRecord CurrentPermissionRecord
        {
            get
            {
                return PermissionRecordProvider.CourseCategoryManage;
            }
        }
    }
}
