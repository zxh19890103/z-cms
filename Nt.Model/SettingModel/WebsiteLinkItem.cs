using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Nt.Model.SettingModel
{
    public class WebsiteLinkItem : BaseSettingModel
    {
        public int Id { get; set; }
        public string Word { get; set; }
        public string Url { get; set; }
    }
}
