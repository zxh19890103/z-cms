using Nt.BLL;
using Nt.Framework;
using Nt.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Nt.Pages.User
{
    public class UserLevelEdit : NtPageForEdit<Nt_UserLevel>
    {

        #region methods

        protected override void InitRequiredData()
        {
            ListUrl = "UserLevel.aspx";
            base.InitRequiredData();
        }

        protected override void BeginConfigInsert()
        {
            Model.Description = NtUtility.SubStringWithoutHtml(Model.Description, 1024);
            base.BeginConfigInsert();
        }

        protected override void BeginConfigUpdate()
        {
            Model.Description = NtUtility.SubStringWithoutHtml(Model.Description, 1204);
            base.BeginConfigUpdate();
        }


        protected override void EndInitDataToUpdate()
        {
            if (Model.Id < WorkingUser.UserLevel_Id)
                GotoListPage("您无权更改您级别以上的用户角色!");
            base.EndInitDataToUpdate();
        }

        #endregion

        #region props

        public override PermissionRecord CurrentPermissionRecord
        {
            get
            {
                return PermissionRecordProvider.UserLevelManage;
            }
        }

        #endregion

    }
}
