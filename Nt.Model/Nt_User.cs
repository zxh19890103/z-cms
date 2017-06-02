using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace Nt.Model
{
    public class Nt_User : BaseViewModel
    {
        public string UserName { get; set; }
        public string Password { get; set; }
        public string Description { get; set; }
        public bool Active { get; set; }
        public string AddUser { get; set; }
        public DateTime AddDate { get; set; }
        public int UserLevel_Id { get; set; }
    }
}
