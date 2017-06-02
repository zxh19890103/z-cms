using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Nt.Model
{
    public class Nt_Course:BaseLocaleModel
    {
        public string Title { get; set; }
        public string Body { get; set; }
        public string Short { get; set; }
        public int ClickRate { get; set; }
        public bool SetTop { get; set; }
        public bool Recommended { get; set; }
        public bool Display { get; set; }
        public string HtmlPath { get; set; }
        public string MetaKeywords { get; set; }
        public string  MetaDescription { get; set; }
        public DateTime EditDate { get; set; }
        public DateTime AddDate { get; set; }
        public int DisplayOrder { get; set; }
        public int CourseCategory_Id { get; set; }
        public DateTime CourseStartDate { get; set; }
        public string CourseDuration { get; set; }
        public string CourseTimeSpan { get; set; }
        public string CourseTeachers { get; set; }
        public string CourseBooks { get; set; }
        public string CourseTarget { get; set; }
    }
}
