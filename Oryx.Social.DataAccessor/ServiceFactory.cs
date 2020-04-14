using Microsoft.Extensions.DependencyInjection;
using Oryx.Social.DataAccessor;
using System;
using System.Collections.Generic;
using System.Text;

namespace Oryx.Shop.DataAccessor
{
    public static class ServiceFactory
    {
        public static IServiceCollection AddSocialDataAccessor(this IServiceCollection services)
        {
            services
                .AddTransient<ChatLogDataAccessor>()
                .AddTransient<ChatMessageDataAccessor>()
                .AddTransient<PostEntryCommentsDataAccessor>()
                .AddTransient<PostEntryDataAccessor>()
                .AddTransient<PostEntryFileDataAccessor>()
                .AddTransient<PostEntrySocialInfoDataAccessor>()
                .AddTransient<PostEntryUserAnchorDataAccessor>()
                .AddTransient<UserSocialRelationshipDataAccessor>();
            return services;
        }
    }
}