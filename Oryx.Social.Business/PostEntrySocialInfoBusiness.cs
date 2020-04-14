using Oryx.Social.DataAccessor;
using System;

namespace Oryx.Social.Business
{
    public class PostEntrySocialInfoBusiness
    {
        private readonly PostEntrySocialInfoDataAccessor postEntrySocialInfoDataAccessor;

        public PostEntrySocialInfoBusiness(PostEntrySocialInfoDataAccessor _postEntrySocialInfoDataAccessor)
        {
            postEntrySocialInfoDataAccessor = _postEntrySocialInfoDataAccessor;
        }
    }
}
