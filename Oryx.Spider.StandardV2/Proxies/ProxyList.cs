using System;
using System.Collections.Generic;
using System.Text;

namespace Oryx.Spider.StandardV2.Proxies
{

    public class ProxyList : List<ProxyItem> { }
    public class ProxyItem
    {
        public string Address { get; set; }

        public string Port { get; set; }
    }
}
