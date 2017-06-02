using Nt.BLL;
using Nt.Framework;
using Nt.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Nt.Pages.Order
{
    public class List : NtPageForList<Nt_Order>
    {
        #region methods
        protected override void InitRequiredData()
        {
            _service = new OrderService();
            NeedPagerize = true;
        }
        #endregion

        #region props

        public OrderService Service { get { return _service as OrderService; } }

        public override PermissionRecord CurrentPermissionRecord
        {
            get
            {
                return PermissionRecordProvider.OrderManage;
            }
        }
        #endregion
    }
}
