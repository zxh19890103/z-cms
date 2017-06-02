using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace Nt.Model
{
    public class BaseLocaleModel:BaseViewModel,ILocaleModel
    {
        public int Language_Id { get; set; }
    }
}
