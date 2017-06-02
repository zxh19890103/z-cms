using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Nt.Model.View
{
    public class View_Member : Nt_Member, IView
    {
        public string MemberRoleName { get; set; }
    }
}
