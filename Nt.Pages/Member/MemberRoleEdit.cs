using Nt.BLL;
using Nt.Framework;
using Nt.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Nt.Pages.Member
{
    public class MemberRoleEdit : NtPageForEdit<Nt_MemberRole>
    {
        #region override

        protected override void InitRequiredData()
        {
            ListUrl = "MemberRole.aspx";
            base.InitRequiredData();
        }

        /// <summary>
        /// 表单验证
        /// </summary>
        protected override bool NtValidateForm()
        {
            if (string.IsNullOrEmpty(Model.Name))
            {
                Alert("会员组名不能为空", -1);
                return false;
            }
            return true;
        }

        protected override void BeginConfigInsert()
        {
            Model.Description = NtUtility.SubStringWithoutHtml(Model.Description, 1024);
            base.BeginConfigInsert();
        }

        protected override void BeginConfigUpdate()
        {
            Model.Description = NtUtility.SubStringWithoutHtml(Model.Description, 1024);
            base.BeginConfigUpdate();
        }

        public override PermissionRecord CurrentPermissionRecord
        {
            get
            {
                return PermissionRecordProvider.MemberRoleManage;
            }
        }

        #endregion
    }
}
