using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Distributed;
using Oryx.CommonDbOperation;
using Oryx.Social.Model;
using Oryx.UserAccount.Model;
using Oryx.ViewModel.Social.UserRelationship;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Oryx.Social.DataAccessor
{
    public class UserSocialRelationshipDataAccessor : CommonAccessor
    {
        public UserSocialRelationshipDataAccessor(CommonDbContext _ShopDbContext, IDistributedCache _DistributedCache) : base(_ShopDbContext, _DistributedCache)
        {
        }

        public async Task<UserRelationshipInfoViewModel> GetRelationshipByUserId(Guid userId)
        {
            var fansCount = await All<UserSocialRelationship>().CountAsync(x => x.RelationConnectorId == userId);
            var subscripCount = await All<UserSocialRelationship>().CountAsync(x => x.RelationOnwerId == userId);
            var userRelationshipInfo = new UserRelationshipInfoViewModel
            {
                FansCount = fansCount,
                SubcribCount = subscripCount,
                UserAccountId = userId
            };
            return userRelationshipInfo;
        }

        public async Task<List<UserSocialRelationship>> GetRelationShipConnectTo(Guid userId, int skipCount, int pageSize)
        {
            return await All<UserSocialRelationship>().Where(x => x.RelationOnwerId == userId).Skip(skipCount).Take(pageSize).ToListAsync();
        }

        public async Task<List<UserSocialRelationship>> GetRelationShipConnectFrom(Guid userId, int skipCount, int pageSize)
        {
            return await All<UserSocialRelationship>().Where(x => x.RelationConnectorId == userId).Skip(skipCount).Take(pageSize).ToListAsync();
        }

        public async Task<bool> CheckUserRelationship(Guid owerUserId, Guid connectorUserId)
        {
            return await All<UserSocialRelationship>().AnyAsync(x => x.RelationOnwerId == owerUserId && x.RelationConnectorId == connectorUserId);
        }

        public async Task<Dictionary<Guid, bool>> GetRelationshipByUserList(Guid? userId, IList<UserAccountEntry> userQueryList)
        {
            var relationshipMapping = new Dictionary<Guid, bool>();
            if (userId != null)
            {
                var relationshipList = await All<UserSocialRelationship>().Where(x => x.RelationOnwerId == userId && userQueryList.Any(c => c.Id == x.RelationConnectorId)).ToListAsync();
                userQueryList.ToList().ForEach(item =>
                {
                    relationshipMapping.Add(item.Id, relationshipList.Any(c => c.RelationConnectorId == item.Id));
                });
            }
            else
            {
                userQueryList.ToList().ForEach(item =>
                {
                    relationshipMapping.Add(item.Id, false);
                });
            }
            return relationshipMapping;
        }
    }
}
