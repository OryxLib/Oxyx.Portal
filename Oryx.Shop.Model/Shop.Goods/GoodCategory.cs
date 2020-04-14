
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Oryx.Shop.Model.Shop.Goods
{
    public class GoodCategory
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public DateTime CreateTime { get; set; } = DateTime.Now;

        public bool IsDelete { get; set; } = false;
    }
}