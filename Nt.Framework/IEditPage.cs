using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Nt.Framework
{
    public interface IEditPage
    {
        string ListUrl { get; set; }
        bool EnsureEdit { get; }
    }
}
