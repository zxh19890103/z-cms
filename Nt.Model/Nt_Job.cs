using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace Nt.Model
{
    public class Nt_Job:BaseLocaleModel
    {
        public string JobName { get; set; }
        public int RecruitCount { get; set; }
        public string Salary { get; set; }
        public string Duties { get; set; }
        public string Requirements { get; set; }
        public string Hr { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public int ClickRate { get; set; }
        public string WorkPlace { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public bool Display { get; set; }
        public int DisplayOrder { get; set; }
        public DateTime AddDate { get; set; }
        public string Note { get; set; }
        public string HtmlPath { get; set; }
        public int Type { get; set; }
    }
}
