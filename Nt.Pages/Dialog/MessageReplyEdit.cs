﻿using Nt.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Nt.Pages.Dialog
{
    public class MessageReplyEdit : NtUserControl
    {
        public int MessageID
        {
            get
            {

                return Convert.ToInt32(Request.QueryString["Message_Id"]);
            }
        }
    }
}
