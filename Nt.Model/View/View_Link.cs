using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Nt.Model.View
{
    public class View_Link : Nt_Link, IView
    {
        public string PictureUrl { get; set; }
    }
}
