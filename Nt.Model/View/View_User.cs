using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Nt.Model.View
{
    public class View_User : Nt_User, IView
    {
        public string UserLevelName { get; set; }
    }
}
