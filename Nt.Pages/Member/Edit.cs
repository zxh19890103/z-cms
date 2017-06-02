using Nt.BLL;
using Nt.BLL.MD5;
using Nt.Framework;
using Nt.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.UI.WebControls;

namespace Nt.Pages.Member
{
    public class Edit : NtPageForEdit<Nt_Member>
    {
        #region service
        private MemberRoleService _roleService;
        #endregion

        #region Props

        private List<ListItem> _availableMemberRoles = null;

        public List<ListItem> AvailableMemberRoles
        {
            get { return _availableMemberRoles; }
        }

        public override PermissionRecord CurrentPermissionRecord
        {
            get
            {
                return PermissionRecordProvider.MemberEdit;
            }
        }

        #endregion

        #region override

        protected override void InitRequiredData()
        {
            _roleService = new MemberRoleService();
            _service = new MemberService();
        }

        protected override void BeginConfigInsert()
        {
            Model.Note = NtUtility.SubStringWithoutHtml(Model.Note, 1024);
            Model.Password = Md5Service.getMd5Hash(Model.Password);
        }

        protected override void BeginConfigUpdate()
        {
            Model.Note = NtUtility.SubStringWithoutHtml(Model.Note, 1024);
            base.BeginConfigUpdate();
        }

        protected override void BeginInitDataToInsert()
        {

            _availableMemberRoles = _roleService.GetAvailableMemberRoles();
            if (_availableMemberRoles == null || _availableMemberRoles.Count < 1)
                Goto("MemberRoleEdit.aspx", "请先添加会员组！");
        }

        protected override void BeginInitDataToUpdate()
        {
            _availableMemberRoles = _roleService.GetAvailableMemberRoles();
            if (_availableMemberRoles == null || _availableMemberRoles.Count < 1)
                Goto("MemberRoleEdit.aspx", "请先添加会员组！");
            NtUtility.ListItemSelect(
                _availableMemberRoles,
                NtID
                );
        }

        protected override bool NtValidateForm()
        {
            if (string.IsNullOrEmpty(Model.LoginName))
            {
                Alert("登录名不可以为空！", -1);
                return false;
            }

            var service = _service as MemberService;
            if (service.LoginNameExisting(Model.LoginName,
                NtUtility.EnsureNotNull(Request.Form["oldLoginName"])))
            {
                Alert("已经存在此会员名！", -1);
                return false;
            }

            return true;
        }

        #endregion
    }
}
