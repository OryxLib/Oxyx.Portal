using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json.Serialization;
using Oryx.Common.Business;
using Oryx.Common.DataAccessor;
using Oryx.CommonDbOperation;
using Oryx.Content.Admin.Filters;
using Oryx.Content.Business;
using Oryx.Content.DataAccessor;
using Oryx.DynamicPage;
using Oryx.Shop.Business;
using Oryx.Shop.DataAccessor;
using Oryx.Social.Business;
using Oryx.UserAccount.Business;
using Oryx.UserAccount.DataAccessor;
using Oryx.Utilities.JPush;
using Oryxl.DynamicPage.Accessor;
using Oryxl.DynamicPage.Business;
using StackExchange.Redis;

namespace Oryx.Content.Admin
{
    public class Startup
    {
        public Startup(IConfiguration configuration, IHostingEnvironment environment)
        {
            Configuration = configuration;
            Environment = environment;
        }

        public IConfiguration Configuration { get; }
        public IHostingEnvironment Environment { get; }

        // This method gets called by the runtime. Use this method to add services to the container.

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc(options =>
            {
                //options.Filters.Add<PageAuthentication>();
            })
            .AddSessionStateTempDataProvider()
            .AddJsonOptions(x =>
            {
                x.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
                x.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
            });
            services.AddSession();
            services.AddDistributedMemoryCache();
            services.AddDistributedRedisCache(option =>
            {
                option.Configuration = "101.132.130.133:6379,password=Linengneng123#";
                option.InstanceName = "master";
            });

            if (Environment.IsDevelopment())
            {
                services.AddDbContextPool<CommonDbContext>(optBuilder =>
                {
                    optBuilder.UseMySql("server=139.224.219.2;database=ShopApp;user=root;password=Linengneng123#;Character Set=utf8;", opts =>
                    {
                        opts.CommandTimeout(60);
                        opts.EnableRetryOnFailure(3);
                        opts.MaxBatchSize(1000);
                        opts.MigrationsAssembly("Oryx.Content.Portal");
                    });
                }, 10);
            }
            else
            {
                services.AddDbContextPool<CommonDbContext>(optBuilder =>
                {
                    optBuilder.UseMySql("server=139.224.219.2;database=ShopApp;user=root;password=Linengneng123#;Character Set=utf8;", opts =>
                    {
                        opts.CommandTimeout(60);
                        opts.EnableRetryOnFailure(3);
                        opts.MaxBatchSize(1000);
                        opts.MigrationsAssembly("Oryx.Content.Portal");
                    });
                }, 10);
            }

            var redis = ConnectionMultiplexer.Connect("101.132.130.133:6379,password=Linengneng123#");
            services.AddDataProtection()
             .SetApplicationName("session_application_name")
             .PersistKeysToRedis(redis, "DataProtection-Keys");

            //动态路由+页面
            //services.AddDynamicPageAccessor();
            //services.AddDynamicBusiness();

            //消息推送
            services.AddTransient<JPushHelper>();

            //社会化登录
            //services.AddSingleton<SinaWeiboClient>();

            services.AddContentDataAccessor();
            services.AddContentBusiness();

            services.AddSocialDataAccessor();
            services.AddSocialBusiness();

            services.AddUserAccountDataAccessor();
            services.AddUserAccountBusiness();

            services.AddShopDataAccessor();
            services.AddShopBusiness();

            services.AddSandBoxDataBusiness();
            services.AddSandBoxDataAccessor();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, IServiceProvider serviceProvider)
        {
            if (env.IsDevelopment())
            {
                app.UseBrowserLink();
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
            }

            app.UseStaticFiles();

            app.UseSession();
             
            //app.UseDatabasePageRedner();

            app.Use((ctx, next) =>
            {
                ctx.Response.Headers.Add("Access-Control-Allow-Origin", "*");
                return next();
            });

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    defaults: new { controller = "Home", action = "Index" },
                    template: "{controller}/{action}/{id?}");
            });
        }
    }
}
