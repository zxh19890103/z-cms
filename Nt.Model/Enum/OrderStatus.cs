﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace Nt.Model.Enum
{
    public enum OrderStatus:int
    {
        /// <summary>
        /// 待处理
        /// </summary>
        Pending = 10,
        /// <summary>
        /// 处理中
        /// </summary>
        Processing = 20,
        /// <summary>
        /// 完成
        /// </summary>
        Complete = 30,
        /// <summary>
        /// 取消
        /// </summary>
        Cancelled = 40
    }
}
