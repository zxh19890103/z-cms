using Nt.BLL;
using Nt.BLL.Helper;
using Nt.Framework;
using Nt.Model;
using Nt.Model.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Services;

namespace Nt.Pages.Message
{
    public class Edit : NtPageEditWithCatalog<Nt_Message>
    {
        #region props
        public MessageService Service { get { return _service as MessageService; } }
        public override PermissionRecord CurrentPermissionRecord
        {
            get
            {
                return PermissionRecordProvider.MessageManage;
            }
        }
        #endregion

        #region methods
        protected override void InitRequiredData()
        {
            base.InitRequiredData();
            _service = new MessageService();
        }


        public int MemberID { get { return Model.Member_Id; } }


        protected override void EndInitDataToUpdate()
        {
            base.EndInitDataToUpdate();
            if (Model.Status == 0)
                CommonHelper.UpdateStatus("Nt_Message", NtID.ToString(), MessageStatus.Read);
        }
        #endregion

    }
}
