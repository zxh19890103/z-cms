using Nt.Framework;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace Nt.Pages.Product.Uc
{
    public class UcProductField : NtUserControl
    {
        DataTable _data;
        public DataTable DataSource
        {
            get { return _data; }
            set
            {
                _data = value;
            }
        }
    }
}
