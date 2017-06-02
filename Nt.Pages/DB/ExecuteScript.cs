using Nt.BLL;
using Nt.DAL.Helper;
using Nt.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Nt.Pages.DB
{
    public class ExecuteScript : NtPage
    {
        public override PermissionRecord CurrentPermissionRecord
        {
            get
            {
                return PermissionRecordProvider.DBManage;
            }
        }


        protected override void OnLoad(EventArgs e)
        {

            if (IsHttpPost)
            {
                string script = Request.Form["Script"];
                if (string.IsNullOrEmpty(script))
                    Alert("空脚本不允许执行!", -1);
                else
                {
                    try
                    {
                        string[] blocks = script.Split(new string[] { "GO" }, StringSplitOptions.RemoveEmptyEntries);
                        for (int i = 0; i < blocks.Length; i++)
                            SqlHelper.ExecuteNonQuery(blocks[i]);
                        Alert("脚本已执行");
                    }
                    catch (Exception ex)
                    {
                        Alert(ex.Message);
                    }
                }
            }

            base.OnLoad(e);
        }

        void BackUpDB()
        {
            Alert("脚本已执行");
        }

    }




}
