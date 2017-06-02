using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace Nt.Model
{
    public class Nt_News:BaseLocaleModel
    {
        public string Title { get; set; }
        public string Author { get; set; }
        public string Source { get; set; }
        public string Short { get; set; }
        public string Body { get; set; }
        public int ClickRate { get; set; }
        public bool SetTop { get; set; }
        public bool Recommended { get; set; }
        public string HtmlPath { get; set; }
        public string FirstPicture { get; set; }
        public string MetaKeyWords { get; set; }
        public string MetaDescription { get; set; }
        public DateTime EditDate { get; set; }
        public DateTime AddDate { get; set; }
        public bool Display { get; set; }
        public int DisplayOrder { get; set; }
        public int NewsCategory_Id { get; set; }


        public string NewsPicture { get; set; }
        public string Title2 { get; set; }
        public string Title3 { get; set; }
        public string Title4 { get; set; }
    }
}
