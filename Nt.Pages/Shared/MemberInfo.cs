using Nt.BLL;
using Nt.DAL;
using Nt.Framework;
using Nt.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Nt.Pages.Shared
{
    public class MemberInfo : NtPage
    {
        #region service
        MemberService _service = null;
        #endregion

        private Nt_Member _model = null;
        public Nt_Member Model
        {
            get { return _model; }
        }

        void InitData()
        {
            int memberID = 0;
            if (!Int32.TryParse(Request.QueryString["Member_Id"], out memberID))
            {
                CloseWindow("参数错误!");
            }
            _service = new MemberService();
            _model = CommonFactory.GetById<Nt_Member>(memberID);
            if (_model == null)
                CloseWindow("未发现指定的记录!");
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            InitData();
        }
    }
}
