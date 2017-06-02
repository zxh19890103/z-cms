using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace Nt.Model
{
    public class Nt_NewsCategory : BaseTreeModel, ILocaleModel
    {
        public int ClickRate { get; set; }
        public bool Display { get; set; }
        public int DisplayOrder { get; set; }
        public int Language_Id { get; set; }

        public string Title { get; set; }
        public string Description { get; set; }
        public string PictureUrl { get; set; }
        public string Url { get; set; }
        public string EnTitle { get; set; }
    }
}
