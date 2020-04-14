using Microsoft.EntityFrameworkCore;
using OpenQA.Selenium;
using Oryx.CommonDbOperation;
using Oryx.Content.Model;
using Oryx.Social.Model;
using Oryx.Spider.StandardV2.TaskBoard;
using Oryx.Spider.StandardV2.WebDriverExtensions;
using Oryx.UserAccount.Model;
using Oryx.Utilities;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;

namespace Oryx.SpiderSocial.Core
{
    class Program
    {
        static void Main(string[] args)
        {

            //Timer timer = new Timer(paramState =>
            //{
            //    var wc = new WebClient();
            //    var proxyList = wc.DownloadString("http://dps.kdlapi.com/api/getdps/?orderid=993831743973837&num=2&pt=1&ut=3&dedup=1&sep=1");
            //    File.WriteAllText(AppDomain.CurrentDomain.BaseDirectory + "proxyList.txt", proxyList);
            //}, null, TimeSpan.FromMilliseconds(20), TimeSpan.FromMinutes(50));
            //UpdateDAT();
            //return;
            var excutor = TaskExutor.Instance;
            var mainState = new Dictionary<string, string>();
            var mainSpiderTask = new TaskSpider("https://bcy.net/coser/toppost100", mainState);
            Task.Run(() => excutor.SetTask(mainSpiderTask, AnalysisMain)).Wait();
            Console.WriteLine("Hello World!");
            Console.ReadLine();
        }

        public static void UpdateDAT()
        {
            var dbContext = GetDbContext();
            var zeroPostEntry = dbContext.PostEntry;
            foreach (var item in zeroPostEntry)
            {
                item.TimeStamp = TimeStamp.Get(item.CreateTime);
            }
            dbContext.SaveChanges();
        }

