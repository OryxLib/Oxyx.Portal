using Oryx.Social.DataAccessor;
using Oryx.Social.Model;
using Oryx.ViewModel.Social.UserRelationship;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Oryx.Social.Business
{
    public class UserSocialRelationshipBusiness
    {
        private readonly UserSocialRelationshipDataAccessor userSocialInfoDataAccessor;



        public UserSocialRelationshipBusiness(UserSocialRelationshipDataAccessor _userSocialInfoDataAccessor)
        {
            userSocialInfoDataAccessor = _userSocialInfoDataAccessor;
        }

        /// <summary>
        /// 目标用户的关注和粉丝列表
        /// </summary> 
        public async Task<UserRelationshipInfoViewModel> GetUserRelationShipInfo(Guid userId)
        {
            return await userSocialInfoDataAccessor.GetRelationshipByUserId(userId);
        }

        /// <summary>
        /// 目标关注的(即)
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="skipCount"></param>
        /// <param name="pageSize"></param>
        /// <returns>关注人列表</returns>
        public async Task<List<UserSocialRelationship>> GetRelationShipInfoFromUser(Guid userId, int skipCount = 0, int pageSize = 10)
        {
            return await userSocialInfoDataAccessor.GetRelationShipConnectFrom(userId, skipCount, pageSize);
        }

        /// <summary>
        /// 关注目标的(即粉丝)
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="skipCount"></param>
        /// <param name="pageSize"></param>
        /// <returns>粉丝列表</returns>
        public async Task<List<UserSocialRelationship>> GetRelationShipInfoToUser(Guid userId, int skipCount = 0, int pageSize = 10)
        {
            return await userSocialInfoDataAccessor.GetRelationShipConnectTo(userId, skipCount, pageSize);
        }

        public async Task<bool> CheckSubscib(Guid? owerUserId, Guid connectorUserId)
        {
            if (owerUserId == null)
            {
                return false;
            }
            return await userSocialInfoDataAccessor.CheckUserRelationship(owerUserId.Value, connectorUserId);
        }

        public async Task SetUserRelationship(Guid owerUserId, Guid connectorUserId, bool isSubscibe)
        {
            if (isSubscibe)
            {
                await userSocialInfoDataAccessor.Delete<UserSocialRelationship>(x => x.RelationOnwerId == owerUserId && x.RelationConnectorId == connectorUserId);
            }
            else
            {
                await userSocialInfoDataAccessor.Add(new UserSocialRelationship
                {
                    RelationOnwerId = owerUserId,
                    RelationConnectorId = connectorUserId,
                    Id = Guid.NewGuid(),
                    CreateTime = DateTime.Now
                });
            }
        }
    }
}
