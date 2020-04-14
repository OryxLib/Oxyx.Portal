using System;
using System.ComponentModel.DataAnnotations;

namespace Oryx.DataPlatform.Model
{
    public class CityInfo
    {
        [Key]
        public string Code { get; set; }

        public string Name { get; set; }

        public string ParentCode { get; set; }

        public int Level { get; set; }
    }
}
