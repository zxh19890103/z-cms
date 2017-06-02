using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace Nt.Model
{
    public class Nt_Resume : BaseViewModel
    {
        public string Name { get; set; }
        public bool Gender { get; set; }
        public string Height { get; set; }
        public DateTime BirthDay { get; set; }
        public string HomeAddress { get; set; }
        public int MarritalStatus { get; set; }
        public string GraduatedFrom { get; set; }
        public DateTime GraduatedDate { get; set; }
        public DateTime BeginWorkDate { get; set; }
        public string Address { get; set; }
        public string MobilePhone { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public int EduDegree { get; set; }
        public string Work_History { get; set; }
        public string Salary { get; set; }
        public string Major { get; set; }
        public string Proffession { get; set; }
        public string ZipCode { get; set; }
        public int Status { get; set; }
        public string ReplyContent { get; set; }
        public DateTime ReplyDate { get; set; }
        public string AttachedObject { get; set; }
        public DateTime AddDate { get; set; }
        public string Note { get; set; }
        public int Job_Id { get; set; }
    }
}
