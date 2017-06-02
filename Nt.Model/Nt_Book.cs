using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace Nt.Model
{
    public class Nt_Book : BaseLocaleModel
    {
        public string Title { get; set; }
        public string Name { get; set; }
        public string Tel { get; set; }
        public bool Gender { get; set; }
        public string NativePlace { get; set; }
        public string Nation { get; set; }
        public string PersonID { get; set; }
        public string EduDegree { get; set; }
        public string ZipCode { get; set; }
        public string PoliticalRole { get; set; }
        public string Address { get; set; }
        public string GraduatedFrom { get; set; }
        public string Grade { get; set; }
        public DateTime BirthDate { get; set; }
        public string Mobile { get; set; }
        public string Email { get; set; }
        public string Company { get; set; }
        public string Body { get; set; }
        public bool Display { get; set; }
        public int DisplayOrder { get; set; }
        public DateTime AddDate { get; set; }
        public string Note { get; set; }
        public int Type { get; set; }

        public string Fax { get; set; }
        public int CustomerNumber { get; set; }
    }
}
