
using OpenQA.Selenium;
using Oryx.Spider.StandardV3;
using Oryx.Spider.StandardV3.Infrastructures;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Oryx.Spider.StandardV3.Ultilities;
using Oryx.Content.Model;
using OpenQA.Selenium.PhantomJS;
using Oryx.Uploader.Business;
using System.Text.RegularExpressions;

namespace Oryx.News.AutoSpider
{
    public class Manhuadaquan
    {
        ContentSpiderCreator uploderCreator;

        public Manhuadaquan()
        {
            uploderCreator = new ContentSpiderCreator();
        }
        public void Start()
        {
            var excutor = TaskExutor.Instance;
            excutor.ClearUpdata("manhuaguiListPage");
            excutor.ClearUpdata("manhuiguiCategoryPage");
            excutor.ClearUpdata("manhuaguiDetailPage");
            excutor.SpiderTaskDelegate.Add("manhuaguiListPage", SetListPageTask);
            excutor.SpiderTaskDelegate.Add("manhuiguiCategoryPage", SetCategoryPageTask);
            excutor.SpiderTaskDelegate.Add("manhuaguiDetailPage", SetDetailPageTask);
           //excutor.Reload("manhuiguiCategoryPage").Wait();
           // excutor.Reload("manhuaguiDetailPage").Wait();
           // return;
            for (int pageIndex = 1; pageIndex < 51; pageIndex++)
            {
                var listPageUrl = $"https://www.manhuagui.com/list/2018/index_p{pageIndex}.html";
                excutor.SetTask(listPageUrl, "manhuaguiListPage").Wait();
            }
        }

        private async Task SetListPageTask(SpiderLog spiderLog, IWebDriver webDriver)
        {
            var aLinkContainer = webDriver.FindElement(By.CssSelector("#contList"));
            var excutor = TaskExutor.Instance;
            var aLinkList = aLinkContainer.FindMultiValueByCss("a.bcover@href");
            foreach (var alink in aLinkList)
            {
                await excutor.SetTask(alink, "manhuiguiCategoryPage");
            }
        }
        private async Task SetCategoryPageTask(SpiderLog spiderLog, IWebDriver webDriver)
        {
            var htmlElement = webDriver.FindElement(By.TagName("html"));
            var title = htmlElement.FindValueByCss(".book-title@text");
            var imgSrc = htmlElement.FindValueByCss(".hcover img@src");
            var tagsTmpArr = htmlElement.FindMultiValueByCss(".detail-list li span@text");
            var tags = FilterTagsTmpArr(tagsTmpArr);
            tags.Add("漫画柜");
            uploderCreator.CreateCategory(title, imgSrc, tags.ToArray());

            var excutor = TaskExutor.Instance;
            var detailPageAElements = htmlElement.FindElements(By.CssSelector(".chapter-list a"));
            var detailOrder = 0;
            var index = detailPageAElements.Count;
            foreach (var aElement in detailPageAElements)
            {
                var href = aElement.GetAttribute("href");
                var contentTitle = aElement.GetAttribute("innerText");
                var _contentTitle = Regex.Replace(contentTitle, @"\d+p", string.Empty);

                uploderCreator.CreateContent(_contentTitle, title, detailOrder);
                await excutor.SetTaskWithParentUrl(href, spiderLog.TargetUrl, title, index, "manhuaguiDetailPage");
                detailOrder++;
                index--;
            }
        }
        private async Task SetDetailPageTask(SpiderLog spiderLog, IWebDriver webDriver)
        {
            var _webDriver = (PhantomJSDriver)webDriver;
            var htmlElement = webDriver.FindElement(By.TagName("html"));
            var imgCount = htmlElement.FindMultiValueByCss("#pageSelect option@text").Count;
            var imgList = new List<string>();
            for (int imgIndex = 0; imgIndex < imgCount; imgIndex++)
            {
                _webDriver.ExecuteScript("SMH.utils.goPage(" + (imgIndex + 1) + ")");
                var imgElement = _webDriver.FindElement(By.CssSelector("#mangaFile"));
                var imgAlt = imgElement.GetAttribute("alt");
                var imgSrc = imgElement.GetAttribute("src");
                var cateName = imgAlt.Split(' ')[0];
                var contentTitle = imgAlt.Split(' ')[1];
                uploderCreator.SetContentIImg(contentTitle, cateName, imgSrc, imgIndex);
                //imgList.Add()
            }
        }
        private List<string> FilterTagsTmpArr(List<string> tagsTmp)
        {
            var tagList = new List<string>();
            foreach (var tag in tagsTmp)
            {
                var targArr = tag.Split('：');
                if (targArr.Length < 2)
                {
                    continue;
                }
                var tagKey = targArr[0].Trim();
                var tagValue = tag.Split('：')[1].Trim();
                switch (tagKey)
                {
                    case "漫画地区":
                        tagList.Add(tagValue.Replace("漫画", ""));
                        break;
                    case "漫画剧情":
                        foreach (var item in tagValue.Split(','))
                        {
                            tagList.Add(item);
                        }
                        break;
                }
            }
            return tagList;
        }
    }
}