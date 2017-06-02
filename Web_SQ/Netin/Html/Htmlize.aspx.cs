using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Nt.Framework;
using Nt.BLL;
using Nt.Model;
using System.Web.Services;
using System.IO;
using Nt.BLL.Helper;
using Nt.DAL.Helper;
using Nt.Web;

public partial class Admin_Html_Htmlize : NtPage
{
    public override PermissionRecord CurrentPermissionRecord
    {
        get
        {
            return PermissionRecordProvider.HtmliztionManage;
        }
    }

    [WebMethod]
    public static string Htmlize(string type)
    {
        int t = Convert.ToInt32(type);
        Htmlizer.Instance.CurrenType = t;
        Htmlizer.Instance.Run();
        return string.Empty;
    }

    [WebMethod]
    public static string DeHtmlize(string type)
    {
        string _message = "";
        int _error = 0;
        string tab = "";
        try
        {
            switch (type)
            {
                case "0":
                    foreach (string file in Directory.GetFiles(WebHelper.MapPath("/html/"),
                        "*.html", SearchOption.TopDirectoryOnly))
                    {
                        File.Delete(file);
                    }
                    SqlHelper.ExecuteNonQuery("update nt_navigation set htmlpath='' ");
                    _message = "导航静态文件已经全部删除!";
                    break;
                case "1":
                    string filepath = WebHelper.MapPath("/index.html");
                    if (File.Exists(filepath))
                        File.Delete(filepath);
                    _message = "首页静态文件已经删除!";
                    break;
                case "2":
                    tab = "Nt_News";
                    break;
                case "3":
                case "4":
                    tab = "Nt_Product";
                    break;
                case "5":
                    tab = "Nt_Course";
                    break;
                case "6":
                    tab = "Nt_SinglePage";
                    break;
                case "7":
                    tab = "Nt_Job";
                    break;
                default:
                    _error = 1;
                    _message = "参数错误!";
                    break;
            }

            //导航静态和首页静态不在此做清除工作
            if (type == "0" || type == "1")
            { }
            else
            {
                string dir = WebHelper.MapPath("/html/" + type + "/");
                if (Directory.Exists(dir))
                {
                    foreach (string file in Directory.GetFiles(dir,
                              "*.html",
                              SearchOption.TopDirectoryOnly))
                    {
                        File.Delete(file);
                    }
                }
                if (tab != "")
                {
                    //产品
                    if (type == "3")
                        SqlHelper.ExecuteNonQuery("update " + tab + " set htmlpath='' Where isdownloadable=0 ");
                    else if (type == "4")//下载
                        SqlHelper.ExecuteNonQuery("update " + tab + " set htmlpath='' Where isdownloadable=1 ");
                    else
                        SqlHelper.ExecuteNonQuery("update " + tab + " set htmlpath='' ");
                }
                _message = "静态文件已经全部删除!";
            }

        }
        catch (Exception ex)
        {
            _error = 1;
            _message = ex.Message;
        }
        return new NtJson(new { error = _error, message = _message }).ToString();
    }
    
    protected override void OnLoad(EventArgs e)
    {
        PageTitle = "静态化管理";
        base.OnLoad(e);
    }
}