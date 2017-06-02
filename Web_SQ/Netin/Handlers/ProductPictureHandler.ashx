<%@ WebHandler Language="C#" Class="ProductPictureHandler" %>

using System;
using System.Web;
using System.Text;
using Nt.Framework;
using Nt.BLL;
using Nt.BLL.Extension;
using Nt.Model;

public class ProductPictureHandler : BaseHttpHandler
{
    const string INSERT = "INSERT";
    const string DELETE_REC = "DELETE_REC";
    const string DELETE_PICTURE = "DELETE_PICTURE";
    const string UPDATE = "UPDATE";
    const string UPLOAD = "UPLOAD";

    int ThumbnailSize
    {
        get
        {
            int size = 80;
            if (Int32.TryParse(System.Configuration.ConfigurationManager.AppSettings["admin-thumbnailSize"], out size))
                return size;
            else
                return 80;
        }
    }


    int pictureID = 0;
    int PictureID
    {
        get
        {
            if (pictureID == 0)
                pictureID = Convert.ToInt32(Request["Id"]);
            return pictureID;
        }
    }

    string Method
    {
        get { return Request["method"]; }
    }

    PictureService service = null;

    protected override void Handle()
    {
        service = new PictureService();
        switch (Method)
        {
            case DELETE_PICTURE:
                DeletePicture();
                break;
            case DELETE_REC:
                DeleteRecord();
                break;
            case INSERT:
                Insert();
                break;
            case UPDATE:
                Update();
                break;
            case UPLOAD:
                Upload();
                break;
            default:
                Error("没有执行任何操作");
                break;
        }
    }

    void DeleteRecord()
    {
        service.Delete(PictureID);
        Success("删除成功");
    }

    void DeletePicture()
    {
        service.DeletePicture(PictureID);
        Success("图片删除成功");
    }

    /// <summary>
    /// for product pictures
    /// </summary>
    void Insert()
    {
        Nt_Picture m = new Nt_Picture();
        m.PictureUrl = PictureService.NO_IMAGE;
        m.Display = true;
        m.DisplayOrder = 1;
        m.Title = "untitled";
        m.SeoAlt = "no alt";
        m.Text = "http://www.naite.com.cn";
        int id = service.Insert(m);
        m.Id = id;
        ResponseJson["model"] = new NtJson(m).Json;
        ResponseJson["ckHtml"] = HtmlHelper.CheckBox(m.Display,
            "Picture.Display", new { onchange = "syncValforBoolInput(this);", disabled = "disabled" });
        Success("success");
    }

    void Update()
    {
        Nt_Picture m = new Nt_Picture();
        m.InitDataFromPage();
        service.Update(m, new string[] { "PictureUrl" });//don't update PictureUrl
        Success("保存成功!");
    }

    void Upload()
    {
        try
        {
            service.DeletePicture(PictureID);//delete old picture
            string pictureUrl = service.AsyncUpload(); //upload a new picture
            service.SetPictureUrl(PictureID, pictureUrl); //update the pictureUrl value
            Success("上传成功!");
            ResponseJson["PictureUrl"] = pictureUrl;
            ResponseJson["ThumnailUrl"] = service.GetPictureUrl(pictureUrl, ThumbnailSize, true);
        }
        catch (Exception ex)
        {
            Error(ex.Message);
        }
    }
}