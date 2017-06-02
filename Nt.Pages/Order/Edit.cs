using Nt.BLL;
using Nt.Framework;
using Nt.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.UI.WebControls;

namespace Nt.Pages.Order
{
    public class Edit : NtPageForEdit<Nt_Order>
    {
        #region   _memberService
        MemberService _memberService = null;
        #endregion

        #region methods

        protected override void InitRequiredData()
        {
            _memberService = new MemberService();
            _memberCollection = _memberService.GetAvailableMembers();
            if (_memberCollection == null || _memberCollection.Count < 1)
                GotoListPage("会员库暂无会员!");
        }

        protected override void BeginConfigInsert()
        {
            Model.OrderCode = Service.GetNewOrderCode();
            Model.Status = 10;
            Model.Note = NtUtility.SubStringWithoutHtml(Model.Note, 1024);
            base.BeginConfigInsert();
        }

        protected override void BeginConfigUpdate()
        {
            Model.Note = NtUtility.SubStringWithoutHtml(Model.Note, 1024);
            base.BeginConfigUpdate();
        }

        protected override void EndInitDataToUpdate()
        {
            NtUtility.ListItemSelect(_memberCollection, Model.Member_Id);
            NtUtility.ListItemSelect(Service.OrderStatusProvider, Model.Status);
            base.EndInitDataToUpdate();
        }

        #endregion

        #region props

        public OrderService Service { get { return _service as OrderService; } }

        public override PermissionRecord CurrentPermissionRecord
        {
            get
            {
                return PermissionRecordProvider.OrderEdit;
            }
        }

        List<ListItem> _memberCollection = null;
        public List<ListItem> MemberCollection { get { return _memberCollection; } }

        #endregion

    }
}
