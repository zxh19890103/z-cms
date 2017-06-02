<%@ WebHandler Language="C#" Class="MessageReplyHandler" %>

using System;
using System.Web;
using Nt.Framework;
using Nt.Model;
using Nt.BLL;
using Nt.BLL.Extension;


public class MessageReplyHandler : AdminHttpHandler<Nt_MessageReply>
{
    protected override void Save()
    {
        if (NtID == NtContext.IMPOSSIBLE_ID)
        {
            Error("参数错误!");
            return;
        }
        var m = new Nt_MessageReply();
        m.InitDataFromPage();
        _service.Update(m, new string[]{"AddUser", "AddDate", "Message_Id", "ReplyDate"});
        Success("修改成功!");
    }

    protected override void Insert()
    {
        try
        {
            base.Insert();
            Success("添加成功!");
        }
        catch (Exception ex)
        {
            Error(ex.Message);
        }
    }

}