using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
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

namespace Oryx.SpiderCartoon.Core.V2
{
    class Program
    {
        static string sessionKey = "wSession";
        static string sessionValue = "947B0C10970B5F5B79F97934AD013B7D5EBF4EAB";
        static string sessionDomain = ".weehui.com";
        static string sessionPath = "/";
        static OpenQA.Selenium.Cookie _cookie = new OpenQA.Selenium.Cookie(sessionKey, sessionValue, sessionDomain, sessionPath, DateTime.Now.AddDays(3));
        static DbContextPool<CommonDbContext> dbContextPool;
        static List<Categories> zeroList;
        static void Main(string[] args)
        {
            Timer timer = new Timer(paramState =>
            {
                var wc = new WebClient();
                var proxyList = wc.DownloadString("http://dps.kdlapi.com/api/getdps/?orderid=993831743973837&num=2&pt=1&ut=3&dedup=1&sep=1");
                File.WriteAllText(AppDomain.CurrentDomain.BaseDirectory + "proxyList.txt", proxyList);
            }, null, TimeSpan.FromMilliseconds(20), TimeSpan.FromMinutes(50));
            var uri = new Uri("https://mioto.milbit.com/2353w5/234234/adfasdf/test.jpg");

            var dbCtx = GetDbContext();
            // var contentEntryList = dbCtx.ContentEntry.Where(x => x.MediaResource == null || x.MediaResource.Count <= 5 && x.Category.Tags.Any(c => c.Name == "恋爱")).ToList();
            zeroList = dbCtx.Categories.Where(x => x.ContentList.Any(c => c.MediaResource == null || c.MediaResource.Count == 0) && x.Tags.Any(c => c.Name == "恋爱")).ToList();
            var zeroContentList = dbCtx.ContentEntry.Where(x => (x.MediaResource == null || x.MediaResource.Count == 0) && x.Category.Tags.Any(c => c.Name == "恋爱")).ToList();
            var contentEntryList3 = dbCtx.Categories.Where(x => x.Tags.Any(c => c.Name == "恋爱")).Count();

            Console.WriteLine();
            Console.ReadLine();
            TaskExutor exuctor = TaskExutor.Instance;

            //exuctor.OnTaskResult += Exuctor_OnTaskResult;
            for (int i = 1; i < 30; i++)
            {
                var state = new Dictionary<string, string>();
                state.Add("mainIdex", i.ToString());
                var taskSpider = new TaskSpider("http://www.weehui.com/cartoon/list/" + i, state);
                Task.Run(() => exuctor.SetTask(taskSpider, ProcessListPage)).Wait();
                // Thread.Sleep(1500000);
            }
            Console.WriteLine("Press any key to exit...");
            Console.ReadLine();
        }

