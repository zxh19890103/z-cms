using Nt.BLL;
using Nt.DAL;
using Nt.Framework;
using Nt.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Nt.Pages.Email
{
    public class Detail : NtPage
    {
        private Nt_Email _model = null;
        public Nt_Email Model { get { return _model; } }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            int mailID = 0;

            if (!Int32.TryParse(Request["Id"], out mailID))
            {
                CloseWindow("参数错误");
            }
            _model = CommonFactory.GetById<Nt_Email>(mailID);
            if (_model == null)
                CloseWindow("未查询到指定的记录");
        }

        public override PermissionRecord CurrentPermissionRecord
        {
            get
            {
                return PermissionRecordProvider.EmailManage;
            }
        }

    }
}
