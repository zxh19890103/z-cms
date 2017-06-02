using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace Nt.Model
{
    public class Nt_Message : BaseLocaleModel
    {
        public string Title { get; set; }
        public string Body { get; set; }
        public string LinkMan { get; set; }
        public int Status { get; set; }
        public int SetTop { get; set; }
        public bool Display { get; set; }
        public int DisplayOrder { get; set; }
        public DateTime AddDate { get; set; }
        public string Note { get; set; }
        public int Member_Id { get; set; }
        public int Type { get; set; }
    }
}
