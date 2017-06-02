<%@ WebHandler Language="C#" Class="DownloadHandler" %>

using System;
using System.Web;
using System.IO;
using Nt.DAL.Helper;

public class DownloadHandler : Nt.Web.BaseHandler
{
    protected override void Handle()
    {
        string redirectUrl = Request["redirectUrl"];
        if (string.IsNullOrEmpty(redirectUrl))
        {
            redirectUrl = Nt.Web.NtConfig.CurrentTemplatesPath;
        }
        string id = Request["fileID"];
        int int_id = 0;
        if (id == "" || !Int32.TryParse(id, out int_id))
        {
            AppendMessage("参数错误!");
            Alert(redirectUrl);
        }

        object filePath = SqlHelper.ExecuteScalar("Select FileUrl From Nt_Product Where IsDownloadable=1 And Display=1 And ID=" + id);
        if (filePath == null)
        {
            AppendMessage("记录不存在或不允许下载该文件!");
            Alert(redirectUrl);
        }

        string phy_filePath = Nt.BLL.Helper.WebHelper.MapPath(filePath.ToString());//路径

        //以字符流的形式下载文件
        FileStream fs = new FileStream(phy_filePath, FileMode.Open);
        byte[] bytes = new byte[(int)fs.Length];
        fs.Read(bytes, 0, bytes.Length);
        fs.Close();
        //通知浏览器下载文件而不是打开
        string name = phy_filePath.Substring(phy_filePath.LastIndexOf('/') + 1);
        Response.ContentType = "application/octet-stream";
        Response.AddHeader("Content-Disposition", "attachment;  filename=" + HttpUtility.UrlEncode(name, System.Text.Encoding.UTF8));
        Response.BinaryWrite(bytes);
        Response.Flush();
        Response.End();
        base.Handle();
    }
}