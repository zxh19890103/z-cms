using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace Nt.Model
{
    public class Nt_Email:BaseViewModel
    {
        public int Priority { get; set; }
        public string From { get; set; }
        public string FromName { get; set; }
        public string To { get; set; }
        public string ToName { get; set; }
        public string CC { get; set; }
        public string Bcc { get; set; }
        public string Subject { get; set; }
        public string Body { get; set; }
        public DateTime AddDate { get; set; }
        public int SentTries { get; set; }
        public DateTime SentDate { get; set; }
        public int EmailAccountId { get; set; }
    }
}
