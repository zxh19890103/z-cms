using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Nt.Model;
using System.Data;
using Nt.DAL;

namespace Nt.BLL
{
    public class MessageReplyService : BaseService<Nt_MessageReply>
    {
        /// <summary>
        /// 获取留言回复
        /// </summary>
        /// <param name="messageID">留言ID</param>
        /// <returns></returns>
        public DataTable GetMessageReply(int messageID)
        {
            return CommonFactory.GetList(TableName,
                "Message_Id=" + messageID, "DisplayOrder desc,AddDate desc");
        }

        public override int Insert(Nt.Model.Nt_MessageReply m)
        {
            _sql.AppendFormat("Update Nt_Message Set Status=2 Where ID={0} And Status<2 \r\n",m.Message_Id);
            ExecuteSql();
            return base.Insert(m);
        }

    }
}
