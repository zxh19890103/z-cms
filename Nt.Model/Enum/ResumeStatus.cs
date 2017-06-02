using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Nt.Model.Enum
{
    public enum ResumeStatus:int
    {
        /// <summary>
        /// 待审核
        /// </summary>
        Pending = 10,
        /// <summary>
        /// 已审核通过
        /// </summary>
        Passed = 20,
        /// <summary>
        /// 储备
        /// </summary>
        StoreTalence = 30,
        /// <summary>
        /// 放弃
        /// </summary>
        GiveUp = 40
    }
}
