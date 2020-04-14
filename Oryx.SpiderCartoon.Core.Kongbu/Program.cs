using Microsoft.EntityFrameworkCore;
using OpenQA.Selenium;
using Oryx.CommonDbOperation;
using Oryx.Content.Model;
using Oryx.Spider.StandardV2.TaskBoard;
using Oryx.Utilities;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading;
using System.Threading.Tasks;

namespace Oryx.SpiderCartoon.Core.Kongbu
{
    class Program
    {
        static void Main(string[] args)
        {
            Timer timer = new Timer(paramState =>
            {
                try
                {
                    var wc = new WebClient();
                    var proxyList = wc.DownloadString("http://dps.kdlapi.com/api/getdps/?orderid=953622430174277&num=3&pt=1&ut=3&dedup=1&sep=1");
                    File.WriteAllText(AppDomain.CurrentDomain.BaseDirectory + "proxyList.txt", proxyList);
                }
                catch (Exception exc)
                {
                    Console.WriteLine(exc.Message);
                }
            }, null, TimeSpan.FromMinutes(15), TimeSpan.FromMinutes(15));
            //var categoryDbContext = GetDbContext();
            //var contentEntryList = categoryDbContext.ContentEntry.Where(x => x.MediaResource.Count > 0 && x.Category.Tags.Any(c => c.Name == "恐怖")).ToList();
            TaskExutor exuctor = TaskExutor.Instance;
            //exuctor.OnTaskResult += Exuctor_OnTaskResult;
            for (int i = 40; i < 126; i++)
            {
                var state = new Dictionary<string, string>();
                state.Add("index", i.ToString());
                var taskSpider = new TaskSpider("http://m.yookes.com/weimanhua/kbmh/page/" + i, state);
                //var taskSpider = new TaskSpider("http://m.yookes.com/weimanhua/kbmh", state);
                Task.Run(() => exuctor.SetTask(taskSpider, ProcessListPage));
                // Thread.Sleep(1500000);
            }
            Console.ReadLine();
            Console.WriteLine("Waiting...");
        }

        private static void ProcessListPage(IWebElement obj, Dictionary<string, string> State)
        {
            var exuctor = TaskExutor.Instance;

            Console.WriteLine(obj.FindElement(By.CssSelector("body")).GetAttribute("innerHTML"));
            var categories = obj.FindElements(By.CssSelector(".ajax-load-con.content"));
            var categoryDbContext = GetDbContext();
            foreach (var item in categories)
            {
                var nextUrl = item.FindElement(By.CssSelector("a")).GetAttribute("href");
                Console.WriteLine(item.GetAttribute("innerHTML"));
                var coverUrl = item.FindElement(By.CssSelector("li .image-item img")).GetAttribute("src");
                var categoryTitle = item.FindElement(By.CssSelector(".posts-default-title a")).GetAttribute("title").Trim();
                var creatTime = DateTime.Parse(item.FindElement(By.CssSelector(".ico-time")).GetAttribute("innerText"));

                var imgStream = DownloadImg(coverUrl);
                var coverImgPath = "cartoon/" + categoryTitle + "/coverimg.jpg";
                QiniuTool.UploadImage(imgStream, coverImgPath).Wait();
                var dbCategory = categoryDbContext.Categories.FirstOrDefault(x => x.Name == categoryTitle);
                if (dbCategory == null)
                {
                    var categoryModel = CreateCategory(categoryTitle, creatTime, coverImgPath);
                    categoryDbContext.Add(categoryModel);
                }
                var dbContent = categoryDbContext.ContentEntry.FirstOrDefault(x => x.Title == categoryTitle);

                if (dbContent == null)
                {
                    dbContent = CreateContent(dbCategory, categoryTitle, creatTime);
                    categoryDbContext.Add(dbContent);
                }

                if (dbContent.Category == null)
                {
                    var subCate = categoryDbContext.Categories.FirstOrDefault(x => x.Name == categoryTitle);
                    dbContent.Category = subCate;
                }
                categoryDbContext.SaveChanges();
                if (!string.IsNullOrEmpty(nextUrl))
                {
                    var state = new Dictionary<string, string>();
                    var categoriesTaskSpider = new TaskSpider(nextUrl, state);
                    exuctor.SetTask(categoriesTaskSpider, ProcessImgPage);
                }
            }
        }

