using System;
using System.Collections.Generic;
using System.Text;

namespace Oryx.DataPlatform.Model
{
    public class StoreMapInfo
    {
        public Guid Id { get; set; }

        public string IdCode { get; set; }

        /// <summary>
        /// 店铺名称
        /// </summary>
        public string Name { get; set; }

        public string Type { get; set; }

        public string TypeCode { get; set; }

        public string Address { get; set; }

        public string Locaiton { get; set; }

        public string Tel { get; set; }

        public string Biz_Rating { get; set; }

        public string Biz_Cost { get; set; }

        /// <summary>
        /// 省
        /// </summary>
        public string Province { get; set; }

        /// <summary>
        /// 市
        /// </summary>
        public string City { get; set; }

        /// <summary>
        /// 区县
        /// </summary>
        public string District { get; set; }

        /// <summary>
        /// Map info photos 
        /// </summary>
        public string Photos { get; set; }
    }
}
