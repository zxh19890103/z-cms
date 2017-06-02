using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace Nt.Model
{
    public class Nt_MemberRole:BaseViewModel
    {
        public string Description { get; set; }
        public bool Active { get; set; }
        public string Name { get; set; }
        public string SystemName { get; set; }
    }
}
