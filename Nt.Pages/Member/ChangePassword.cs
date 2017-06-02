using Nt.BLL;
using Nt.BLL.MD5;
using Nt.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Nt.Pages.Member
{
    public class ChangePassword : NtPage
    {
        private int _memberID = IMPOSSIBLE_ID;
        public int MemberID
        {
            get
            {
                if (_memberID ==IMPOSSIBLE_ID)
                {
                    if (!Int32.TryParse(Request.QueryString["ID"], out _memberID))
                    {
                        Goto("List.aspx", "参数错误!");
                    }
                }
                return _memberID;
            }
        }


        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            if (IsHttpPost)
            {
                if (Request.Form["NewPassword"].Trim()
                    .Equals(Request.Form["NewPassword.Again"].Trim())
                    && !string.IsNullOrEmpty(Request.Form["NewPassword"].Trim()))
                {
                    MemberService service = new MemberService();
                    string md5Pass = Md5Service.getMd5Hash(Request.Form["NewPassword"].Trim());
                    service.ChangePassword(MemberID, md5Pass);
                    Alert("修改成功!");
                }
                else
                {
                    Alert("密码不一致或密码为空!");
                }
            }
        }

        public override PermissionRecord CurrentPermissionRecord
        {
            get
            {
                return PermissionRecordProvider.MemberChangePwd;
            }
        }
    }
}
