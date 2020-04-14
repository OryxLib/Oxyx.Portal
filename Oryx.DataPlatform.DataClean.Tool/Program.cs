using Microsoft.EntityFrameworkCore;
using Oryx.CommonDbOperation;
using System;
using System.Linq;
using Oryx.Utilities.Http;
using Newtonsoft.Json.Linq;
using Oryx.Utilities;
using Oryx.DataPlatform.Model;
using System.Threading;
using System.Collections.Generic;
using System.Threading.Tasks;
using SimpleInjector;
using SimpleInjector.Lifestyles;
using Microsoft.Extensions.DependencyInjection;

namespace Oryx.DataPlatform.DataClean.Tool
{
    class Program
    {
        static Container container;// = new Container();
        static int index = 1;
        static void Main(string[] args)
        {
            RegisterService();
            MainAsync().Wait();
            //AsyncContext.Run(() => MainAsync());
            Console.ReadLine();
        }

        static async Task MainAsync()
        {
            using (AsyncScopedLifestyle.BeginScope(container))
            {
                var dbContext = container.GetInstance<CommonDbContext>();

                var districts = dbContext.CityInfos.Where(x => x.Level == 2).ToList();

                var okDistrict = await dbContext.StoreMapInfos.GroupBy(x => x.District).Select(x => new
                {
                    district = x.Key,
                    count = x.Count()
                }).Where(x => x.count > 100).ToListAsync();

                try
                {
                    foreach (var item in districts)
                    {
                        if (!okDistrict.Any(x => x.district == item.Name))
                        {
                            await StartGetPosInfo(item);
                        }
                    }
                }
                catch (Exception exc)
                {
                    Console.WriteLine(exc.Message + exc.StackTrace);
                }
            }
        }

        static void RegisterService()
        {
            container = new Container();
            container.Options.DefaultScopedLifestyle = new AsyncScopedLifestyle();

            // Register stuff
            DbContext(container);

            container.Verify();
        }
        // Magic starts here:
        private static void DbContext(Container container)
        {
            // Build an IServiceProvider with DbContext pooling and resolve a scope factory.
            var scopeFactory = new ServiceCollection()
                .AddDbContextPool<CommonDbContext>(o => o.UseMySql("server=101.132.130.133;database=ShopApp;user=root;password=Linengneng123#;Character Set=utf8", opts =>
                {
                    opts.CommandTimeout(60);
                    opts.EnableRetryOnFailure(3);
                    opts.MaxBatchSize(1000);
                    //opts.MigrationsAssembly(" Oryx.SpiderCartoon.Core.V2");
                }), 12)
                .BuildServiceProvider(validateScopes: true)
                .GetRequiredService<IServiceScopeFactory>();

            // Use that scope factory to register an IServiceScope into Simple Injector
            container.Register<IServiceScope>(scopeFactory.CreateScope, Lifestyle.Scoped);

            // Cross wire the DbContext by resolving the IServiceScope and requesting the
            // DbContext from that scope.
            container.Register(() => container.GetInstance<IServiceScope>().ServiceProvider
                .GetService<CommonDbContext>(),
                Lifestyle.Scoped);
        }

