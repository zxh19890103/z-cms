using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Nt.Model
{
    public class Nt_ProductField:BaseViewModel
    {
        public int ProductCategory_Id { get; set; }
        public string Name { get; set; }
        public bool Display { get; set; }
        public int DisplayOrder { get; set; }
    }
}
