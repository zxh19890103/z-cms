using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Nt.Model
{
    public class Nt_CourseCategory:BaseTreeModel,ILocaleModel
    {
        public bool Display { get; set; }
        public int DisplayOrder { get; set; }
        public string FitPeople { get; set; }
        public string EduAim { get; set; }
        public string EduTeachers { get; set; }
        public string CourseType { get; set; }
        public int Language_Id { get; set; }
        public int ClickRate { get; set; }
    }
}
