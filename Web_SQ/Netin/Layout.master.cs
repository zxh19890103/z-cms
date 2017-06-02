using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Admin_Layout : System.Web.UI.MasterPage
{
    public string PageTitle
    {
        get
        {
            return (Page as Nt.Framework.NtPage).PageTitle;
        }
    }
}
