using Oryx.CommonInterface.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace Oryx.Shop.Model.Shop.Goods
{
    public class GoodOffer : IEntityModel
    {
        public Guid Id { get; set; }

        public double StartScore { get; set; }

        public double EndScore { get; set; }

        public double OfferValue { get; set; }

        public DateTime CreateTime { get; set; } = DateTime.Now;
    }
}
