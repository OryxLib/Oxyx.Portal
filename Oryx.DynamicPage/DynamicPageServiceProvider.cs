using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.Extensions.DependencyInjection;
using Oryx.CommonDbOperation;
using System;
using System.Collections.Generic;
using System.Text;

namespace Oryx.DynamicPage.Middleware
{
    public static class DynamicPageServiceExtension
    {
        public static IServiceCollection AddDynamicPageService(this IServiceCollection serviceCollection, CommonDbContext dbContext)
        {
            RelationalDatabaseCreator databaseCreator =
                      (RelationalDatabaseCreator)dbContext.Database.GetService<IDatabaseCreator>();
            databaseCreator.CreateTables();
             
            return serviceCollection;
        }
    }
}
