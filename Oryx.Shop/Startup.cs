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
using Oryx.Shop.Business;
using Oryx.Shop.DataAccessor;
using Oryx.Shop.Model;
using StackExchange.Redis;

namespace Oryx.Shop
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
                    x.SerializerSettings.ContractResolver = new DefaultContractResolver();
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
                        opts.MigrationsAssembly("Oryx.Shop");
                    });
                }, 10);
            }
            else
            {
                services.AddDbContextPool<CommonDbContext>(optBuilder =>
                {
                    optBuilder.UseMySql("server=172.19.37.189;database=ShopApp;user=root;password=Linengneng123#;Character Set=utf8;", opts =>
                    {
                        opts.CommandTimeout(60);
                        opts.EnableRetryOnFailure(3);
                        opts.MaxBatchSize(1000);
                        opts.MigrationsAssembly("Oryx.Shop");
                    });
                }, 10);
            }

            var redis = ConnectionMultiplexer.Connect("101.132.130.133:6379,password=Linengneng123#");
            services.AddDataProtection()
             .SetApplicationName("session_application_name")
             .PersistKeysToRedis(redis, "DataProtection-Keys");

            services.AddShopDataAccessor();
            services.AddShopBusiness();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
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
