using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace Nt.Model
{
    public class Nt_Language:BaseViewModel
    {
        public string LanguageCode { get; set; }
        public string ResxPath { get; set; }
        public bool Published { get; set; }
        public int DisplayOrder { get; set; }
        public string Name { get; set; }
    }
}