        private static void ProcessListPage(IWebElement webElement, Dictionary<string, string> state)
        {
            try
            {
                var body = webElement.FindElement(By.TagName("body")).GetAttribute("innerHTML");
                Console.WriteLine(body);
                var conentList = webElement.FindElements(By.CssSelector(".storyCell"));
                //漫画列表
                foreach (var contentItem in conentList)
                {
                    var categoryTitle = contentItem.FindElement(By.CssSelector(".rightInfo .title")).GetAttribute("innerText");
                    if (!zeroList.Any(x => x.Name == categoryTitle))
                    {
                        continue;
                    }

                    var coverUrl = contentItem.FindElement(By.CssSelector(".leftCover img")).GetAttribute("src");
                    var url = contentItem.FindElement(By.CssSelector(".leftCover a")).GetAttribute("href");
                    var desc = contentItem.FindElement(By.CssSelector(".rightInfo .intr")).GetAttribute("innerText");


                    TaskExutor exuctor = TaskExutor.Instance;
                    //var state = new Dictionary<string, string>();
                    var curState = new Dictionary<string, string>();
                    foreach (var item in state)
                    {
                        curState.Add(item.Key, item.Value);
                    }
                    curState.Add("cateTitle", categoryTitle);
                    var contentTaskSpider = new TaskSpider(url, curState);
                    contentTaskSpider.SetCookie(_cookie);
                    //Console.WriteLine(title);
                    //upload qiniu 
                    var imgStream = DownloadImg(coverUrl);
                    var coverImgPath = "cartoon/" + categoryTitle + "/coverimg.jpg";
                    QiniuTool.UploadImage(imgStream, coverImgPath).Wait();

                    var categoryDbContext = GetDbContext();
                    var hasCategory = categoryDbContext.Categories.AnyAsync(x => x.Name == categoryTitle).Result;
                    if (!hasCategory)
                    {
                        categoryDbContext.Categories.Add(new Categories
                        {
                            Id = Guid.NewGuid(),
                            CreateTime = DateTime.Now,
                            Name = categoryTitle,
                            Description = desc.Trim(),
                            MediaResource = new List<FileEntry> {
                             new FileEntry {
                                   Id = Guid.NewGuid(),
                                  ActualPath = "https://mioto.milbit.com/" +coverImgPath,
                                  CreateTime = DateTime.Now,
                                   Name = coverImgPath
                             }
                        },
                            Tags = new List<Tags> {
                             new  Tags{
                                 Id = Guid.NewGuid(),
                                  Name = "恋爱"
                             },
                             new  Tags{
                                 Id = Guid.NewGuid(),
                                  Name = "都市"
                             },
                             new  Tags{
                                 Id = Guid.NewGuid(),
                                  Name = "Sex"
                             },
                             new  Tags{
                                 Id = Guid.NewGuid(),
                                  Name = "SpiderUplaod2"
                             }
                        }
                        });
                        categoryDbContext.SaveChanges();
                    }

                    processChapter(categoryTitle, contentTaskSpider);
                }
            }
            catch (Exception exc)
            {
                throw;
            }
        }

        private static void processChapter(string categoryTitle, TaskSpider contentTaskSpider)
        {
            TaskExutor exuctor = TaskExutor.Instance;
            var taskSpider = contentTaskSpider;
            Task.Run(() => exuctor.SetTask(taskSpider, (contentWebElement, state) =>
            {
                var chapterList = contentWebElement.FindElement(By.TagName("body")).FindElements(By.CssSelector(".chapterList .item"));
                var contentIndex = 0;
                //章节列表
                foreach (var chapterItem in chapterList)
                {
                    var chapterTitle = chapterItem.GetAttribute("innerText");
                    var chatpterItemUrl = chapterItem.FindElement(By.TagName("a")).GetAttribute("href");
                    var curState = new Dictionary<string, string>();
                    foreach (var item in state)
                    {
                        curState.Add(item.Key, item.Value);
                    }
                    curState.Add("chapterTitle", chapterTitle);
                    //保存content entry
                    //判断是否存在content
                    ContentEntry contentEntry;
                    var chapterDbContext = GetDbContext();
                    var hasCurrentContent = chapterDbContext.ContentEntry.AnyAsync(x => x.Category.Name == categoryTitle && x.Title == chapterTitle).Result;
                    if (!hasCurrentContent)
                    {
                        var currentCategory = chapterDbContext.Categories.FirstOrDefaultAsync(x => x.Name == categoryTitle).Result;
                        contentEntry = new ContentEntry
                        {
                            Id = Guid.NewGuid(),
                            Category = currentCategory,
                            Title = chapterTitle,
                            CreateTime = DateTime.Now,
                            Order = contentIndex
                        };
                        chapterDbContext.ContentEntry.Add(contentEntry);
                        chapterDbContext.SaveChanges();
                    }
                    else
                    {
                        contentEntry = chapterDbContext.ContentEntry.FirstOrDefaultAsync(x => x.Title == chapterTitle && x.Category.Name == categoryTitle).Result;
                    }

                    //Console.WriteLine(chapterTitle);
                    var chapterTaskSpider = new TaskSpider(chatpterItemUrl, curState);
                    chapterTaskSpider.SetCookie(_cookie);
                    var chapterExcutor = TaskExutor.Instance;
                    processContent(categoryTitle, chapterTitle, contentIndex, chatpterItemUrl, contentEntry.Id, chapterTaskSpider);
                    contentIndex++;
                }
            }));
        }

