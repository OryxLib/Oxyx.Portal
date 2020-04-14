using Oryx.CommonInterface.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace Oryx.Shop.Model.Shop.Customer
{
    public class CustomerYanzhi : IEntityModel
    {
        public Guid Id { get; set; }

        public virtual CustomerAccount CustomerAccount { get; set; }

        public string PhotoUrl { get; set; }

        public double Score { get; set; }

        public DateTime CreateTime { get; set; } = DateTime.Now;
    }
}
