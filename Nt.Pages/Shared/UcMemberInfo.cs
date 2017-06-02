using Nt.BLL;
using Nt.DAL;
using Nt.Framework;
using Nt.Model;
using Nt.Model.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Nt.Pages.Shared
{
    public class UcMemberInfo : NtUserControl
    {

        private int _memberID = 0;

        public int MemberID
        {
            get { return _memberID; }
            set
            {
                _memberID = value;
                _model = CommonFactory.GetById<View_Member>(value);
            }
        }

        private View_Member _model = null;
        public View_Member Model
        {

            get
            {
                return _model;
            }
        }
    }
}
