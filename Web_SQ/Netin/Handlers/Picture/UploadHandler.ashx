<%@ WebHandler Language="C#" Class="UploadHandler" %>

using System;
using System.Web;
using Nt.BLL;
using Nt.Framework;
using Nt.Model;
using Nt.BLL.Extension;

public class UploadHandler : BaseHttpHandler
{

    const string DELETE_PICTURE = "del";
    const string UPLOAD = "upload";

    PictureService service = null;
    int pictureID = 0;

    public string Method { get { return Request["method"]; } }
    public int PictureID { get { return pictureID; } }

    protected override void Handle()
    {
        service = new PictureService();
        pictureID = Convert.ToInt32(Request["Picture_Id"]);
        switch (Method)
        {
            case DELETE_PICTURE:
                Del();
                break;
            case UPLOAD:
                Upload();
                break;
            default:
                Error("未执行任何操作!");
                break;
        }
    }

    void Del()
    {
        if (PictureID > 0)
        {
            service.Delete(pictureID);
            Success("图片已经被删除!");
            ResponseJson["PictureUrl"] = PictureService.NO_IMAGE;
            ResponseJson["Picture_Id"] = 0;
        }
        else
        {
            Error("图片不存在!");
        }
    }

    void Upload()
    {
        try
        {
            string pictureUrl = service.AsyncUpload();
            if (pictureID > 0)
                service.Delete(pictureID);
            Nt_Picture m = new Nt_Picture();
            m.InitData();
            m.PictureUrl = pictureUrl;
            pictureID = service.Insert(m);
            ResponseJson["Picture_Id"] = pictureID;
            ResponseJson["PictureUrl"] = service.GetPictureUrl(pictureUrl, SinglePictureThumnailSize, true);
            Success("上传成功!");
        }
        catch (Exception ex)
        {
            Error(ex.Message);
        }
    }
}