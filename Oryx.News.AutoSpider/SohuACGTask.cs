using OpenQA.Selenium;
using Oryx.Spider.StandardV3;
using Oryx.Spider.StandardV3.Infrastructures;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Oryx.Spider.StandardV3.Ultilities;
using System.Text.RegularExpressions;
using Oryx.CommonDbOperation;
using Microsoft.EntityFrameworkCore;
using Oryx.UserAccount.Model;
using Oryx.Content.Model;
using System.Linq;
using Oryx.Content.Model.ContentEntryExtension;

namespace Oryx.News.AutoSpider
{
    public class SohuACGTask
    {
        const string PortUrl = "";
        string sohuNewsItemRegexText = @"\/\/www\.sohu\.com\/a\/\d+_*\d+";
        Regex sohuNewsItemRegex;
        public void SetCategoryTas()
        {
            sohuNewsItemRegex = new Regex(sohuNewsItemRegexText);
            var excutor = TaskExutor.Instance;
            excutor.ClearUpdata("SohuXinfan");
            excutor.ClearUpdata("SohuNewsDetail");
            excutor.SpiderTaskDelegate.Add("SohuXinfan", SetXinfanTask);
            excutor.SpiderTaskDelegate.Add("SohuNewsDetail", SetSohuNewsDetail);

            //excutor.Reload("SohuXinfan").Wait();
            //excutor.Reload("SohuNewsDetail").Wait();
            //return;
            excutor.SetTask("http://www.sohu.com/tag/68798", "SohuXinfan").Wait();


        }

        private async Task SetXinfanTask(SpiderLog spiderLog, IWebDriver webDriver)
        {
            webDriver.ScrollToEnd(10, 2);
            var targetElements = webDriver.FindElements(By.CssSelector(".news-wrapper .news-box"));
            var excutor = TaskExutor.Instance;
            foreach (var item in targetElements)
            {
                var aLink = item.FindValueByCss("h4 a@href");
                var IsNewsItem = sohuNewsItemRegex.IsMatch(aLink);
                //若广告链接跳过
                if (!IsNewsItem)
                {
                    continue;
                }
                await excutor.SetTask(aLink, "SohuNewsDetail");
            }
        }

        private async Task SetSohuNewsDetail(SpiderLog spiderLog, IWebDriver webDriver)
        {
            var dbCtx = GetDbContext();

            var authElement = webDriver.FindElement(By.CssSelector(".column.left"));
            var articleElement = webDriver.FindElement(By.CssSelector(".left.main"));


            var auth_img = authElement.FindValueByCss(".user-pic img@src");
            var auth_name = authElement.FindValueByCss(".user-info h4@text");

            var article_title = articleElement.FindValueByCss(".text-title h1@text");
            var article_createTime = articleElement.FindValueByCss(".text-title .article-info .time@text");
            var article_tags = articleElement.FindMultiValueByCss(".text-title .article-info .tag@text");
            var article_content = articleElement.FindValueByCss(".article@html");
            var article_imgs = articleElement.FindMultiValueByCss(".article img@src");

            var excitedAccount = await dbCtx.Set<UserAccountEntry>().FirstOrDefaultAsync(x => x.NickName == auth_name);
            if (excitedAccount == null)
            {
                excitedAccount = new UserAccountEntry()
                {
                    Id = Guid.NewGuid(),
                    Avatar = auth_img,
                    NickName = auth_name
                };
                await dbCtx.AddAsync(excitedAccount);
                await dbCtx.SaveChangesAsync();
            }

            var excitedArticle = await dbCtx.ContentEntry.FirstOrDefaultAsync(x => x.Title == article_title);
            if (excitedArticle == null)
            {
                var imgList = new List<FileEntry>();
                if (article_imgs != null && article_imgs.Count > 0)
                {
                    foreach (var item in article_imgs)
                    {
                        imgList.Add(new FileEntry
                        {
                            ActualPath = item,
                            CreateTime = DateTime.Now,
                            Name = "tmppath",
                            Id = Guid.NewGuid()
                        });
                    }
                }
                var tagList = new List<Tags>();
                if (article_tags != null && article_tags.Count > 0)
                {
                    foreach (var item in article_tags)
                    {
                        tagList.Add(new Tags
                        {
                            Id = Guid.NewGuid(),
                            Name = item
                        });
                    }
                }
                var articleId = Guid.NewGuid();
                var category = await dbCtx.Categories.FirstOrDefaultAsync(x => x.Name == "资讯");
                var article = new ContentEntry
                {
                    Category = category,
                    Id = articleId,
                    Content = article_content,
                    CreateTime = DateTime.Parse(article_createTime),
                    MediaResource = imgList,
                    Title = article_title,
                    Tags = tagList,
                    ContentEntryInfo = new ContentEntryInfo
                    {
                        Author = auth_name,
                        Source = spiderLog.TargetUrl,
                        Id = articleId,
                        Type = "资讯",
                        UserAccount = excitedAccount,
                        UserAccountId = excitedAccount.Id
                    }
                };
                await dbCtx.AddAsync(article);
                await dbCtx.SaveChangesAsync();
            }
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
