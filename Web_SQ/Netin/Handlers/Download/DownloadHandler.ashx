<%@ WebHandler Language="C#" Class="DownloadHandler" %>

using System;
using System.Collections;
using System.Web;
using System.IO;
using System.Globalization;
using LitJson;

public class DownloadHandler : IHttpHandler
{

    private HttpContext context;

    public void ProcessRequest(HttpContext ctext)
    {
        this.context = ctext;
        string method = context.Request["method"];
        if (!string.IsNullOrEmpty(method)
            && method == "del")//删除
        {
            DelFile(context.Request["fileUrl"]);
        }

        DelFile(context.Request["fileUrl"]);

        String aspxUrl = context.Request.Path.Substring(0, context.Request.Path.LastIndexOf("/") + 1);

        //文件保存目录路径
        String savePath = "/Upload/";

        //文件保存目录URL
        String saveUrl = "/Upload/";

        //定义允许上传的文件扩展名

        string allowedFormats = "doc,docx,xls,xlsx,ppt,htm,html,txt,zip,rar,gz,bz2";

        //最大文件大小
        int maxSize = 1024 * 1024;

        HttpPostedFile downloadFile = context.Request.Files["imgFile"];
        if (downloadFile == null)
        {
            showError("请选择文件。");
        }

        String dirPath = context.Server.MapPath(savePath);

        if (!Directory.Exists(dirPath))
        {
            showError("上传目录不存在。");
        }

        String dirName = "Download";

        String fileName = downloadFile.FileName;
        String fileExt = Path.GetExtension(fileName).ToLower();

        if (downloadFile.InputStream == null || downloadFile.InputStream.Length > maxSize)
        {
            showError("上传文件大小超过限制。");
        }

        if (String.IsNullOrEmpty(fileExt) || Array.IndexOf((allowedFormats).Split(','), fileExt.Substring(1).ToLower()) == -1)
        {
            showError("上传文件扩展名是不允许的扩展名。\n只允许" + (allowedFormats) + "格式。");
        }

        //创建文件夹
        dirPath += dirName + "/";
        saveUrl += dirName + "/";
        if (!Directory.Exists(dirPath))
        {
            Directory.CreateDirectory(dirPath);
        }
        String ymd = DateTime.Now.ToString("yyyyMMdd", DateTimeFormatInfo.InvariantInfo);
        dirPath += ymd + "/";
        saveUrl += ymd + "/";
        if (!System.IO.Directory.Exists(dirPath))
        {
            Directory.CreateDirectory(dirPath);
        }

        String newFileName = DateTime.Now.ToString("yyyyMMddHHmmss_ffff", System.Globalization.DateTimeFormatInfo.InvariantInfo) + fileExt;
        String filePath = dirPath + newFileName;

        downloadFile.SaveAs(filePath);

        String fileUrl = saveUrl + newFileName;
        int fileSize = downloadFile.ContentLength;

        Hashtable hash = new Hashtable();
        hash["error"] = 0;
        hash["FileUrl"] = fileUrl;
        hash["FileSize"] = fileSize;
        context.Response.AddHeader("Content-Type", "text/html; charset=UTF-8");
        context.Response.Write(JsonMapper.ToJson(hash));
        context.Response.End();
    }

    /// <summary>
    /// 删除文件
    /// </summary>
    /// <param name="url">文件的绝对路径</param>
    void DelFile(string url)
    {
        if (!string.IsNullOrEmpty(url))
        {
            string phy_path = Nt.BLL.Helper.WebHelper.MapPath(url);
            if (File.Exists(phy_path))
            {
                File.Delete(phy_path);
            }
            Hashtable hash = new Hashtable();
            hash["error"] = 0;
            context.Response.AddHeader("Content-Type", "text/html; charset=UTF-8");
            context.Response.Write(JsonMapper.ToJson(hash));
            context.Response.End();
        }
    }


    private void showError(string message)
    {
        Hashtable hash = new Hashtable();
        hash["error"] = 1;
        hash["message"] = message;
        context.Response.AddHeader("Content-Type", "text/html; charset=UTF-8");
        context.Response.Write(JsonMapper.ToJson(hash));
        context.Response.End();
    }

    public bool IsReusable
    {
        get
        {
            return false;
        }
    }
}