using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace Nt.Model
{
    public class Nt_SinglePage : BaseLocaleModel
    {
        public string Title { get; set; }
        public string Short { get; set; }
        public string Body { get; set; }
        public string HtmlPath { get; set; }
        public string FirstPicture { get; set; }
        public string MetaKeyWords { get; set; }
        public string MetaDescription { get; set; }
        public bool Display { get; set; }
    }
}
