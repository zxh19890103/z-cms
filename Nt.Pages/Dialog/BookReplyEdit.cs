using Nt.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Nt.Pages.Dialog
{
    public class BookReplyEdit : NtUserControl
    {
        public int BookID
        {
            get
            {
                return Convert.ToInt32(Request.QueryString["Book_Id"]);
            }
        }
    }
}
