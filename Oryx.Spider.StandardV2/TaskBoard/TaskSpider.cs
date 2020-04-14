using OpenQA.Selenium;
using Oryx.Spider.StandardV2.Interfaces;
using Oryx.Spider.StandardV2.WebDriverExtensions;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace Oryx.Spider.StandardV2.TaskBoard
{
    public class TaskSpider
    {
        public TaskWrapper<TaskItem> TaskItem { get; set; }
        public TaskSpider SubTask { get; set; }

        private Cookie cookie { get; set; }

        private string proxy_address { get; set; }

        private string prox_port { get; set; }

        public TaskSpider(string url, Dictionary<string, string> State, TaskSpider subtask = null)
        {
            TaskItem = new TaskWrapper<TaskItem>(new TaskBoard.TaskItem
            {
                Url = url,
                Cookie = cookie,
                ProxyAddress = proxy_address,
                ProxyPort = prox_port,
                State = State
            });
            SubTask = subtask;
        }

        public void SetCookie(string key, string value, string domain, string path, DateTime expire)
        {
            cookie = new Cookie(key, value, domain, path, expire);
        }

        public void SetCookie(Cookie cookie)
        {
            this.cookie = cookie;
        }

        public void SetProxy(string address, string port)
        {
            proxy_address = address;
            prox_port = port;
        }
    }

    public class TaskItem : SpiderTaskInterface
    {
        static Random rdm = new Random();
        static string sessionKey = "wSession";
        static string sessionValue = "206168247FCEAAE5743293F6202DAF9E6D0ADE73";
        static string sessionDomain = ".weehui.com";
        static string sessionPath = "/";
        static OpenQA.Selenium.Cookie _cookie = new OpenQA.Selenium.Cookie(sessionKey, sessionValue, sessionDomain, sessionPath, DateTime.Now.AddDays(3));
        public int RetryTime = 0;
        private PhantomRunner runner { get; set; }
        public TaskItem()
        {
            runner = new PhantomRunner();
        }
        public string Url { get; set; }

        public Cookie Cookie { get; set; }

        public string ProxyAddress { get; set; }

        public string ProxyPort { get; set; }

        public IWebElement WebElement { get; set; }
        public Dictionary<string, string> State { get; internal set; }

        public Action<IWebElement, Dictionary<string, string>> GetResult;
        public Func<IWebElement, Dictionary<string, string>, WebDriverWrapper, Task> GetResultWithTask;

        public Task TaskExcute()
        {
            //var pxyTuple = GetProxy();
            //runner.SetProxy(pxyTuple.Item1, pxyTuple.Item2);
            var driver = runner.GetDriver(null, null);

            driver.WebDriver.Manage().Cookies.AddCookie(_cookie);
            driver.Status = WebDriverExtensions.WebDriverStatus.Busy;
            driver.WebDriver.Navigate().GoToUrl(Url);
            WebElement = driver.WebDriver.FindElement(By.TagName("html"));
            driver.Status = WebDriverExtensions.WebDriverStatus.Ready;
            GetResult?.Invoke(WebElement, State);
            GetResultWithTask?.Invoke(WebElement, State, driver).Wait();
            driver.WebDriver.Quit();
            return Task.CompletedTask;
        }



        private Tuple<string, string> GetProxy()
        {
            var proxyList = File.ReadAllLines(AppDomain.CurrentDomain.BaseDirectory + "proxyList.txt");

            var rdmIndex = rdm.Next(0, proxyList.Length);
            var proxyStrArr = proxyList[rdmIndex].Split(':');
            return new Tuple<string, string>(proxyStrArr[0], proxyStrArr[1]);
        }
    }
}
