using Oryx.CommonInterface.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace Oryx.Shop.Model.Shop.Goods
{
    public class GoodEntity : IEntityModel
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public double Price { get; set; }

        public string CoverPics { get; set; }

        public string Detail { get; set; }

        public DateTime CreateTime { get; set; } = DateTime.Now;

        public virtual GoodCategory Category { get; set; }

        public virtual GoodOffer GoodOffer { get; set; }

        public string GoodsType { get; set; }

        public bool IsDelete { get; set; } = false;
    }
}
