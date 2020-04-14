
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
    public class Dongmanzhijia
    {
        ContentSpiderCreator uploderCreator;

        public Dongmanzhijia()
        {
            uploderCreator = new ContentSpiderCreator();
        }
        public void Start()
        {
            var excutor = TaskExutor.Instance;
            excutor.ClearUpdata("dongmanzhijiaListPage");
            excutor.ClearUpdata("dongmanzhijiaCategoryPage");
            excutor.ClearUpdata("dongmanzhijiaDetailPage");
            excutor.SpiderTaskDelegate.Add("dongmanzhijiaListPage", SetListPageTask);
            excutor.SpiderTaskDelegate.Add("dongmanzhijiaCategoryPage", SetCategoryPageTask);
            excutor.SpiderTaskDelegate.Add("dongmanzhijiaDetailPage", SetDetailPageTask);
            //excutor.Reload("dongmanzhijiaCategoryPage").Wait();
            // excutor.Reload("dongmanzhijiaDetailPage").Wait();
            // return;
            //for (int pageIndex = 1; pageIndex < 51; pageIndex++)
            //{
            //    var listPageUrl = $"https://www.dongmanzhijia.com/list/2018/index_p{pageIndex}.html";
            //    excutor.SetTask(listPageUrl, "dongmanzhijiaListPage").Wait();
            //}
            excutor.SetTask("https://manhua.dmzj.com/tags/marvel.shtml", "dongmanzhijiaListPage").Wait();

        }

        private async Task SetListPageTask(SpiderLog spiderLog, IWebDriver webDriver)
        {
            var aLinkContainer = webDriver.FindElement(By.CssSelector(".column"));
            var excutor = TaskExutor.Instance;
            var aLinkList = aLinkContainer.FindMultiValueByCss(".pic a@href");
            foreach (var alink in aLinkList)
            {
                await excutor.SetTask(alink, "dongmanzhijiaCategoryPage");
            }
        }
        private async Task SetCategoryPageTask(SpiderLog spiderLog, IWebDriver webDriver)
        {
            var htmlElement = webDriver.FindElement(By.TagName("html"));
            var title = htmlElement.FindValueByCss(".anim_title_text a@text");
            var imgSrc = htmlElement.FindValueByCss(".anim_intro_ptext img@src");
            var tagsTmpArr = htmlElement.FindMultiValueByCss(".anim-main_list td@text");
            var tags = FilterTagsTmpArr(tagsTmpArr);
            tags.Add("动漫之家");
            uploderCreator.CreateCategory(title, imgSrc, tags.ToArray());

            var excutor = TaskExutor.Instance;
            var detailPageAElements = htmlElement.FindElements(By.CssSelector(".cartoon_online_border a"));
            var detailOrder = 0;
            var index = detailPageAElements.Count;
            foreach (var aElement in detailPageAElements)
            {
                var href = aElement.GetAttribute("href");
                var contentTitle = aElement.GetAttribute("innerText");
                var _contentTitle = Regex.Replace(contentTitle, @"\d+p", string.Empty);

                uploderCreator.CreateContent(_contentTitle, title, detailOrder);
                await excutor.SetTaskWithParentUrl(href, spiderLog.TargetUrl, title, index, "dongmanzhijiaDetailPage");
                detailOrder++;
                index--;
            }
        }
        private async Task SetDetailPageTask(SpiderLog spiderLog, IWebDriver webDriver)
        {
            var _webDriver = (PhantomJSDriver)webDriver;
            var htmlElement = webDriver.FindElement(By.TagName("html"));
            var imgList = htmlElement.FindMultiValueByCss("#pageSelect option@value");
            for (int imgIndex = 0; imgIndex < imgList.Count; imgIndex++)
            {
                var item = imgList[imgIndex];
                var imgSrc = item;
                var cateName = "";
                var contentTitle = "";
                uploderCreator.SetContentIImg(contentTitle, cateName, imgSrc, imgIndex);
            }
            //var imgCount = htmlElement.FindMultiValueByCss("#pageSelect option@text").Count;
            //var imgList = new List<string>();
            //for (int imgIndex = 0; imgIndex < imgCount; imgIndex++)
            //{
            //    _webDriver.ExecuteScript("SMH.utils.goPage(" + (imgIndex + 1) + ")");
            //    var imgElement = _webDriver.FindElement(By.CssSelector("#mangaFile"));
            //    var imgAlt = imgElement.GetAttribute("alt");
            //    var imgSrc = imgElement.GetAttribute("src");
            //    var cateName = imgAlt.Split(' ')[0];
            //    var contentTitle = imgAlt.Split(' ')[1];
            //    uploderCreator.SetContentIImg(contentTitle, cateName, imgSrc, imgIndex);
            //    //imgList.Add()
            //}
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