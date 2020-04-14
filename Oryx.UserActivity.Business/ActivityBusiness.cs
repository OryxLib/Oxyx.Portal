using Oryx.UserActivity.DataAccessor;
using Oryx.UserActivity.Model;
using Oryx.CommonDbOperation;
using System;
using System.Threading.Tasks;

namespace Oryx.UserActivity.Business
{
    public class ActivityBusiness
    {
        private readonly DataActivityAccessor dataActivityAccessor;
        public ActivityBusiness(DataActivityAccessor _dataActivityAccessor)
        {
            dataActivityAccessor = _dataActivityAccessor;
        }

        public async Task SetActivityLog(string activityName, Guid userId)
        {
            await SetActivityLog(activityName, userId, string.Empty);
        }

        public async Task SetActivityLog(string activityName, Guid userId, string text)
        {
            await dataActivityAccessor.Add(new ActivityLog
            {
                CreateTime = DateTime.Now,
                Id = Guid.NewGuid(),
                Key = activityName,
                Text = text,
                UserAccountId = userId
            });
        }

        public async Task<bool> HasPlay(string activityName, Guid userId)
        {
            return await dataActivityAccessor.Any<ActivityLog>(x => x.UserAccountId == userId && x.Key == activityName);
        }
    }
}
