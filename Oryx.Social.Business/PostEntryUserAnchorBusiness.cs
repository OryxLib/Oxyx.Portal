using Oryx.Social.DataAccessor;
using System;

namespace Oryx.Social.Business
{
    public class PostEntryUserAnchorBusiness
    {
        private readonly PostEntryUserAnchorDataAccessor postEntryUserAnchorDataAccessor;

        public PostEntryUserAnchorBusiness(PostEntryUserAnchorDataAccessor _postEntryUserAnchorDataAccessor)
        {
            postEntryUserAnchorDataAccessor = _postEntryUserAnchorDataAccessor;
        }
    }
}
