using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Nt.Model.View
{
    public class View_ProductField : Nt_ProductField, IView
    {
        public string ProductCategoryCrumbs { get; set; }
    }
}
