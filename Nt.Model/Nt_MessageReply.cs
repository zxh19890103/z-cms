using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace Nt.Model
{
    public class Nt_MessageReply:BaseViewModel
    {
        public string Body { get; set; }
        public string ReplyMan { get; set; }
        public DateTime ReplyDate { get; set; }
        public bool Display { get; set; }
        public int DisplayOrder { get; set; }
        public DateTime AddDate { get; set; }
        public string Note { get; set; }
        public int Message_Id { get; set; }
    }
}
