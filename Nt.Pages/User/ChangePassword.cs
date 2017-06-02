using Nt.BLL;
using Nt.BLL.MD5;
using Nt.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Nt.Pages.User
{
    public class ChangePassword : NtPage
    {

        #region props

        int _userID = IMPOSSIBLE_ID;
        public new int UserID
        {
            get
            {
                if (_userID == IMPOSSIBLE_ID)
                {
                    if (!Int32.TryParse(Request.QueryString["ID"], out _userID))
                    {
                        Goto("List.aspx", "未指定管理员!");
                    }
                }
                return _userID;
            }
        }

        public override PermissionRecord CurrentPermissionRecord
        {
            get
            {
                return PermissionRecordProvider.UserChangePwd;
            }
        }

        #endregion

        #region methods
        protected void Page_Load(object sender, EventArgs e)
        {
            if (IsHttpPost)
            {
                if (Request.Form["NewPassword"].Trim()
                    .Equals(Request.Form["NewPassword.Again"].Trim())
                    && !string.IsNullOrEmpty(Request.Form["NewPassword"].Trim()))
                {
                    UserService service = new UserService();
                    string md5Pass = Md5Service.getMd5Hash(Request.Form["NewPassword"].Trim());
                    service.ChangePassword(UserID, md5Pass);
                    Goto("List.aspx", "密码修改成功!");
                }
                else
                {
                    ReLoadByScript("密码为空为两次输入密码不一致!");
                }
            }
            PageTitle = "用户更改密码";
        }
        #endregion
    }
}
