using Nt.BLL;
using Nt.Framework;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace Nt.Pages.Shared
{
    public class UcLanguageSelector : NtUserControl
    {
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            InitPageData();
        }

        DataTable _dataSource = null;
        public DataTable DataSource
        {
            get { return _dataSource; }
        }

        void InitPageData()
        {
            LanguageService service = new LanguageService();
            _dataSource = service.GetList(" Published=1 ", "DisplayOrder desc");
        }
    }
}
