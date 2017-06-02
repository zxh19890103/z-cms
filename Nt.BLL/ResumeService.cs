using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Nt.Model;
using System.Web.UI.WebControls;
using System.Data;
using Nt.DAL;
using Nt.Model.Enum;

namespace Nt.BLL
{
    public class ResumeService : BaseService<Nt.Model.Nt_Resume>
    {
        public DataTable GetAvailableResume(int jobID)
        {
            return CommonFactory.GetList("Nt_Resume", "Job_Id=" + jobID);
        }

        #region Enum

        /// <summary>
        /// Status
        /// </summary>
        /// <param name="value">10-40(s=10)</param>
        /// <returns></returns>
        public string GetStatusName(object value)
        {
            int status = Convert.ToInt32(value);
            switch (status)
            {
                case 10:
                    return "待审核";
                case 20:
                    return "通过";
                case 30:
                    return "储备";
                case 40:
                    return "放弃";
                default:
                    return "未知";
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="value">10-30(s=10)</param>
        /// <returns></returns>
        public string GetMarritalStatusName(object value)
        {
            int status = Convert.ToInt32(value);
            switch (status)
            {
                case 10:
                    return "未婚";
                case 20:
                    return "已婚";
                case 30:
                    return "离婚";
                default:
                    return "未知";
            }
        }

        /// <summary>
        /// EduDegree Name
        /// </summary>
        /// <param name="value">10-70(s=10)</param>
        /// <returns></returns>
        public string GetEduDegreeName(object value)
        {
            int status = Convert.ToInt32(value);
            switch (status)
            {
                case 10:
                    return "小学";
                case 20:
                    return "初中";
                case 30:
                    return "高中";
                case 40:
                    return "专科";
                case 50:
                    return "学士";
                case 60:
                    return "硕士";
                case 70:
                    return "博士";
                default:
                    return "未知";
            }
        }

        #endregion

        private List<ListItem> _resumeStatusProvider;
        /// <summary>
        /// Status
        /// </summary>
        public List<ListItem> ResumeStatusProvider
        {
            get
            {
                if (_resumeStatusProvider == null)
                {
                    _resumeStatusProvider = new List<ListItem>();
                    _resumeStatusProvider.AddRange(new ListItem[] {
                    new ListItem("待审核","10"),
                    new ListItem("通过","20"),
                    new ListItem("储备","30"),
                    new ListItem("放弃","40"),
                    });
                }
                return _resumeStatusProvider;
            }
        }

       
        private List<ListItem> _resumeMarritalStatusProvider;
        /// <summary>
        /// MarritalStatus
        /// </summary>
        public List<ListItem> ResumeMarritalStatusProvider
        {
            get
            {
                if (_resumeMarritalStatusProvider == null)
                {
                    _resumeMarritalStatusProvider = new List<ListItem>();
                    _resumeMarritalStatusProvider.AddRange(new ListItem[] {
                    new ListItem("未婚","10"),
                    new ListItem("已婚","20"),
                    new ListItem("离婚","30")
                    });
                }
                return _resumeMarritalStatusProvider;
            }
        }

        private List<ListItem> _resumeEduDegreeProvider;
        /// <summary>
        /// EduDegree
        /// </summary>
        public List<ListItem> ResumeEduDegreeProvider
        {
            get
            {
                if (_resumeEduDegreeProvider == null)
                {
                    _resumeEduDegreeProvider = new List<ListItem>();
                    _resumeEduDegreeProvider.AddRange(new ListItem[] {
                    new ListItem("小学","10"),
                    new ListItem("初中","20"),
                    new ListItem("高中","30"),
                    new ListItem("专科","40"),
                    new ListItem("学士","50"),
                    new ListItem("硕士","60"),
                    new ListItem("博士","70")
                    });
                }
                return _resumeEduDegreeProvider;
            }
        }


    }
}
