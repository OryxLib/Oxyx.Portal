using OpenQA.Selenium;
using OpenQA.Selenium.PhantomJS;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using Oryx.Spider.StandardV2.WebDriverExtensions;
using Microsoft.Extensions.ObjectPool;

namespace Oryx.Spider.StandardV2
{
    public class PhantomRunner
    {
        public PhantomJSDriverService driverService { get; set; }

        private ConcurrentBag<WebDriverWrapper> driverPooling { get; set; }

        private List<Cookie> CookieList { get; set; }

        private int InstanceNumber = 32;

        static ObjectPool<WebDriverWrapper> PhantomJsDriverPool { get; set; }

        public WebDriverWrapper Driver
        {
            get
            {
                //WebDriverWrapper driverWrapper = new WebDriverWrapper();
                //PhantomJsDriverPool.Return(driverWrapper);
                var driver = driverPooling.FirstOrDefault(x => x.Status == WebDriverStatus.Ready);
                if (driver == null)
                {
                    driver = new WebDriverWrapper()
                    {
                        Status = WebDriverStatus.NotReady,
                        WebDriver = new PhantomJSDriver(driverService)
                    };
                    driverPooling.Add(driver);
                }
                driver.WebDriver.ExecutePhantomJS("--proxy=");
                //driver.WebDriver.Manage().
                //var driver = new WebDriverWrapper()
                //{
                //    Status = WebDriverStatus.NotReady,
                //    WebDriver = new PhantomJSDriver(driverService),

                //};

                return driver;
            }
        }

        public PhantomRunner(int instanceNumer = 128)
        {
            driverService = PhantomJSDriverService.CreateDefaultService(AppDomain.CurrentDomain.BaseDirectory);
            driverService.LoadImages = false;
            driverService.HideCommandPromptWindow = false;
            driverService.AddArgument("--webdriver-loglevel=ERROR");
            driverService.LogFile = AppDomain.CurrentDomain.BaseDirectory + "phantomjs.log";
            driverPooling = new ConcurrentBag<WebDriverWrapper>();
            CookieList = new List<Cookie>();
        }

        public void SetCookie()
        {
        }

        public void SetProxy(string address, string port)
        {
            Proxy proxy = new Proxy();
            proxy.HttpProxy = string.Format(address + ":" + port);
            //driverService.Proxy = "{" + address + "}:{" + port + "}";// string.Format("{{0}}:{{1}}", address, port);
            //driverService.Proxy = address + ":" + port;// string.Format("{{0}}:{{1}}", address, port);
            driverService.Proxy = proxy.HttpProxy;// string.Format("{{0}}:{{1}}", address, port);
            driverService.ProxyType = "https";
        }

        public WebDriverWrapper GetDriver(string item1, string item2)
        {
            var driver = driverPooling.FirstOrDefault(x => x.Status == WebDriverStatus.Ready);
            if (driver == null)
            {
                driver = new WebDriverWrapper()
                {
                    Status = WebDriverStatus.NotReady,
                    WebDriver = new PhantomJSDriver(driverService)
                };
                driverPooling.Add(driver);
            }
            if (!string.IsNullOrEmpty(item1) && !string.IsNullOrEmpty(item2))
            {
                String script = "return phantom.setProxy('{0}', {1}, 'http', '', '');";
                string func = string.Format(script, item1, item2);
                driver.WebDriver.ExecutePhantomJS(func);
            } 

            return driver;
        }
    }
}
