using Oryx.Social.DataAccessor;
using System;

namespace Oryx.Social.Business
{
    public class PostEntryFileBusiness
    {
        private readonly PostEntryFileDataAccessor postEntryCommentsDataAccessor;

        public PostEntryFileBusiness(PostEntryFileDataAccessor _postEntryFileDataAccessor)
        {
            postEntryCommentsDataAccessor = _postEntryFileDataAccessor;
        }
    }
}
