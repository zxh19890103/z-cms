using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace Nt.Model
{
    /// <summary>
    /// has tree view,like nt_navigation
    /// </summary>
    public class BaseTreeModel:BaseViewModel
    {
        public string Crumbs { get; set; }
        public string Name { get; set; }
        public int Parent { get; set; }
        public int Depth { get; set; }
    }
}