        private static void ProcessImgPage(IWebElement obj, Dictionary<string, string> State)
        {
            Console.WriteLine(obj.GetAttribute("innerHTML"));
            var imgList = obj.FindElements(By.CssSelector(".post-content .post-images-item img"));
            var title = obj.FindElement(By.CssSelector(".post-title strong")).GetAttribute("innerText").Trim();
            var dbContext = GetDbContext();
            var contentEntry = dbContext.ContentEntry.Include(x => x.MediaResource).FirstOrDefaultAsync(x => x.Title == title).Result;

            if (contentEntry.MediaResource == null || contentEntry.MediaResource.Count == 0)
            {
                contentEntry.MediaResource = new List<FileEntry> { };
            }
            var lastImgIndex = State.ContainsKey("imgIndex") ? State["imgIndex"] : "";
            var startOrder = string.IsNullOrEmpty(lastImgIndex) ? 0 : int.Parse(lastImgIndex);
            var indexImg = 0;
            foreach (var item in imgList)
            {
                Console.WriteLine("==========================");
                Console.WriteLine(item.GetAttribute("outerHTML"));
                Console.WriteLine("==========================");
                var originUrl = item.GetAttribute("data-original");
                var imgUrl = string.IsNullOrEmpty(originUrl) ? item.GetAttribute("src") : originUrl;
                //var imgKey = "cartoon/" + title + "/" + indexImg + "/img" + indexImg + ".jpg";
                //var imgResultStream = DownloadImg(imgUrl);
                //QiniuTool.UploadImage(imgResultStream, imgKey).Wait();
                if (contentEntry.MediaResource.Any(x => x.ActualPath == imgUrl))
                {
                    continue;
                }
                contentEntry.MediaResource.Add(new FileEntry
                {
                    Id = Guid.NewGuid(),
                    ActualPath = imgUrl,// "https://mioto.milbit.com/" + imgKey,
                    CreateTime = DateTime.Now,
                    Name = imgUrl,
                    Order = indexImg + startOrder
                });
                indexImg++;
            }
            dbContext.SaveChanges();

            var nextPageList = obj.FindElements(By.CssSelector(".page-links a"));
            string nxtPageUrl;
            foreach (var item in nextPageList)
            {
                var currEleTxt = item.GetAttribute("innerText");
                if (currEleTxt.Contains("下一页"))
                {
                    nxtPageUrl = item.GetAttribute("href");
                    if (!string.IsNullOrEmpty(nxtPageUrl))
                    {
                        var nxtState = new Dictionary<string, string>();
                        nxtState.Add("imgIndex", indexImg.ToString());
                        var tSpider = new TaskSpider(nxtPageUrl, nxtState);
                        var exuctor = TaskExutor.Instance;

                        exuctor.SetTask(tSpider, ProcessImgPage);
                    }

                    break;
                }
            }
        }

        private static ContentEntry CreateContent(Categories categoryModel, string categoryTitle, DateTime creatTime)
        {
            return new ContentEntry
            {
                Id = Guid.NewGuid(),
                Category = categoryModel,
                CreateTime = creatTime,
                Title = categoryTitle
            };
        }

        private static Categories CreateCategory(string categoryTitle, DateTime creatTime, string coverImgPath)
        {
            return new Categories
            {
                Id = Guid.NewGuid(),
                CreateTime = creatTime,
                Name = categoryTitle,
                MediaResource = new List<FileEntry> {     new FileEntry {
                                   Id = Guid.NewGuid(),
                                  ActualPath = "https://mioto.milbit.com/" +coverImgPath,
                                  CreateTime = DateTime.Now,
                                   Name = coverImgPath
                             } },
                Tags = new List<Tags> {
                             new  Tags{
                                 Id = Guid.NewGuid(),
                                  Name = "恐怖"
                             },
                             new  Tags{
                                 Id = Guid.NewGuid(),
                                  Name = "幽客社"
                             },
                             new  Tags{
                                 Id = Guid.NewGuid(),
                                  Name = "SpiderUplaod2"
                             } }
            };
        }

        public static Stream DownloadImg(string url)
        {
            var wc = new WebClient();
            var resultBytes = wc.DownloadDataTaskAsync(url).Result;
            return new MemoryStream(resultBytes);
        }

        private static CommonDbContext GetDbContext()
        {

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