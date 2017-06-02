using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Nt.Model
{
    public class Nt_BookReply : BaseViewModel
    {
        public string ReplyContent { get; set; }
        public int Book_Id { get; set; }
        public DateTime ReplyDate { get; set; }
        public string ReplyMan { get; set; }
        public int DisplayOrder { get; set; }
        public bool Display { get; set; }
    }
}
