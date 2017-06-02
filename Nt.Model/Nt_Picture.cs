using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace Nt.Model
{
    public class Nt_Picture : BaseViewModel
    {
        public string SeoAlt { get; set; }
        public string PictureUrl { get; set; }
        public bool Display { get; set; }
        public int DisplayOrder { get; set; }
        public string Title { get; set; }
        public string Text { get; set; }
    }
}
