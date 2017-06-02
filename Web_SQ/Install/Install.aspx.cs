using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using Nt.BLL;
using System.IO;
using Nt.BLL.Helper;

public partial class Install_Install : System.Web.UI.Page
{
    protected override void OnPreInit(EventArgs e)
    {
        base.OnPreInit(e);
        bool dbInstalled = false;
        if (File.Exists(MapPath("/App_Data/Connection.txt")))
            dbInstalled = true;
        if (dbInstalled)
        {
            Response.Redirect("/", true);
        }

        DataSource.Text = WebHelper.GetIP();
    }

    protected void Install_Click(object sender, EventArgs e)
    {
        try
        {
            string dbname = DbName.Text.Trim();
            string server = DataSource.Text.Trim();
            bool useWindows = UseWindowsAuthentication.Checked;
            InstallService service;
            if (useWindows)
                service = new InstallService(dbname, server);
            else
            {
                string userid = UserID.Text.Trim();
                string pass = Password.Text.Trim();
                service = new InstallService(dbname, server, userid, pass);
            }
            service.DbExisting = DbExisting.Checked;
            service.Install();

            Message.Text = "安装成功!";
        }
        catch (Exception ex)
        {
            Message.Text = ex.Message;
        }
    }
}