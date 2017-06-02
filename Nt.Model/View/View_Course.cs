using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Nt.Model.View
{
    public class View_Course : Nt_Course, IView
    {
        //CategoryID, [ParentCategoryId],[Crumbs],[Name],[FitPeople],[EduTeachers],[CourseIntro],[EduAim]
        public int CategoryID { get; set; }
        public string CategoryCrumbs { get; set; }
        public string CategoryName { get; set; }
        public string FitPeople { get; set; }
        public string EduTeachers { get; set; }
        public string CourseType { get; set; }
        public string EduAim { get; set; }
    }
}
