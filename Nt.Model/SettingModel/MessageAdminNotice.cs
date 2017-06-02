using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Nt.Model.SettingModel
{
    public class MessageAdminNotice : BaseSettingModel
    {
        public string Content { get; set; }
        public string Title { get; set; }
        public DateTime AddDate { get; set; }
    }
}
