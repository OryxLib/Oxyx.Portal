using System;
using System.Collections.Generic;
using System.Text;

namespace Oryx.ViewModel.Shop.Customer
{
    public class AppendAddressViewModel
    {
        public string OpenId { get; set; }

        public string Phone { get; set; }

        public string Name { get; set; }
        
        /// <summary>
        /// 邮编
        /// </summary>
        public string Code { get; set; }

        public string Address { get; set; }

        public string Province { get; set; }

        public string City { get; set; }

        public string Distribute { get; set; }
    }
}
