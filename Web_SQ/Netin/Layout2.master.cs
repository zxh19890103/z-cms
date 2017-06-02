using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Admin_Layout2 : System.Web.UI.MasterPage
{
    public string PageTitle
    {
        get
        {
            return (Page as Nt.Framework.NtPage).PageTitle;
        }
    }

    protected override void OnLoad(EventArgs e)
    {
        Nt.Framework.NtPage page = Page as Nt.Framework.NtPage;
        if (!Nt.BLL.NtContext.Current.Logined())
        {
            page.CloseWindow("登录超时!");
        }
        base.OnLoad(e);
    }
}