        private static async Task StartGetPosInfo(object threadState)
        {
            using (AsyncScopedLifestyle.BeginScope(container))
            {
                var dbContext = container.GetInstance<CommonDbContext>();

                //var dbContext = GetDbContext();
                var item = (CityInfo)threadState;
                Console.WriteLine("启动" + item.Name);

                var posInfoStr = await HttpRequest.Get($"https://restapi.amap.com/v3/place/text?key=32c1923572fc47dac264efc9a171b899&keywords=培训机构&city={item.Code}&citylimit=true&offset=50");

                var posInfoObj = JObject.Parse(posInfoStr);

                var count = posInfoObj["count"].ToString().ToInt();
                Console.WriteLine(item.Name + " : 首次查询完毕: 解析");
                await InsertPosInfoToDB(dbContext, posInfoObj);
                Console.WriteLine(item.Name);
                var pageCount = 50;
                var appendPage = count % pageCount > 0 ? 1 : 0;
                var pageSum = count / pageCount + appendPage;
                Console.WriteLine(item.Name + " : 准备进入第二阶段循环");
                for (int pageIndex = 2; pageIndex <= pageSum; pageIndex++)
                {
                    Console.WriteLine(pageIndex + "/" + pageSum);
                    //var http = new HttpRequest();
                    var posInfoStr2 = await HttpRequest.Get($"https://restapi.amap.com/v3/place/text?key=32c1923572fc47dac264efc9a171b899&keywords=培训机构&city={item.Code}&citylimit=true&page={pageIndex}&offset=50");

                    var posInfoObj2 = JObject.Parse(posInfoStr2);
                    Console.WriteLine(item.Name + " : 循环内部拉取结束");
                    await InsertPosInfoToDB(dbContext, posInfoObj2);
                    Console.WriteLine("Ok");
                }
                Console.WriteLine("Process Task :" + index);
                index++;
            }
        }

        private static async Task InsertPosInfoToDB(CommonDbContext dbContext, JObject posInfoObj)
        {
            var posInfoList = posInfoObj["pois"];

            foreach (var posItem in posInfoList)
            {
                var idcode = posItem["id"].ToString();
                var hasAny = await dbContext.StoreMapInfos.AnyAsync(x => x.IdCode == idcode);
                if (!hasAny)
                {
                    var storeInfoModel = MappingStoreInfoModel(posItem, dbContext);
                    await dbContext.AddAsync(storeInfoModel);
                }
            }
            await dbContext.SaveChangesAsync();
        }

        private static StoreMapInfo MappingStoreInfoModel(JToken posItem, DbContext dbContext)
        {
            var storeMapInfo = new StoreMapInfo
            {
                Id = Guid.NewGuid(),
                Name = posItem["name"].ToString(),
                Type = posItem["type"].ToString(),
                TypeCode = posItem["typecode"].ToString(),
                Address = posItem["address"].ToString(),
                Locaiton = posItem["location"].ToString(),
                Biz_Rating = posItem["biz_ext"]?["rating"]?.ToString(),
                Biz_Cost = posItem["biz_ext"]?["cost"]?.ToString(),
                Province = posItem["pname"].ToString(),
                City = posItem["cityname"].ToString(),
                District = posItem["adname"].ToString(),
                Photos = posItem["photos"].ToString(),
                Tel = posItem["tel"].ToString(),
                IdCode = posItem["id"].ToString()
            };
            return storeMapInfo;
        }

        private static void UpdateCityLefvel(CommonDbContext dbContext)
        {
            var province = dbContext.CityInfos.Where(x => x.ParentCode == null || x.ParentCode == string.Empty).ToList();

            foreach (var pinfo in province)
            {
                var cities = dbContext.CityInfos.Where(x => x.ParentCode == pinfo.Code);

                foreach (var cityInfo in cities)
                {
                    var districts = dbContext.CityInfos.Where(x => x.ParentCode == cityInfo.Code);

                    cityInfo.Level = 1;
                    dbContext.Update(cityInfo);

                    foreach (var dinfo in districts)
                    {
                        dinfo.Level = 2;
                        dbContext.Update(dinfo);
                    }

                    dbContext.SaveChanges();
                }
            }
        }

        private static CommonDbContext GetDbContext()
        {
            Console.WriteLine("拉取DB");
            var option = new DbContextOptions<CommonDbContext>();
            var optBuilder = new DbContextOptionsBuilder(option);
            optBuilder.UseMySql("server=101.132.130.133;database=ShopApp;user=root;password=Linengneng123#;Character Set=utf8", opts =>
            {
                opts.CommandTimeout(60);
                opts.EnableRetryOnFailure(3);
                opts.MaxBatchSize(1000);
                opts.MigrationsAssembly(" Oryx.SpiderCartoon.Core.V2");
            });
            return new CommonDbContext(option);
        }
    }
}
