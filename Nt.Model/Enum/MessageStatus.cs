using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace Nt.Model.Enum
{
    public enum MessageStatus:int
    {
        /// <summary>
        /// 待阅
        /// </summary>
        Pending = 10,
        /// <summary>
        /// 已经阅读
        /// </summary>
        Read = 20,
        /// <summary>
        /// 已回复
        /// </summary>
        Replied = 30
    }
}
