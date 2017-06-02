using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Nt.Model.View
{
    public class View_News : Nt_News, IView
    {
        public string CategoryCrumbs { get; set; }
        public string CategoryName { get; set; }
    }
}
