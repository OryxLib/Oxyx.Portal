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
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;

namespace Oryx.SpiderCartoon.Core.V3.SpiderInstances
{
    public class SpiderWeehui
    {
        static List<Categories> zeroList;
        public SpiderWeehui()
        {
            //FilterDupContent();
            Timer timer = new Timer(paramState =>
            {
                var wc = new WebClient();
                var proxyList = wc.DownloadString("http://dps.kdlapi.com/api/getdps/?orderid=993831743973837&num=2&pt=1&ut=3&dedup=1&sep=1");
                File.WriteAllText(AppDomain.CurrentDomain.BaseDirectory + "proxyList.txt", proxyList);
            }, null, TimeSpan.FromMilliseconds(0), TimeSpan.FromMinutes(25));
            var dbCtx = GetDbContext();
            TaskExutor exuctor = TaskExutor.Instance;
            //zeroList = dbCtx.Categories.Where(x => x.Tags.Any(c => c.Name == "恋爱")).ToList();
            //var zeroContentList = dbCtx.ContentEntry.Include(x => x.Category).Where(x => (x.MediaResource == null || x.MediaResource.Count == 0) && x.Category.Tags.Any(c => c.Name == "恋爱") && x.Content == null).ToList();

            //var regex = new Regex(@"(?<=<url:)(\S+)(?=>)");
            //foreach (var item in zeroContentList)
            //{
            //    var url = regex.Match(item.Content).Value;
            //    var state = new Dictionary<string, string>();
            //    state.Add("cateTitle", item.Category.Name);
            //    state.Add("chapterTitle", item.Title);
            //    var taskSpider = new TaskSpider(url, state);
            //    Task.Run(() => exuctor.SetTask(taskSpider, ProcessChapterDetailPage)).Wait();
            //}
            for (int i = 1; i < 31; i++)
            {
                var state = new Dictionary<string, string>();
                state.Add("mainIdex", i.ToString());
                var taskSpider = new TaskSpider("http://www.weehui.com/cartoon/list/" + i, state);
                Task.Run(() => exuctor.SetTask(taskSpider, ProcessListPage)).Wait();
                // Thread.Sleep(1500000);
            }

            Console.WriteLine("Start...");
            Console.ReadLine();
        }

        private static void FilterDupContent()
        {
            var dbCtx = GetDbContext();
            var dupContent = dbCtx.ContentEntry.Where(x => x.MediaResource.GroupBy(c => c.ActualPath).Select(v => v.Key).Count() > 1).ToList();

        }

        private static void ProcessListPage(IWebElement webElement, Dictionary<string, string> state)
        {
            var body = webElement.FindElement(By.TagName("body")).GetAttribute("innerHTML");
            Console.WriteLine(body);
            var conentList = webElement.FindElements(By.CssSelector(".storyCell"));
            foreach (var contentItem in conentList)
            {
                var categoryTitle = contentItem.FindElement(By.CssSelector(".rightInfo .title")).GetAttribute("innerText").Trim();
                //if (!zeroList.Any(x => x.Name == categoryTitle))
                //{
                //    continue;
                //}

                var coverUrl = contentItem.FindElement(By.CssSelector(".leftCover img")).GetAttribute("src");
                var url = contentItem.FindElement(By.CssSelector(".leftCover a")).GetAttribute("href");
                var desc = contentItem.FindElement(By.CssSelector(".rightInfo .intr")).GetAttribute("innerText");

                var curState = ExtensionState(state);
                curState.Add("cateTitle", categoryTitle);

                var categoryDbContext = GetDbContext();
                var hasCategory = categoryDbContext.Categories.AnyAsync(x => x.Name == categoryTitle).Result;
                if (!hasCategory)
                {
                    var imgStream = DownloadImg(coverUrl);
                    var coverImgPath = "cartoon/" + categoryTitle + "/coverimg.jpg";
                    QiniuTool.UploadImage(imgStream, coverImgPath).Wait();
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

                var contentTaskSpider = new TaskSpider(url, curState);
                Task.Run(() => TaskExutor.Instance.SetTask(contentTaskSpider, ProcessChapterPage)).Wait();
            }
        }

        private static void ProcessChapterPage(IWebElement webElement, Dictionary<string, string> state)
        {

            var dbCtx = GetDbContext();
            var cateTitle = state["cateTitle"];
            var categoryData = dbCtx.Categories.FirstOrDefault(x => x.Name == cateTitle);

            var chapterList = webElement.FindElement(By.TagName("body")).FindElements(By.CssSelector(".chapterList .item"));
            var contentIndex = 0;

            foreach (var chapterItem in chapterList)
            {
                var chapterTitle = chapterItem.GetAttribute("innerText").Trim();
                var chatpterItemUrl = chapterItem.FindElement(By.TagName("a")).GetAttribute("href");
                var curState = ExtensionState(state);
                curState.Add("chapterTitle", chapterTitle);
                var currentContent = dbCtx.ContentEntry.FirstOrDefaultAsync(x => x.Category.Name == cateTitle && x.Title == chapterTitle).Result;
                if (currentContent == null)
                {
                    var currentCategory = dbCtx.Categories.FirstOrDefaultAsync(x => x.Name == cateTitle).Result;
                    var contentEntry = new ContentEntry
                    {
                        Id = Guid.NewGuid(),
                        Category = currentCategory,
                        Title = chapterTitle,
                        CreateTime = DateTime.Now,
                        Order = contentIndex,
                        Content = "<url:" + chatpterItemUrl + ">"
                    };
                    dbCtx.ContentEntry.Add(contentEntry);
                }
                else
                {
                    currentContent.Content = "<url:" + chatpterItemUrl + ">";
                }
                dbCtx.SaveChanges();
                var chapterDetailSpiderTask = new TaskSpider(chatpterItemUrl, curState);
                if (currentContent.MediaResource == null || currentContent.MediaResource.Count() == 0)
                {
                    Task.Run(() => TaskExutor.Instance.SetTask(chapterDetailSpiderTask, ProcessChapterDetailPage)).Wait();
                }
                contentIndex++;
            }
        }

        private static void ProcessChapterDetailPage(IWebElement webElement, Dictionary<string, string> state)
        {
            var body = webElement.FindElement(By.TagName("body")).GetAttribute("innerHTML");
            Console.WriteLine(body);
            var curState = ExtensionState(state);
            var cateTitle = curState["cateTitle"];
            var chapterTitle = curState["chapterTitle"];
            var dbCtx = GetDbContext();
            var imgIndex = 0;
            var imgFileList = new List<FileEntry>();
            var chapterContent = dbCtx.ContentEntry.FirstOrDefault(x => x.Category.Name == cateTitle && x.Title == chapterTitle);
            var imgActualPathList = chapterContent.MediaResource?.Select(x => x.ActualPath).ToList();
            var contentList = webElement.FindElements(By.CssSelector(".contentNovel img"));
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

                if (imgActualPathList?.Any(x => x == imgUrl) ?? false)
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
                    Tag = chapterTitle.Trim() + "/" + chapterTitle
                });
                imgIndex++;
            }
            chapterContent.MediaResource = new List<FileEntry>();
            dbCtx.FileEntry.AddRange(imgFileList);
            chapterContent.MediaResource = imgFileList;
            dbCtx.SaveChanges();

        }

        public static Dictionary<string, string> ExtensionState(Dictionary<string, string> parentState)
        {
            var _curState = new Dictionary<string, string>();
            foreach (var item in parentState)
            {
                _curState.Add(item.Key, item.Value);
            }
            return _curState;
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
    }
}
