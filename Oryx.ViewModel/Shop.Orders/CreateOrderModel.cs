using Oryx.Shop.Model.Shop.Orders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Oryx.ViewModel.Shop.Orders
{
    public class CreateOrderModel
    {
        public List<OrderGoodsDetail> GoodsList { get; set; }
          
        public int TotalFee { get; set; }

        public int OfferFee { get; set; }

        public OfferType OffetType { get; set; }

        public Guid UserCode { get; set; }

        public string OpenId { get; set; }

        public string OrderName { get; set; }

        public string CustomerName { get; set; }

        public string CustomerAddress { get; set; }

        public string CustomerPhone { get; set; }
    }

    public class OrderGoodsDetail
    {
        public Guid GoodsId { get; set; }

        public int Num { get; set; }
    }
}
