using Nt.BLL;
using Nt.Framework;
using Nt.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Nt.Pages.Common
{
    public class LanguageEdit : NtPageForEdit<Nt_Language>
    {
        protected override void InitRequiredData()
        {
            ListUrl = "Language.aspx";
        }

        public override PermissionRecord CurrentPermissionRecord
        {
            get
            {
                return PermissionRecordProvider.LanaguageManage;
            }
        }

        protected override void EndInitDataToInsert()
        {
            Model.LanguageCode = "cn";//moren
        }
    }
}
