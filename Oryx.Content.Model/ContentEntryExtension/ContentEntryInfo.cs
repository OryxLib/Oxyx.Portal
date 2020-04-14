using Oryx.UserAccount.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Oryx.Content.Model.ContentEntryExtension
{
    public class ContentEntryInfo
    {
        public Guid Id { get; set; }

        [ForeignKey("Id")]
        public ContentEntry ContentEntry { get; set; }

        public Guid UserAccountId { get; set; }

        [ForeignKey("UserAccountId")]
        public UserAccountEntry UserAccount { get; set; }

        public string Author { get; set; }

        public string Source { get; set; } 

        public string Type { get; set; }
    }
}
