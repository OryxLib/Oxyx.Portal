using Oryx.CommonInterface.Interface;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Oryx.Content.Model
{
    public class Tags : IEntityModel
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public int Order { get; set; }

        public Guid? ContentEntryId { get; set; }

        [ForeignKey("ContentEntryId")]
        public ContentEntry ContentEntry { get; set; }

        public Guid? CategoryId { get; set; }

        [ForeignKey("CategoryId")]
        public Categories Categories { get; set; }

        public DateTime CreateTime { get; set; }
    }
}
