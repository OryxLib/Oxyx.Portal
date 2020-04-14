using Oryx.CommonInterface.Interface;
using Oryx.UserAccount.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Oryx.Common.Model.Feedback
{
    public class UserFeedback : IEntityModel
    {
        public Guid Id { get; set; }

        public Guid UserAccountId { get; set; }

        [ForeignKey("UserAccountId")]
        public UserAccountEntry UserAccount { get; set; }

        public string Cantact { get; set; }

        public string FeedbackTxt { get; set; }

        public DateTime CreateTime { get; set; }
    }
}
