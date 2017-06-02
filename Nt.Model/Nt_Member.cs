using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace Nt.Model
{
    public class Nt_Member:BaseViewModel
    {
        public string LoginName { get; set; }
        public string Password { get; set; }
        public string RealName { get; set; }
        public bool Sex { get; set; }
        public string Company { get; set; }
        public string Address { get; set; }
        public string ZipCode { get; set; }
        public string MobilePhone { get; set; }
        public string Phone { get; set; }
        public string Fax { get; set; }
        public string Email { get; set; }
        public bool Active { get; set; }
        public string Note { get; set; }
        public int LoginTimes { get; set; }
        public DateTime LastLoginDate { get; set; }
        public string LastLoginIP { get; set; }
        public DateTime AddDate { get; set; }
        public int MemberRole_Id { get; set; }
    }
}
