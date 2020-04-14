using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace Oryx.Social.Business
{
    public static class ServiceFactory
    {
        public static IServiceCollection AddSocialBusiness(this IServiceCollection services)
        {
            services
                .AddTransient<ChatBusiness>()
                .AddTransient<PostEntryBusiness>()
                .AddTransient<PostEntryFileBusiness>()
                .AddTransient<PostEntryCommentsBusiness>()
                .AddTransient<PostEntrySocialInfoBusiness>()
                .AddTransient<PostEntryUserAnchorBusiness>()
                .AddTransient<UserSocialRelationshipBusiness>();
            return services;
        }
    }
}
