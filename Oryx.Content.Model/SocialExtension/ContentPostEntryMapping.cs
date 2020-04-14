using Oryx.CommonInterface.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace Oryx.Content.Model.SocialExtension
{
    public class ContentPostEntryMapping : IEntityModel
    {
        public Guid Id { get; set; }

        public Guid ContentId { get; set; }

        public Guid PostEntryId { get; set; }
        public DateTime CreateTime { get; set; }
    }
}
