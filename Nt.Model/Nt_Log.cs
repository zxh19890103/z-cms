using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace Nt.Model
{
    public class Nt_Log : BaseViewModel
    {
        public int UserID { get; set; }
        public string LoginIP { get; set; }
        public string Description { get; set; }
        public DateTime AddDate { get; set; }
    }
}
