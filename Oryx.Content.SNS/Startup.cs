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
using Oryx.CommonDbOperation;
using Oryx.Content.Business;
using Oryx.Content.DataAccessor;
using Social.WebSocket;
using StackExchange.Redis;
using Oryx.WebSocket.Extension.Builder;
using Oryx.RabbitMQService;
using Oryx.DistributedManager;
using Oryx.WebSocket.Extension.DependencyInjection;
using Social.WebSocket.ApplicationBuilderExtension;
using Oryx.UserAccount.Business;
using Oryx.UserAccount.DataAccessor;
using Oryx.Social.Business;
using Oryx.Shop.DataAccessor;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;
using Oryx.Shop.Business;
using Microsoft.AspNetCore.Diagnostics;
using Oryx.Utilities.SentryIO;
using Oryx.Common.Business;
using Oryx.Common.DataAccessor;
using Oryx.Utilities.JPush;
using Oryx.OpenAuth.Sina;
using Oryx.Wx.Core;
using Oryx.Wx.JsSdk;
using Oryx.UserActivity.DataAccessor;
using Oryx.UserActivity.Business;
using Oryx.DynamicPage.Middleware;
using Oryx.DynamicPage;

namespace Oryx.Content.Portal
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
            services.AddMvc()
                .AddSessionStateTempDataProvider()
                .AddJsonOptions(x =>
                {
                    //x.SerializerSettings.ContractResolver = new DefaultContractResolver();
                    x.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
                    x.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
                });
            services.AddCors();
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

            //aspnet distribute session 
            var redis = ConnectionMultiplexer.Connect("101.132.130.133:6379,password=Linengneng123#");
            services.AddDataProtection()
             .SetApplicationName("session_application_name")
             .PersistKeysToRedis(redis, "DataProtection-Keys");

            //add cookie option
            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme).AddCookie(options =>
            {
                options.Cookie.SameSite = Microsoft.AspNetCore.Http.SameSiteMode.None;
                options.Cookie.SecurePolicy = Microsoft.AspNetCore.Http.CookieSecurePolicy.None;
            });
            //services.ConfigureApplicationCookie(options =>
            //{
            //    options.Cookie.SameSite = SameSiteMode.None;
            //});


            //基础通信
            services.AddOryxWebSocket();
            services.AddSingleton<DistributeManager>();
            services.AddSingleton<RabbitMQClient>();
            services.AddSingleton<SocialMsgManager>();
            services.AddSingleton<SocialOryxlWSHandler>();

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

            //活动相关功能
            services.AddActivityDataAccessor();
            services.AddActivityBusiness();

            //微信公号次元吖类注入
            services.AddSingleton(new WxAccessToken(Configuration["Ciyuanya:Wx:WebAppId"], Configuration["Ciyuanya:Wx:WebSecret"]));
            services.AddSingleton<Jsapi_Ticket>();
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
                app.UseDeveloperExceptionPage();
                //app.UseExceptionHandler(builder => builder.Run(async context => await ErrorEvent(context)));

                //app.UseExceptionHandler("/Error");
            }

            app.UseDatabasePageRedner();

            app.UseStaticFiles();
            var sessionOpt = new SessionOptions();
            sessionOpt.Cookie.HttpOnly = false;
            sessionOpt.Cookie.SameSite = SameSiteMode.None;
            app.UseSession(sessionOpt);

            app.UseAuthentication();

            app.Use((ctx, next) =>
            {
                ctx.Response.Headers.Add("Access-Control-Allow-Origin", ctx.Request.Headers["Origin"]);
                ctx.Response.Headers.Add("Access-Control-Allow-Methods", "*");
                ctx.Response.Headers.Add("Access-Control-Allow-Credentials", "true");
                ctx.Response.Headers.Add("Access-Control-Allow-Headers", "AccessToken,Content-Type");
                ctx.Response.Headers.Add("Access-Control-Expose-Headers", "*");
                if (ctx.Request.Method.ToLower() == "options")
                {
                    ctx.Response.StatusCode = 204;

                    return Task.CompletedTask;
                }
                return next();
            });

            app.UserOryxWebSocket(options =>
            {
                options.Register("/social", serviceProvider.GetService<SocialOryxlWSHandler>());
            });

            app.UserSocialRabbitMQ();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller}/{action}/{id?}");
            });
        }

        public Task ErrorEvent(HttpContext context)
        {
            var feature = context.Features.Get<IExceptionHandlerFeature>();
            var error = feature?.Error;
            SentryLog.Log(error);
            return context.Response.WriteAsync("发生错误，已上报，请等待处理。");
        }
    }
}
