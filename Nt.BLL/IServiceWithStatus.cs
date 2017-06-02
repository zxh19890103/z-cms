using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.UI.WebControls;

namespace Nt.BLL
{
    public interface IServiceWithStatus
    {
        string GetStatusName(object value);
    }
}
