using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace Nt.Model
{
    public class Nt_Product : BaseLocaleModel
    {
        public string Title { get; set; }
        public string Short { get; set; }
        public string Body { get; set; }
        public int ClickRate { get; set; }
        public bool SetTop { get; set; }
        public bool Recommended { get; set; }
        public string HtmlPath { get; set; }
        public string MetaKeyWords { get; set; }
        public string MetaDescription { get; set; }
        public DateTime EditDate { get; set; }
        public DateTime AddDate { get; set; }
        public bool Display { get; set; }
        public int DisplayOrder { get; set; }
        public int DownloadedRate { get; set; }
        public long FileSize { get; set; }
        public string FileUrl { get; set; }
        public bool IsDownloadable { get; set; }
        public int ProductCategory_Id { get; set; }
        public string PictureIds { get; set; }
        public string ThumbnailUrl { get; set; }
        public int ThumbnailID { get; set; }


        public string Poster { get; set; }
        public string ManNameTeam { get; set; }

    }
}
