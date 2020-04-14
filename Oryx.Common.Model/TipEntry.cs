using Oryx.CommonInterface.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace Oryx.Common.Model
{
    public class TipEntry : IEntityModel
    {
        public Guid Id { get; set; } = Guid.NewGuid();

        public string Title { get; set; }

        public string Content { get; set; }

        public string Link { get; set; }

        public DateTime CreateTime { get; set; } = DateTime.Now;

        public DateTime? PublishTime { get; set; } = null;
    }
}
