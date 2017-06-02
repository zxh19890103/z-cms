<%@ WebHandler Language="C#" Class="ProductFieldHandler" %>

using System;
using System.Web;
using Nt.Framework;
using Nt.BLL;

public class ProductFieldHandler : AdminHttpHandler<Nt.Model.Nt_ProductField>
{
    protected override void Insert()
    {
        int pcid = 0;
        if (!Int32.TryParse(Request["ProductCategory_Id"], out pcid))
        {
            Error("参数错误");
            return;
        }

        System.Text.StringBuilder sqlBuilder = new System.Text.StringBuilder();
        Nt.Model.Nt_ProductField m = new Nt.Model.Nt_ProductField();
        m.Display = true;
        m.DisplayOrder = 1;
        m.Name = "unNamed";
        m.ProductCategory_Id = pcid;
        int id = Nt.DAL.CommonFactory.Insert(m);
        m.Id = id;
        responseJson["model"] = new NtJson(m).Json;
        responseJson["ckHtml"] = HtmlHelper.CheckBox(m.Display, "Display", new { });
        Success("添加成功!");
    }

    protected override void Del()
    {
        if (NtID == NtContext.IMPOSSIBLE_ID)
        {
            Error("参数错误!");
            return;
        }

        string sql = string.Format(
            "Delete From Nt_ProductField Where id={0}\r\nDelete From Nt_ProductFieldValue Where ProductField_Id={0}",
            NtID);
        Nt.DAL.Helper.SqlHelper.ExecuteNonQuery(sql);
        Success("删除成功!");
    }
}