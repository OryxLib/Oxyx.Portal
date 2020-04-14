using System;
using System.Collections.Generic;
using System.Text;

namespace Oryx.ViewModel.Shop.Goods
{
    public class GoodsQueryModel : BaseQueryViewModel
    {
        public string GoodsName { get; set; }

        public double? StartPrice { get; set; }

        public double? EndPrice { get; set; }
    }
}
