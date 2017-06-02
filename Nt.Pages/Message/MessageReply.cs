using Nt.BLL;
using Nt.Framework;
using Nt.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Nt.Pages.Message
{
    public class MessageReply : NtPageForList<Nt_MessageReply>
    {
        #region methods
        protected override void InitRequiredData()
        {
            _service = new MessageReplyService();
        }

        int _messageID;
        public int MessageID { get { return _messageID; } }

        protected override void BeginInitPageData()
        {
            if (!Int32.TryParse(Request.QueryString["Message_Id"], out _messageID))
            {
                CloseWindow("参数错误");
            }
            var service = _service as MessageReplyService;
            DataSource = service.GetMessageReply(_messageID);
        }
        #endregion

        #region props
        public override PermissionRecord CurrentPermissionRecord
        {
            get
            {
                return PermissionRecordProvider.MessageReplyManage;
            }
        }
        #endregion
    }
}
