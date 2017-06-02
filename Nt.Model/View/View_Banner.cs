using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Nt.Model.View
{
    public class View_Banner : Nt_Banner, IView
    {
        public string PictureUrl { get; set; }
    }
}