        public static async Task AnalysisMain(IWebElement element, Dictionary<string, string> state, WebDriverWrapper webDriverWrapper)
        {
            var regex = new Regex(@"(?<=avatar\/)(\d+)");
            var exutor = TaskExutor.Instance;
            for (int i = 0; i < 4; i++)
            {
                webDriverWrapper.WebDriver.ExecuteScript("window.scrollTo(0,document.body.offsetHeight)");
                Thread.Sleep(2 * 1000);
            }
            var _curState = ExtensionState(state);
            //解析 post 列表
            var _elementPostList = webDriverWrapper.WebDriver.FindElements(By.CssSelector(".rank-index-box .rank-cos-item"));
            for (int i = 2; i < _elementPostList.Count;)
            {
                var item = _elementPostList[i++];

                var itemHtml2 = _elementPostList[3].GetAttribute("innerHTML");
                var itemHtml = item.GetAttribute("innerHTML");
                var imgSrc = item.FindElement(By.CssSelector(".rank-cos-bottom img")).GetAttribute("src");

                var userId = regex.Match(imgSrc);
                var profileUrl = "https://bcy.net/u/" + userId;
                _curState.Add("profileUrl", profileUrl);
                var profileSpiderTask = new TaskSpider(profileUrl, _curState);
                await Task.Run(() => exutor.SetTask(profileSpiderTask, AnalysisProfile));
                //var postUrl = item.FindElement(By.CssSelector("a")).GetAttribute("href");
                //var postSpiderTask = new TaskSpider(postUrl, _curState);
                //exutor.SetTask(postSpiderTask, AnalysisPost);
            }
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

        public static async Task AnalysisPost(IWebElement element, Dictionary<string, string> state, WebDriverWrapper webDriverWrapper)
        {
            var tagList = element.FindElements(By.CssSelector(".dm-tag.dm-tag-a"));
            var createTiem = element.FindElement(By.CssSelector(".meta-info.mb20 span")).GetAttribute("innerText");
            var content = Encoding.UTF8.GetString(Encoding.Default.GetBytes(element.FindElement(By.CssSelector(".content")).GetAttribute("innerHTML")));
            var postImgUrlList = element.FindElements(By.CssSelector(".album .inner-container img"));
            var nickName = state["nickName"];
            var avatar = state["avatar"];
            var userModel = GetOrSetUser(nickName, avatar);
            var dbContext = GetDbContext();
            var postImgList = new List<PostEntryFile>();
            var imgindex = 1;
            if (dbContext.PostEntry.Any(x => x.TextContent == content))
            {
                return;
            }
            foreach (var item in postImgUrlList)
            {
                var oldPath = item.GetAttribute("src");
                var key = "postEntryFile/" + nickName + "/" + Guid.NewGuid().ToString();
                await UploadQiniu(oldPath, key);
                postImgList.Add(new PostEntryFile
                {
                    ActualPath = "https://mioto.milbit.com/" + key,
                    Name = key,
                    Type = PostEntryFileType.Image,
                    Order = imgindex,
                    Tag = "半次元"
                });
                imgindex++;
            }

            var postEntry = new PostEntry
            {
                Id = Guid.NewGuid(),
                CreateTime = DateTime.Parse(createTiem),
                TimeStamp = TimeStamp.Get(),
                TextContent = content,
                PostEntryFileList = postImgList,
                UserId = userModel.Id,
                PostEntryTags = new List<PostEntryTag> {
                      new PostEntryTag {
                           CreateTime =DateTime.Now,
                            Id = Guid.NewGuid(),
                             Name ="Coser"
                      }
                 }
            };

            dbContext.PostEntry.Add(postEntry);
            await dbContext.SaveChangesAsync();
        }

        public static async Task AnalysisProfile(IWebElement element, Dictionary<string, string> state, WebDriverWrapper webDriverWrapper)
        {
            for (int i = 0; i < 4; i++)
            {
                webDriverWrapper.WebDriver.ExecuteScript("window.scrollTo(0,document.body.offsetHeight)");
                Thread.Sleep(2 * 1000);
            }
            var body = element.FindElement(By.TagName("body")).GetAttribute("innerHTML");

            var profileElement = element.FindElement(By.CssSelector(".user-info"));

            var avatar = profileElement.FindElement(By.CssSelector(".user-info-top img.avatar-img")).GetAttribute("src");
            var nickName = profileElement.FindElement(By.CssSelector(".user-info-bottom .user-info-name")).GetAttribute("innerText");

            var userModel = GetOrSetUser(nickName, avatar);
            var _curState = ExtensionState(state);
            _curState.Add("nickName", nickName);
            _curState.Add("avatar", avatar);

            var exutor = TaskExutor.Instance;
            var contentWrap = element.FindElement(By.CssSelector(".one-fall-li-wrap"));
            var postUrlList = contentWrap.FindElements(By.CssSelector(".desc-content"));
            foreach (var postUrlElement in postUrlList)
            {
                var postUrl = postUrlElement.GetAttribute("href");
                var postSpiderTask = new TaskSpider(postUrl, _curState);
                Task.Run(() => exutor.SetTask(postSpiderTask, AnalysisPost)).Wait();
            }
        }

        public static UserAccountEntry GetOrSetUser(string userName, string avatar)
        {
            var dbcontext = GetDbContext();
            var userModel = dbcontext.Set<UserAccountEntry>().FirstOrDefault(x => x.NickName == userName);
            if (userModel == null)
            {
                userModel = new UserAccountEntry
                {
                    Id = Guid.NewGuid(),
                    NickName = userName,
                    Avatar = avatar,
                };
                dbcontext.Set<UserAccountEntry>().Add(userModel);
                dbcontext.SaveChanges();
            }
            return userModel;
        }

        public async Task SetOtherPost(string userName)
        {

        }

        public async Task SetPost(string url)
        {

        }

        public async Task SetComments(string url)
        {

        }
        public static async Task UploadQiniu(string url, string key)
        {
            var imgStream = DownloadImg(url);
            var coverImgPath = key;
            await QiniuTool.UploadImage(imgStream, coverImgPath);
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
