using Nt.BLL;
using Nt.Framework;
using Nt.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Nt.Pages.Seo
{
    public class SearchKeyword : NtPageForList<Nt_SearchKeyWord>
    {
        public override PermissionRecord CurrentPermissionRecord
        {
            get
            {
                return PermissionRecordProvider.SearchWordsManage;
            }
        }
    }
}
