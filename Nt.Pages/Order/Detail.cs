using Nt.BLL;
using Nt.DAL;
using Nt.Framework;
using Nt.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Nt.Pages.Order
{
    public class Detail : NtPage
    {
        #region service
        OrderService _service = null;
        #endregion

        #region methods

        void InitData()
        {
            if (NtID == IMPOSSIBLE_ID)
                Goto("List.aspx", "参数错误!");
            _service = new OrderService();
            _model = CommonFactory.GetById<Nt_Order>(NtID);
            if (_model == null)
                Goto("List.aspx", "没有发现您所查看的留言!");
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            InitData();
        }

        #endregion

        #region props
        
        public int MemberID { get { return Model.Member_Id; } }

        public OrderService Service { get { return _service; } }

        public override PermissionRecord CurrentPermissionRecord
        {
            get
            {
                return PermissionRecordProvider.OrderManage;
            }
        }

        private Nt_Order _model = null;
        public Nt_Order Model
        {
            get { return _model; }
        }

        private int _id = IMPOSSIBLE_ID;
        public int NtID
        {
            get
            {
                if (_id == IMPOSSIBLE_ID)
                {
                    if (!Int32.TryParse(Request["Id"], out _id))
                        _id = IMPOSSIBLE_ID;
                }
                return _id;
            }
        }

        #endregion
    }
}
