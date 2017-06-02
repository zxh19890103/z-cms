using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Nt.Model.View
{
    public class View_Product : Nt_Product, IView
    {
        public string CategoryCrumbs { get; set; }
        public string CategoryName { get; set; }
    }
}
