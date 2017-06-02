using Nt.BLL;
using Nt.DAL.Helper;
using Nt.Framework;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Netin_Common_hezuojigou : Nt.Framework.NtPage
{

    protected override void OnLoad(EventArgs e)
    {
        base.OnLoad(e);

        string method = Request["method"];
        if (!string.IsNullOrEmpty(Request.Headers["X-Requested-With"])
            && Request.Headers["X-Requested-With"].Equals("XMLHttpRequest"))
        {
            Response.Clear();
            method = method.ToLower();
            switch (method)
            {
                case "fetchlist":
                    List();
                    break;
                case "saveone":
                    SaveOne();
                    break;
                case "getone":
                    GetOne();
                    break;
                case "delone":
                    DelOne();
                    break;
                case "delmuti":
                    DelMuti();
                    break;
                case "setdisplay":
                    SetDisplay();
                    break;
                case "reorder":
                    ReOrder();
                    break;
                default:
                    break;
            }
            Response.End();
        }

    }

    void List()
    {
        DataTable data = new DataTable();

        using (var conn = SqlHelper.GetConnection())
        {
            conn.Open();
            SqlCommand cmd = conn.CreateCommand();
            cmd.CommandText = "select * from nt_hezuojigou order by displayorder";
            SqlDataAdapter adp = new SqlDataAdapter(cmd);
            adp.Fill(data);
            conn.Close();
        }

        Response.Write("<table class=\"admin-table\">");
        Response.Write("<tr>");
        Response.Write("<th>选择</th>");
        Response.Write("<th>标题</th>");
        Response.Write("<th>图片</th>");
        Response.Write("<th>链接</th>");
        Response.Write("<th>显示</th>");
        Response.Write("<th>排序</th>");
        Response.Write("<th>操作</th>");
        Response.Write("</tr>");
        foreach (DataRow r in data.Rows)
        {
            Response.Write("<tr>");
            Response.Write("<td><input type=\"checkbox\" class=\"nt-ck-id\" value=\"");
            Response.Write(r["id"]);
            Response.Write("\"/></td>");
            Response.Write("<td>");
            Response.Write(r["title"]);
            Response.Write("</td>");
            Response.Write("<td><img width=\"80\" height=\"80\" src=\"");
            Response.Write(r["img"]);
            Response.Write("\"/></td>");
            Response.Write("<td>");
            Response.Write(r["url"]);
            Response.Write("</td>");
            Response.Write("<td><a href=\"javascript:;\"  type=\"checkbox\" data-item-id=\"");
            Response.Write(r["id"]);
            Response.Write("\" class=\"nt-list-display\">");
            Response.Write(r["display"]);
            Response.Write("</a></td>");
            Response.Write("<td><input  type=\"text\" data-item-id=\"");
            Response.Write(r["id"]);
            Response.Write("\" class=\"input-int32 nt-list-order\" value=\"");
            Response.Write(r["displayorder"]);
            Response.Write("\" /></td>");
            Response.Write("<td><a href=\"javascript:;\" class=\"admin-edit\" onclick=\"edit(");
            Response.Write(r["id"]);
            Response.Write(");\"></a><a href=\"javascript:;\"   class=\"admin-delete\"   onclick=\"delOne(");
            Response.Write(r["id"]);
            Response.Write(");\"></a></td>");
            Response.Write("</tr>");
        }
        Response.Write("<tr>");
        Response.Write("<td><input type=\"checkbox\" class=\"admin-button\" onclick=\"selectAll(this);\" value=\"全选\"/></td>");
        Response.Write("<td colspan=\"4\"></td>");
        Response.Write("<td><input type=\"button\" class=\"admin-button\" onclick=\"reOrder();\" value=\"重新排序\"/></td>");
        Response.Write("<td><input type=\"button\" class=\"admin-button\" onclick=\"delMuti();\" value=\"批量删除\"/></td>");
        Response.Write("</tr>");
        Response.Write("</table>");
    }

    void SaveOne()
    {

        Hashtable hash = new Hashtable();

        try
        {
            string sql = "if @id=0 \r\n begin\r\n insert into nt_hezuojigou (title,url,img,display,displayorder) values(@title,@url,@img,@display,@displayorder);\r\n end \r\n else \r\n begin \r\n update nt_hezuojigou set title=@title,url=@url,img=@img,display=@display,displayorder=@displayorder where id=@id;\r\n end\r\n";

            SqlParameter[] parameters = new SqlParameter[6];
            parameters[0] = new SqlParameter("@title", Request["title"]);
            parameters[1] = new SqlParameter("@url", Request["url"]);
            parameters[2] = new SqlParameter("@img", Request["img"]);
            parameters[3] = new SqlParameter("@display", Request["display"]);
            parameters[4] = new SqlParameter("@displayorder", Request["displayorder"]);
            parameters[5] = new SqlParameter("@id", Request["id"]);

            HttpPostedFile file = Request.Files["File_Img"];
            //save file
            if (file != null)
            {
                string img = string.Empty;
                object pvalue = null;

                if ((pvalue = parameters[2].Value) != null
                    && (img = pvalue.ToString()) != ""
                    && File.Exists((img = MapPath(img))))
                    File.Delete(img);

                string fileUrl = string.Format("/upload/hezuojigou/{0}{1}", DateTime.Now.ToString("yyyyMMddhhmmss_ffff"), Path.GetExtension(file.FileName));
                file.SaveAs(MapPath(fileUrl));
                parameters[2].Value = fileUrl;
            }

            SqlHelper.ExecuteNonQuery(SqlHelper.GetConnection(), CommandType.Text, sql, parameters);

            hash["error"] = 0;
            hash["msg"] = "success!";
        }
        catch (Exception ex)
        {
            hash["error"] = 1;
            hash["msg"] = ex.Message;
        }

        Response.Write(LitJson.JsonMapper.ToJson(hash));
    }

    void GetOne()
    {

        int id = 0;
        int.TryParse(Request["id"], out id);

        Hashtable hash = new Hashtable();

        using (var conn = SqlHelper.GetConnection())
        {
            conn.Open();
            SqlCommand cmd = conn.CreateCommand();
            string[] cols = new string[] { "Title", "Id", "Url", "Img", "Display", "DisplayOrder" };
            cmd.CommandText = string.Format("select {0} from nt_hezuojigou where id={1};", string.Join(",", cols), id);
            SqlDataReader r = cmd.ExecuteReader(System.Data.CommandBehavior.SingleRow);
            hash["error"] = 0;
            hash["msg"] = "yeah!";
            if (r.Read())
            {
                foreach (var col in cols)
                    hash[col] = r[col] == DBNull.Value ? "null" : r[col];
            }
            else
            {
                hash["error"] = 1;
                hash["msg"] = "no record";
            }

            conn.Close();
        }

        Response.Write(LitJson.JsonMapper.ToJson(hash));

    }

    void SetDisplay()
    {
        Hashtable hash = new Hashtable();
        var o = SqlHelper.ExecuteScalar(
            string.Format("update nt_hezuojigou set display=1-display where id={0};select display from nt_hezuojigou where id={0}", Request["id"]));
        hash["error"] = 0;
        hash["msg"] = "success!";
        hash["yes"] = o;
        Response.Write(LitJson.JsonMapper.ToJson(hash));
    }

    void DelOne()
    {
        Hashtable hash = new Hashtable();
        hash["error"] = 0;
        hash["msg"] = "success!";
        SqlHelper.ExecuteNonQuery("delete from nt_hezuojigou where id=" + Request["id"]);
        Response.Write(LitJson.JsonMapper.ToJson(hash));
    }

    void DelMuti()
    {
        Hashtable hash = new Hashtable();
        hash["error"] = 0;
        hash["msg"] = "success!";
        SqlHelper.ExecuteNonQuery("delete from nt_hezuojigou where id in (" + Request["ids"] + ")");
        Response.Write(LitJson.JsonMapper.ToJson(hash));
    }

    void ReOrder()
    {
        string pattern = "update nt_hezuojigou set displayorder={0} where id={1}\r\n";
        string sql = "";
        string[] ids = Request["ids"].Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
        string[] orders = Request["orders"].Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);

        for (int i = 0; i < ids.Length; i++)
        {
            sql += string.Format(pattern, orders[i], ids[i]);
        }

        SqlHelper.ExecuteNonQuery(sql);

        Hashtable hash = new Hashtable();
        hash["error"] = 0;
        hash["msg"] = "success!";
        Response.Write(LitJson.JsonMapper.ToJson(hash));
    }

    public override Nt.BLL.PermissionRecord CurrentPermissionRecord
    {
        get
        {
            return PermissionRecordProvider.ProductManage;
        }
    }
}