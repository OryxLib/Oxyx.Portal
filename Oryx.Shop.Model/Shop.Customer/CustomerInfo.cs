using System;
using System.Collections.Generic;
using System.Text;

namespace Oryx.Shop.Model.Shop.Customer
{
    /// <summary>
    /// 用户地址信息
    /// </summary>
    public class CustomerInfo
    {
        public Guid Id { get; set; }

        public bool IsDefault { get; set; }

        public CustomerAccount Customer { get; set; }

        public string Name { get; set; }

        public string PhoneNumber { get; set; }

        public string Code { get; set; }

        public string Address { get; set; }

        public string Province { get; set; }

        public string City { get; set; }

        public string District { get; set; }

        public DateTime CreateTime { get; set; } = DateTime.Now;
    }
}
