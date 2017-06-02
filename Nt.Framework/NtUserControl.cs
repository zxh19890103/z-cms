using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using Nt.BLL;
using Nt.Model;

namespace Nt.Framework
{
    public class NtUserControl : UserControl
    {

        #region common props
        public string LocalUrl { get { return Request.Url.PathAndQuery; } }
        public NtPage NtPage { get { return Page as NtPage; } }
        public Nt_Language WorkingLang { get { return NtPage.WorkingLang; } }
        public Nt_User WorkingUser { get { return NtPage.WorkingUser; } }
        public bool IsAdministrator { get { return NtPage.IsAdministrator; } }
        //public bool IsLanguageDefault { get { return NtPage.IsLanguageDefault; } }
        public int UserID { get { return NtPage.UserID; } }
        public int LanguageID { get { return NtPage.LanguageID; } }
        #endregion

        #region overidable method

        protected virtual void InitRequiredData()
        { }

        #endregion

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            InitRequiredData();
        }
    }
}
