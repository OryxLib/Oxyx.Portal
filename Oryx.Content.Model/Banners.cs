using Oryx.CommonInterface.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace Oryx.Content.Model
{
    public class Banners : IEntityModel
    {
        public Guid Id { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public string Image { get; set; }

        public DateTime CreateTime { get; set; } = DateTime.Now;

        public int Order { get; set; }

        public string Link { get; set; }
    }
}
