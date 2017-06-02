using Nt.BLL;
using Nt.BLL.MD5;
using Nt.Framework;
using Nt.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.UI.WebControls;

namespace Nt.Pages.User
{
    public class Edit : NtPageForEdit<Nt_User>
    {
        #region service
        UserLevelService _levelService;
        #endregion

        #region props

        IList<ListItem> _availableUserLevel = null;
        public IList<ListItem> AvailableUserLevel
        {
            get
            {
                return _availableUserLevel;
            }
            set
            {
                if (value != null && value.Count > 0)
                {
                    _availableUserLevel = value;
                }
            }
        }

        #endregion

        #region override

        protected override void BeginInitDataToInsert()
        {
            AvailableUserLevel = _levelService.GetAvailableUserLevel(false);
        }

        protected override void EndInitDataToUpdate()
        {
            AvailableUserLevel = _levelService.GetAvailableUserLevel(false);
            NtUtility.ListItemSelect(AvailableUserLevel, Model.UserLevel_Id);
        }

        protected override void BeginConfigInsert()
        {
            Model.Description = NtUtility.SubStringWithoutHtml(Model.Description, 1024);
            Model.Password = Md5Service.getMd5Hash(Model.Password);//md5加密
        }

        protected override void BeginConfigUpdate()
        {
            Model.Description = NtUtility.SubStringWithoutHtml(Model.Description, 1024);
        }

        protected override bool NtValidateForm()
        {
            if (string.IsNullOrEmpty(Model.UserName))
            {
                Alert("用户名不能为空!", -1);
                return false;
            }

            var oldUserName = Request.Form["oldUserName"];
            var service = _service as UserService;
            if (service.LoginNameExisting(Model.UserName, oldUserName))
            {
                Alert("此用户名已经存在!", -1);
                return false;
            }

            if (!EnsureEdit)
            {
                if (!Model.Password.Equals(Request.Form["Password.Again"]))
                {
                    Alert("两次输入的密码不一致!", -1);
                    return false;
                }
            }
            return true;
        }

        protected override void InitRequiredData()
        {
            _service = new UserService();
            _levelService = new UserLevelService();
            PageTitle = "用户管理";
        }

        #endregion

        public override PermissionRecord CurrentPermissionRecord
        {
            get
            {
                return PermissionRecordProvider.UserManage;
            }
        }
    }
}
