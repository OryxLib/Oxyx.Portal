﻿using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Oryx.UserAccount.Model.UserAccountExtend
{
    public class UserAccountEmail
    {
        public Guid Id { get; set; }
        
        public Guid UserAccountId { get; set; }
        
        [ForeignKey("UserAccountId")]
        public UserAccountEntry UserAccount { get; set; }

        public string Email { get; set; }

        public string Password { get; set; }

        public DateTime CreateTime { get; set; }
    }
}