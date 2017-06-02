using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Nt.Model.SettingModel
{
    public class BookSettings : BaseSettingModel
    {
        public bool EnableVerification { get; set; }
        public bool EnableCheckCode { get; set; }
        public bool EnableFloatQueryBox { get; set; }
        public bool EnableSendEmail { get; set; }
        public string EmailAddressToReceiveEmail { get; set; }
        public string EmailToName { get; set; }

        public bool FiltrateUrl { get; set; }
        public bool FiltrateSensitiveWords { get; set; }

        public string SensitiveWords { get; set; }
    }
}
