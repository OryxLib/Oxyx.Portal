using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Oryx.Content.Portal.Controllers.paynotfiy
{
    public class payapiNotifyModel
    {
        public string paysapi_id { get; set; }

        public string orderid { get; set; }

        public double price { get; set; }

        public double realprice { get; set; }

        public string orderuid { get; set; }

        public string key { get; set; }
    }
}
