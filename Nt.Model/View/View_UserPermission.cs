using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Nt.Model.View
{
    public class View_UserPermission : Nt_Permission, IView
    {
        public int UserLevel_Id { get; set; }
    }
}
