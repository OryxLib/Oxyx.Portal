using System;
using System.Collections.Generic;
using System.Text;

namespace Oryx.Shop.Model.Shop.Coupon
{
    public class CouponEntity
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        /// <summary>
        /// 可使用此券的商品类型
        /// </summary>
        public string GoodsType { get; set; }

        /// <summary>
        /// 优惠券类型/ 折扣/ 或者代金
        /// discount/ fee
        /// </summary>
        public string CouponType { get; set; }

        public string Amount { get; set; }

        public DateTime CreateTime { get; set; }

        public DateTime EndTime { get; set; }
    }
}
