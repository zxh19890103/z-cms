using Nt.BLL;
using Nt.BLL.Helper;
using Nt.DAL;
using Nt.Framework;
using Nt.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Services;

namespace Nt.Pages.Job
{
    public class ResumeDetail : NtPage
    {
        private Nt_Resume _model = null;
        public Nt_Resume Model { get { return _model; } }

        ResumeService _service;

        int _resumeID;
        public int ResumeID { get { return _resumeID; } }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            if (!Int32.TryParse(Request["Id"], out _resumeID))
            {
                CloseWindow("参数错误");
            }
            _model = CommonFactory.GetById<Nt_Resume>(_resumeID);
            if (_model == null)
                CloseWindow("未发现查询记录！");
        }

        public ResumeService Service
        {
            get
            {
                if (_service == null)
                    _service = new ResumeService();
                return _service;
            }

        }
        
        [WebMethod]
        public static string ChangeStatus(string id, string status)
        {
            int c = CommonHelper.UpdateStatus("Nt_Resume", id, status);
            return new NtJson(new { error = c > 0 ? 0 : 1, message = c > 0 ? "cool!" : "oh,my god!" }).ToString();
        }
    }
}
