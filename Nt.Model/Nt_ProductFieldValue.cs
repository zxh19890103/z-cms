using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace Nt.Model
{
    public class Nt_ProductFieldValue : BaseViewModel
    {
        public int Product_Id { get; set; }
        public int ProductField_Id { get; set; }
        public string Value { get; set; }
    }
}
