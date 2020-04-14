using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using NLog.Extensions.Logging;
using Oryx.CommonDbOperation;
using Oryx.Shop.Business;
using Oryx.Shop.DataAccessor;
using StackExchange.Redis;
using System.Text;

namespace Oryx.Shop.BusinessApi
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
                        opts.MigrationsAssembly("Oryx.Shop.BusinessApi");
                    });
                }, 128);
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
                        opts.MigrationsAssembly("Oryx.Shop.BusinessApi");
                    });
                }, 128);
            }

            var redis = ConnectionMultiplexer.Connect("101.132.130.133:6379,password=Linengneng123#");
            services.AddDataProtection()
             .SetApplicationName("session_application_name")
             .PersistKeysToRedis(redis, "DataProtection-Keys");

            services.AddShopDataAccessor();
            services.AddShopBusiness();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            if (env.IsDevelopment())
            {
                app.UseBrowserLink();
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler();
            }
            NLog.LogManager.LoadConfiguration("nlog.config");
            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
            loggerFactory.AddNLog();

            app.UseStaticFiles();

            app.UseSession();

            //app.Use((ctx,next)=>{
            //    ctx.Response.Headers.Add("Access-Control-Allow-Origin","*");
            //    return next();
            //});

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller}/{action}/{id?}");
            });
        }
    }
}