        private static void processContent(string categoryTitle, string chapterTitle, int chapterIndex, string chatpterItemUrl, Guid contentEntryId, TaskSpider chapterTaskSpider)
        {
            TaskExutor chapterExcutor = TaskExutor.Instance;
            var spiderTask = chapterTaskSpider;
            Task.Run(() => chapterExcutor.SetTask(chapterTaskSpider, (detailWebElement, state) =>
             {
                 //图片列表 
                 //if (contentList == null || contentList.Count == 0)
                 //{
                 //    var html = detailWebElement.FindElement(By.TagName("body")).GetAttribute("innerHTML");
                 //    Console.WriteLine(html);
                 //    Console.WriteLine(chatpterItemUrl);

                 //}

                 var chapterItemDbContext = GetDbContext();
                 var currentContentEntry = chapterItemDbContext.ContentEntry.Include("MediaResource").FirstOrDefaultAsync(x => x.Id == contentEntryId).Result;
                 var imgIndex = 0;
                 var imgFileList = new List<FileEntry>();

                 if (currentContentEntry.MediaResource != null && currentContentEntry.MediaResource.Count > 0)
                 {
                     return;
                 }

                 var contentList = detailWebElement.FindElements(By.CssSelector(".contentNovel img"));
                 foreach (var item in contentList)
                 {
                     var imgUrl = "";
                     var src = item.GetAttribute("src");
                     var original = item.GetAttribute("data-original");
                     if (src.Contains("data:"))
                     {
                         imgUrl = original;
                     }
                     else
                     {
                         imgUrl = src;
                     }
                     //不进行下载 ，优先保存到数据库
                     //var imgKey = "cartoon/" + categoryTitle.Trim() + "/" + chapterIndex+ "/img" + imgIndex++ + ".jpg";
                     //var imgResultStream = DownloadImg(imgUrl);
                     //QiniuTool.UploadImage(imgResultStream, imgKey).Wait();
                     //Console.WriteLine(imgUrl);
                     //imgFileList.Add(new FileEntry
                     //{
                     //    Id = Guid.NewGuid(),
                     //    ActualPath = "https://mioto.milbit.com/" + imgKey,
                     //    CreateTime = DateTime.Now,
                     //    Name = imgKey
                     //});
                     if (currentContentEntry.MediaResource.Any(x => x.ActualPath == imgUrl))
                     {
                         continue;
                     }

                     imgFileList.Add(new FileEntry
                     {
                         Id = Guid.NewGuid(),
                         ActualPath = imgUrl,
                         CreateTime = DateTime.Now,
                         Name = "tmpimg",
                         Order = imgIndex,
                         Tag = chapterTitle.Trim() + "/" + chapterIndex
                     });
                     imgIndex++;
                 }
                 chapterItemDbContext.FileEntry.AddRange(imgFileList);
                 currentContentEntry.MediaResource = imgFileList;
                 chapterItemDbContext.SaveChanges();
             }));
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
            optBuilder.UseMySql("server=139.224.219.2;database=ShopApp;user=root;password=Linengneng123#;Character Set=utf8", opts =>
            {
                opts.CommandTimeout(60);
                opts.EnableRetryOnFailure(3);
                opts.MaxBatchSize(1000);
                opts.MigrationsAssembly(" Oryx.SpiderCartoon.Core.V2");
            });
            return new CommonDbContext(option);
        }

        //private static void Exuctor_OnTaskResult(TaskItem taskitem)
        //{
        //    var html = taskitem.WebElement.FindElement(By.TagName("body")).GetAttribute("innerHTML");
        //    var excutor = TaskExutor.Instance;
        //    taskitem.WebElement.FindElements(By.ClassName(""));
        //    //excutor.SetTask()
        //    Console.WriteLine(html);
        //}
    }
}
