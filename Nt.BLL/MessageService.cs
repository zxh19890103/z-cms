using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Nt.Model;
using Nt.BLL.Helper;
using System.Web.UI.WebControls;
using Nt.Model.Enum;

namespace Nt.BLL
{
    public class MessageService : BaseServiceWithCatalog<Nt_Message>
    {
        #region Enum

        /// <summary>
        /// Status
        /// </summary>
        /// <param name="value">10-30(s=10)</param>
        /// <returns></returns>
        public string GetStatusName(object value)
        {
            int status = Convert.ToInt32(value);
            switch (status)
            {
                case 10:
                    return "待阅";
                case 20:
                    return "已阅";
                case 30:
                    return "已回复";
                default:
                    return "未知";
            }
        }

        #endregion

        private List<ListItem> _messageStatusProvider;
        /// <summary>
        /// Status
        /// </summary>
        public List<ListItem> MessageStatusProvider
        {
            get
            {
                if (_messageStatusProvider == null)
                {
                    _messageStatusProvider = new List<ListItem>();
                    _messageStatusProvider.AddRange(new ListItem[] {
                    new ListItem("待阅","10"),
                    new ListItem("已阅","20"),
                    new ListItem("已回复","30")
                    });
                }
                return _messageStatusProvider;
            }
        }


    }
}
