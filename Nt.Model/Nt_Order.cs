using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace Nt.Model
{
    public class Nt_Order : BaseViewModel
    {
        public string OrderCode { get; set; }
        public string Title { get; set; }
        public string LinkMan { get; set; }
        public string ReplyContent { get; set; }
        public DateTime ReplyDate { get; set; }
        public int Status { get; set; }
        public DateTime EditDate { get; set; }
        public DateTime AddDate { get; set; }
        public string Note { get; set; }
        public int Member_Id { get; set; }
    }
}
