<%@ WebHandler Language="C#" Class="ResumePostHandler" %>

using System;
using System.Web;
using Nt.Model;
using Nt.Framework;
using Nt.BLL;
using Nt.BLL.Extension;
using Nt.BLL.Mail;
using Nt.Web;
using Nt.Model.SettingModel;

public class ResumePostHandler : BaseHandler
{

    protected override void Handle()
    {

    }

    /// <summary>
    /// 提交留言
    /// </summary>
    /// <param name="book"></param>
    void PostResume(Nt_Resume resume)
    {
      
    }


    /// <summary>
    /// 验证
    /// </summary>
    /// <param name="book"></param>
    /// <returns></returns>
    bool Validate(Nt_Resume resume)
    {
        bool flag = true;

        return flag;

    }

}