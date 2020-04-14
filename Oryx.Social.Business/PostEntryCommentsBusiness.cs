using Oryx.Social.DataAccessor;
using System;

namespace Oryx.Social.Business
{
    public class PostEntryCommentsBusiness
    {
        private readonly PostEntryCommentsDataAccessor postEntryCommentsDataAccessor;

        public PostEntryCommentsBusiness(PostEntryCommentsDataAccessor _postEntryCommentsDataAccessor)
        {
            postEntryCommentsDataAccessor = _postEntryCommentsDataAccessor;
        }
    }
}
