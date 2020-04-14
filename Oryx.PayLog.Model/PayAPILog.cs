using Oryx.UserAccount.Model;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Oryx.PayLog.Model
{
    public class PayAPILog
    {
        public Guid Id { get; set; }

        public Guid UserAccountId { get; set; }

        [ForeignKey("UserAccountId ")]
        public UserAccountEntry UserAccount { get; set; }

        public string PayAPIOrderId { get; set; }

        public int PayNum { get; set; }

        public PayAPILogStatus Statue { get; set; }

        public DateTime? EndTime { get; set; }

        public DateTime CreateTime { get; set; }
    }

    public enum PayAPILogStatus
    {
        Create,
        Process,
        End,
        Error
    }
}
