using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Nt.Model;
using System.Web.UI.WebControls;
using Nt.Model.Enum;

namespace Nt.BLL
{
    public class OrderService : BaseService<Nt_Order>
    {

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
                    return "待处理";
                case 20:
                    return "审核中";
                case 30:
                    return "已处理";
                case 40:
                    return "取消";
                default:
                    return "未知";
            }
        }

        #endregion

        public override void Update(Nt_Order m)
        {
            base.Update(m, new string[] { "OrderCode" });
        }

        public string GetNewOrderCode()
        {
            return Guid.NewGuid().ToString("N");
        }

        private List<ListItem> _orderStatusProvider;
        /// <summary>
        /// Status 
        /// </summary>
        public List<ListItem> OrderStatusProvider
        {
            get
            {
                if (_orderStatusProvider == null)
                {
                    _orderStatusProvider = new List<ListItem>();
                    _orderStatusProvider.AddRange(new ListItem[] {
                    new ListItem("待处理","10"),
                    new ListItem("审核中","20"),
                    new ListItem("已处理","30"),
                    new ListItem("取消","40")
                    });
                }
                return _orderStatusProvider;
            }
        }
    }
}
