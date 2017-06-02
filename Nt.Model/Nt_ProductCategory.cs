using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace Nt.Model
{
    public class Nt_ProductCategory : BaseTreeModel, ILocaleModel
    {
        public int ClickRate { get; set; }
        public bool Display { get; set; }
        public int DisplayOrder { get; set; }
        public bool IsDownloadable { get; set; }
        public int Language_Id { get; set; }
    }
}
