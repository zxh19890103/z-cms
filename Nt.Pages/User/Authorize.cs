using Nt.BLL;
using Nt.DAL;
using Nt.DAL.Helper;
using Nt.Framework;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Web.UI.WebControls;

namespace Nt.Pages.User
{
    public class Authorize : NtPage
    {
        #region service

        PermissionService _permissionservice = new PermissionService();

        UserLevelService _userLevelService = new UserLevelService();

        #endregion

        #region props

        private int _currentUserLevel = IMPOSSIBLE_ID;
        public int CurrentUserLevel
        {
            get
            {
                if (_currentUserLevel == IMPOSSIBLE_ID
                    && !Int32.TryParse(Request.QueryString["UserLevel_Id"], out _currentUserLevel))
                    return IMPOSSIBLE_ID;
                return _currentUserLevel;
            }
        }

        private IList<ListItem> _userLevels = null;
        public IList<ListItem> UserLevels
        {
            get
            {
                if (_userLevels == null)
                    _userLevels = NtUtility.ListItemSelect
                (_userLevelService.GetAvailableUserLevel(true), CurrentUserLevel);
                return _userLevels;
            }
        }

        public override PermissionRecord CurrentPermissionRecord
        {
            get
            {
                return PermissionRecordProvider.UserLevelAuthorization;
            }
        }


        #endregion

        #region methods

        /// <summary>
        /// 为非超级管理员提供权限项
        /// </summary>
        public void RenderPermissionRecords(bool nonAdmin)
        {
            DataSet data = null;
            int[] selected = _permissionservice.GetPermissionIdsByUserLevel(CurrentUserLevel);
            StringBuilder sqlBuilder = new StringBuilder();
            foreach (var c in PermissionRecordProvider.AllPermissionCategory)
            {
                if (nonAdmin)
                    sqlBuilder.AppendFormat("Select * From View_UserPermission Where Category='{0}' And UserLevel_Id={1} \r\n",
                        c, WorkingUser.UserLevel_Id);
                else
                    sqlBuilder.AppendFormat("Select * From Nt_Permission Where Category='{0}'\r\n", c);
            }

            StringBuilder html = new StringBuilder();
            data = SqlHelper.ExecuteDataset(sqlBuilder.ToString());

            foreach (DataTable item in data.Tables)
            {
                if (item.Rows.Count < 1)
                    continue;
                Response.Write("<fieldset>");
                Response.Write(string.Format("<legend>{0}</legend>", item.Rows[0]["CategoryName"]));
                string checkAttribute = string.Empty;
                foreach (DataRow r in item.Rows)
                {
                    checkAttribute = selected.Contains(Convert.ToInt32(r["ID"])) ? "checked=\"checked\"" : string.Empty;
                    Response.Write(string.Format("&nbsp;<input type=\"checkbox\" {2} name=\"PermissionRecord\" value=\"{0}\"/>&nbsp;<label>{1}</label>&nbsp;&nbsp;",
                        r["Id"], r["Name"], checkAttribute));
                }
                Response.Write("</fieldset>");
            }
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            if (CurrentUserLevel == IMPOSSIBLE_ID)
                Goto("UserLevel.aspx", "参数错误!");

            if (_userLevelService.IsAdmin(CurrentUserLevel))
                Goto("UserLevel.aspx", "超级管理员无需授权!");

            if (CurrentUserLevel == WorkingUser.UserLevel_Id)
            {
                Goto("UserLevel.aspx", "您无需对本用户组授权!");
            }

            if (IsHttpPost)
            {
                var level = Request.Form["UserLevel_Id"];
                var permissions = Request.Form["PermissionRecord"];
                var int_level = IMPOSSIBLE_ID;
                if (Int32.TryParse(level, out int_level) && int_level > 0)
                {
                    _permissionservice.SaveAuthorized(int_level, permissions);
                    string phyMenuPath = MapPath(string.Format("/App_Data/Menu/menu-{0}.txt", level));
                    if (File.Exists(phyMenuPath))
                        File.Delete(phyMenuPath);
                    Alert("授权成功");
                }
                else
                {
                    Goto("UserLevel.aspx", "参数错误");
                }
            }
        }

        #endregion
    }
}
