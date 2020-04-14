using Microsoft.EntityFrameworkCore;
using OpenQA.Selenium;
using Oryx.CommonDbOperation;
using Oryx.Spider.StandardV3;
using Oryx.Spider.StandardV3.Infrastructures;
using Oryx.Spider.StandardV3.Ultilities;
using Oryx.Uploader.Business;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Oryx.News.AutoSpider
{
    public class SpiderYaoqiManhua
    {
        public async Task Start()
        {
            var excutor = TaskExutor.Instance;

            excutor.ClearUpdata("YaoqimanhuaMain");
            excutor.ClearUpdata("YaoqimanhuaCategory");

            excutor.SpiderTaskDelegate.Add("YaoqimanhuaMain", YaoqimanhuaMain);
            excutor.SpiderTaskDelegate.Add("YaoqimanhuaCategory", YaoqimanhuaCategory);
            //excutor.SpiderTaskDelegate.Add("YaoqimanhuaDetail", YaoqimanhuaDetail);


            for (int pageIndex = 0; pageIndex < 70; pageIndex++)
            {
                var aLink = $"http://m.yaoqi520.net/shaonvmanhua/list_4_{pageIndex + 1}.html";
                excutor.SetTask(aLink, "YaoqimanhuaMain").Wait();

            }
        }

        private async Task YaoqimanhuaMain(SpiderLog spider, IWebDriver webDriver)
        {
            var excutor = TaskExutor.Instance;
            var targetElements = webDriver.FindElements(By.CssSelector(".pic a"));
            foreach (var item in targetElements)
            {
                var aLink = item.GetAttribute("href");
                await excutor.SetTask(aLink, "YaoqimanhuaCategory");
            }
        }

        private async Task YaoqimanhuaCategory(SpiderLog spiderLog, IWebDriver webDriver)
        {
            var contentCreator = new ContentSpiderCreator();

            var excutor = TaskExutor.Instance;
            var totalCountStr = webDriver.FindElement(By.CssSelector(".showpage a")).GetAttribute("innerText");
            totalCountStr = totalCountStr.Replace("共", "").Replace("页", "").Replace(":", "");

            var name = webDriver.FindElement(By.CssSelector(".ptitle.fc1")).GetAttribute("innerText");
            var coverImg = webDriver.FindElement(By.CssSelector("#imgString img")).GetAttribute("src");
            //contentCreator.CreateCategory(name, coverImg, "日漫", "邪恶漫画", "妖气");

            var totalCount = int.Parse(totalCountStr);
            var targetIdStr = spiderLog.TargetUrl.Split('/').Last().Replace(".html", "");

            var imgResourceTemplate = string.Join('/', coverImg.Split("/").Reverse().Skip(1).Reverse().Append("yaoqi{0}.jpg"));
            contentCreator.CreateNewCategoryAndContentAndLoadImg("全部", name, 0, imgResourceTemplate, totalCount);
            //for (int pageIndex = 1; pageIndex <= totalCount; pageIndex++)
            //{
            //    string aLink;

            //    if (pageIndex == 1)
            //        aLink = $"http://m.yaoqi520.net/shaonvmanhua/{targetIdStr}.html";
            //    else
            //        aLink = $"http://m.yaoqi520.net/shaonvmanhua/{targetIdStr}_{pageIndex}.html";
            //    await excutor.SetTaskWithParentUrl(aLink, spiderLog.ParentUrl, name, pageIndex, "YaoqimanhuaDetail");

            //}
        }

        private async Task YaoqimanhuaDetail(SpiderLog spiderLog, IWebDriver webDriver)
        {
            var img = webDriver.FindElement(By.CssSelector("#imgString img")).GetAttribute("src");
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
