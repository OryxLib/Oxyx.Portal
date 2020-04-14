using Oryx.CommonInterface.Interface;
using Oryx.UserAccount.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Oryx.Social.Model
{
    public class UserSocialRelationship : IEntityModel
    {
        public Guid Id { get; set; }

        public Guid RelationOnwerId { get; set; }

        /// <summary>
        /// 关系主(我)
        /// </summary>
        [ForeignKey("RelationOnwerId")]
        public UserAccountEntry RelationOnwerAccount { get; set; }

        public Guid RelationConnectorId { get; set; }

        /// <summary>
        /// 关系连接(关注的人)
        /// </summary>
        [ForeignKey("RelationConnectorId")]
        public UserAccountEntry RelationConnectorAccount { get; set; }

        public DateTime CreateTime { get; set; }
    }
}
