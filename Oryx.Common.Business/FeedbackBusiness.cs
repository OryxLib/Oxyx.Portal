using Oryx.Common.DataAccessor;
using Oryx.Common.Model.Feedback;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Oryx.Common.Business
{
    public class FeedbackBusiness
    {
        private FeedbackAccessor feedbackAccessor;
        public FeedbackBusiness(FeedbackAccessor _feedbackAccessor)
        {
            feedbackAccessor = _feedbackAccessor;
        }

        public async Task AddFeedback(UserFeedback feedbackModel)
        {
            await feedbackAccessor.Add(feedbackModel);
        }

        public async Task<IList<UserFeedback>> ListFeedback(int skipCount, int pageSize)
        {
            return await feedbackAccessor.ListOrderByDescendingAsync<UserFeedback>(skipCount, pageSize);
        }
    }
}
