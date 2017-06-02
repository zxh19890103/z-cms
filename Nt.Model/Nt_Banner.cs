using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace Nt.Model
{
    public class Nt_Banner:BaseLocaleModel
    {
        public int Picture_Id { get; set; }
        public string Url { get; set; }
        public string Title { get; set; }
        public string Text { get; set; }
        public bool Display { get; set; }
        public int DisplayOrder { get; set; }
        public string Note { get; set; }
    }
}
