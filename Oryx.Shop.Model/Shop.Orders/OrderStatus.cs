using System;
using System.Collections.Generic;
using System.Text;

namespace Oryx.Shop.Model.Shop.Orders
{
    public enum OrderStatus
    {
        /// <summary>
        /// 创建订单
        /// </summary>
        Created,
        /// <summary>
        /// 订单处理中
        /// </summary>
        Proccessing,
        /// <summary>
        /// 已发货
        /// </summary>
        Expressing,
        /// <summary>
        /// 订单关闭
        /// </summary>
        Closed,
        /// <summary>
        /// 订单成功
        /// </summary>
        Successful,
        /// <summary>
        /// 订单失败
        /// </summary>
        Faild,
        /// <summary>
        /// 等待支付
        /// </summary>
        WaitPay,
        /// <summary>
        /// 支付中
        /// </summary>
        Paying
    }
}
