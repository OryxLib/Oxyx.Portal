using Oryx.CommonInterface.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace Oryx.Social.Model
{
    public class PostEntryTag : IEntityModel
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public DateTime CreateTime { get; set; }
    }
}
