using Microsoft.EntityFrameworkCore;
using OpenQA.Selenium;
using Oryx.CommonDbOperation;
using Oryx.Content.Model;
using Oryx.Spider.StandardV3;
using Oryx.Spider.StandardV3.Infrastructures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Orxy.SpiderCartoon.Core.V4
{
    class Program
    {
        static void Main(string[] args)
        {
           // GumaManhua();
           MoejamZixun();
            Console.WriteLine("Start process....");
            //Console.ReadLine();
        }
        private static void MoejamZixun()
        {
            var exutor = TaskExutor.Instance;
            exutor.SpiderTaskDelegate.Add("zixunMain", SetNewsListTask);
            exutor.SpiderTaskDelegate.Add("zixunDetail", SetNewsDetailTask);
            for (int index = 1; index <= 20; index++)
            {
                var mainUrl = "http://www.moejam.com/news_" + index;
                exutor.SetTask(mainUrl, "zixunMain").Wait();
            }
        }

        /// <summary>
        /// 处理资讯列表页
        /// </summary>
        /// <param name="spiderLog"></param>
        /// <param name="webElement"></param>
        /// <returns></returns>
        static async Task SetNewsListTask(SpiderLog spiderLog, IWebDriver driver)
        {
            IWebElement webElement = driver.FindElement(By.TagName("html"));
            var targetAList = webElement.FindElements(By.CssSelector(".content h2 a"));
            var exutor = TaskExutor.Instance;
            foreach (var item in targetAList)
            {
                var url = item.GetAttribute("href");
                await exutor.SetTask(url, "zixunDetail");
            }
        }

        /// <summary>
        /// 处理资讯详情
        /// </summary>
        /// <param name="spiderLog"></param>
        /// <param name="webElement"></param>
        /// <returns></returns>
        static async Task SetNewsDetailTask(SpiderLog spiderLog, IWebDriver driver)
        {
            IWebElement webElement = driver.FindElement(By.TagName("html"));
            var title = webElement.FindElement(By.CssSelector(".content .article-title")).GetAttribute("innerText");
            var time = webElement.FindElement(By.CssSelector(".content .article-info .time")).GetAttribute("innerText");
            var content = webElement.FindElement(By.CssSelector(".content .article-content")).GetAttribute("innerHTML");
            var img = webElement.FindElement(By.CssSelector(".content .article-content")).FindElement(By.TagName("img"))?.GetAttribute("src");
            var dbCtx = GetDbContext();
            var dateTime = DateTime.Parse(time);
            Console.WriteLine("process : :" + title);
            var cateNews = GetTopCategory(dbCtx, "资讯");

            var hasContent = dbCtx.ContentEntry.Any(x => x.Title == title);
            if (!hasContent)
            {
                var contentEntry = new ContentEntry
                {
                    Title = title,
                    CreateTime = dateTime,
                    Id = Guid.NewGuid(),
                    Content = content,
                    Category = cateNews,
                    Tags = new List<Tags>
                {
                    new Tags
                    {
                        Id = Guid.NewGuid(),
                        Name = "资讯"
                    },
                    new Tags
                    {
                        Id = Guid.NewGuid(),
                        Name = "梦域动漫"
                    }
                }
                };
                if (img != null)
                {
                    contentEntry.MediaResource = new List<FileEntry> {
                     new FileEntry {
                          ActualPath  = img,
                          CreateTime= dateTime,
                          Id =Guid.NewGuid(),
                          Name = "tmppath"
                     }
                };
                }
                await dbCtx.ContentEntry.AddAsync(contentEntry);
                await dbCtx.SaveChangesAsync();
            }
        }

        private static Categories GetTopCategory(CommonDbContext dbContext, string name)
        {
            Categories cartoonCate = dbContext.Categories.FirstOrDefault(x => x.Name == name);
            if (cartoonCate == null)
            {
                cartoonCate = new Categories
                {
                    Id = Guid.NewGuid(),
                    Name = name,
                    CreateTime = DateTime.Now
                };
                dbContext.Add(cartoonCate);
                dbContext.SaveChanges();
            }
            return cartoonCate;
        }

        private static void GumaManhua()
        {
            var exutor = TaskExutor.Instance;
            exutor.SpiderTaskDelegate.Add("main", SetCategoryMainTask);
            exutor.SpiderTaskDelegate.Add("category", SetCategoryTask);
            exutor.SpiderTaskDelegate.Add("detail", SetDetailTask);
            //var mainUrl = "http://www.gumua.com/";
            //SetMainTask();
            //exutor.SetTask(mainUrl, "main").Wait();
            exutor.Reload("main").Wait();
            exutor.Reload("category").Wait();
            exutor.Reload("detail").Wait();

        }

        static async Task SetCategoryMainTask(SpiderLog spiderLog, IWebDriver webDriver)
        {
            var manhuaCategoryRegexTxt = @"Manhua/\d+.html";
            var manhuaCategoryRegex = new Regex(manhuaCategoryRegexTxt);

            var manhuaDetailRegextTxt = @"Manhua/\d+_\d+.html";
            var manhuaDetailRegex = new Regex(manhuaDetailRegextTxt);

            var newsCategoryTxt = @"News/\d+\.html";

            if (webDriver == null)
            {
                spiderLog.ReloadSuccess = false;
                await TaskExutor.Instance.UpdateTask(spiderLog);
            }

            var html = webDriver.FindElement(By.TagName("body")).GetAttribute("innerHTML");
            Console.WriteLine(html);
            var aList = webDriver.FindElements(By.TagName("a"));

            foreach (var aElement in aList)
            {
                var url = aElement.GetAttribute("href");
                Console.WriteLine(url);
                if (string.IsNullOrEmpty(url) || !url.ToLower().Contains("/news") && !url.ToLower().Contains("/manhua"))
                {
                    continue;
                }

                //处理漫画主页
                var isManhuaCategoryPage = manhuaCategoryRegex.IsMatch(url);
                if (isManhuaCategoryPage)
                {
                    await TaskExutor.Instance.SetTask(url, "category");
                    if (!TaskExutor.Instance.SpiderTaskDelegate.ContainsKey("category"))
                        TaskExutor.Instance.SpiderTaskDelegate.Add("category", SetCategoryTask);
                }
                //处理漫画详情页
                var isManhuaDetailPage = manhuaDetailRegex.IsMatch(url);
                if (isManhuaDetailPage)
                {
                    await TaskExutor.Instance.SetTaskWithParentUrl(url, spiderLog.TargetUrl, spiderLog.ParentName, spiderLog.Order, "detail");
                    if (!TaskExutor.Instance.SpiderTaskDelegate.ContainsKey("detail"))
                        TaskExutor.Instance.SpiderTaskDelegate.Add("detail", SetDetailTask);
                }

                if (!isManhuaCategoryPage
                    && !isManhuaDetailPage)
                {
                    await TaskExutor.Instance.SetTask(url, "main");
                }
            }
        }

        /// <summary>
        /// 当前页为category 页
        /// </summary>
        /// <param name="spiderLog"></param>
        /// <param name="webElement"></param>
        /// <returns></returns>
        static async Task SetCategoryTask(SpiderLog spiderLog, IWebDriver driver)
        {
            IWebElement webElement = driver.FindElement(By.TagName("html"));
            var detailAList = webElement.FindElements(By.CssSelector(".d_menu a"));
            var categoryName = webElement.FindElement(By.CssSelector(".d_bg_t")).GetAttribute("innerText");
            var categoryCoverUrl = webElement.FindElement(By.CssSelector(".fl.d_bgi_href img")).GetAttribute("src");
            var categoryDescription = webElement.FindElement(By.CssSelector(".d_bg_ce")).GetAttribute("innerText");
            var categoryNote = "note:" + webElement.FindElement(By.CssSelector(".Disclaimer_tit")).GetAttribute("innerText");

            var mainDbContext = GetDbContext();

            //可能存在标签再名字上显示的情况
            var nameArr = categoryName.Split(' ');

            var cateName = nameArr[0];

            for (int index = detailAList.Count - 1; index >= 0; index--)
            {
                var currentItem = detailAList[index];
                await TaskExutor.Instance.SetTaskWithParentUrl(currentItem.GetAttribute("href"), spiderLog.TargetUrl, cateName, detailAList.Count - index, "detail");
            }

            var tagList = new List<Tags>();

            foreach (var item in nameArr)
            {
                tagList.Add(new Tags
                {
                    Id = Guid.NewGuid(),
                    Name = item
                });
            }

            tagList.Add(new Tags
            {
                Id = Guid.NewGuid(),
                Name = "咕嘛"
            });

            tagList.Add(new Tags
            {
                Id = Guid.NewGuid(),
                Name = "恋爱"
            });
            var exitedCategory = await mainDbContext.Categories.AnyAsync(x => x.Name == cateName);


            if (!exitedCategory)
            {
                var topcategory = await mainDbContext.Categories.FirstOrDefaultAsync(x => x.Name == "漫画");
                var category = new Categories
                {
                    Id = Guid.NewGuid(),
                    Name = cateName,
                    Description = categoryDescription,
                    CreateTime = DateTime.Now,
                    Tags = tagList,
                    ParentCategory = topcategory,
                    MediaResource = new List<FileEntry> {
                       new FileEntry {
                            ActualPath =  categoryCoverUrl,
                            CreateTime = DateTime.Now,
                            Name =    "tmpname"
                       }
                    }
                };

                await mainDbContext.Categories.AddAsync(category);
                await mainDbContext.SaveChangesAsync();
            }

            spiderLog.ParentName = categoryName;
            await TaskExutor.Instance.UpdateTask(spiderLog);

            Console.WriteLine("Process: " + categoryName);
        }


        /// <summary>
        /// 当前页为detail
        /// </summary>
        /// <param name="spiderLog"></param>
        /// <param name="webElement"></param>
        /// <returns></returns>
        static async Task SetDetailTask(SpiderLog spiderLog, IWebDriver driver)
        {
            var mainDbContext = GetDbContext();
            IWebElement webElement = driver.FindElement(By.TagName("html"));
            var imgList = webElement.FindElements(By.CssSelector(".r_img img"));
            var detailTitle = webElement.FindElement(By.CssSelector(".fl.r_tab_l span")).GetAttribute("innerText");

            var category = await mainDbContext.Categories.FirstOrDefaultAsync(x => x.Name == spiderLog.ParentName);

            var exitedContent = await mainDbContext.ContentEntry.AnyAsync(x => x.Title == detailTitle && x.Category.Id == category.Id);

            if (!exitedContent)
            {
                var medieaResources = new List<FileEntry>();
                for (int imgIndex = 0; imgIndex < imgList.Count; imgIndex++)
                {
                    var imgItem = imgList[imgIndex];
                    var imgSrc = imgItem.GetAttribute("src");
                    medieaResources.Add(new FileEntry
                    {
                        ActualPath = imgSrc,
                        CreateTime = DateTime.Now,
                        Id = Guid.NewGuid(),
                        Name = "tmpname",
                        Order = imgIndex
                    });
                }

                var content = new ContentEntry
                {
                    Id = Guid.NewGuid(),
                    CreateTime = DateTime.Now,
                    Category = category,
                    MediaResource = medieaResources,
                    Title = detailTitle,
                    Order = spiderLog.Order
                };

                await mainDbContext.AddAsync(content);
                await mainDbContext.SaveChangesAsync();
            }


            Console.WriteLine("Process : " + spiderLog.ParentName + " - " + detailTitle);
        }

        //static async Task SetOtherTask(SpiderLog spiderLog, IWebElement webElement)
        //{
        //    var aList = webdriver.FindElements(By.TagName("a"));

        //    var url = aElement.GetAttribute("href");
        //    //是否站内, 是否脚本
        //    if (url.Contains("http") && !url.Contains("gumua.com")
        //    || url.ToLower().Contains("javacript"))
        //    {
        //        continue;
        //    }

        //}

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
