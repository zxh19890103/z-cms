using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace Nt.Model
{
    public class Nt_Link : BaseLocaleModel
    {
        public string Url { get; set; }
        public int Picture_Id { get; set; }
        public string Text { get; set; }
        public int ClickRate { get; set; }
        public bool Display { get; set; }
        public int DisplayOrder { get; set; }
        public DateTime AddDate { get; set; }
        public string Note { get; set; }
    }
}
