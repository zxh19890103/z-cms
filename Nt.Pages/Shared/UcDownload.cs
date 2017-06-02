using Nt.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Nt.Pages.Shared
{
    public class UcDownload : NtUserControl
    {
        public string FileUrl { get; set; }
        public long FileSize { get; set; }
    }
}
