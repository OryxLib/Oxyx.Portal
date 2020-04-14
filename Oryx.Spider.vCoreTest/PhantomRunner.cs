using OpenQA.Selenium;
using OpenQA.Selenium.PhantomJS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Oryx.Spider.vCoreTest
{
    public class PhantomRunner
    {
        public PhantomJSDriverService driverService;

        public PhantomRunner()
        {
            driverService = PhantomJSDriverService.CreateDefaultService(AppDomain.CurrentDomain.BaseDirectory);
            driverService.LoadImages = false;
            driverService.ProxyType = "http";
            driverService.Proxy = "81.169.141.196:80";
        }

        public IWebDriver Create(string url, Cookie cookie = null)
        {
            driverService.HideCommandPromptWindow = true;
            try
            {
                var driver = new PhantomJSDriver(driverService);
                try
                {
                    if (cookie != null)
                    {
                        driver.Manage().Cookies.AddCookie(cookie);
                    }
                }
                catch (Exception exc)
                {

                }
                driver.Manage().Timeouts().SetPageLoadTimeout(new TimeSpan((long)120E7));
                driver.Navigate().GoToUrl(url);
                return driver;
            }
            catch (Exception exc)
            {
                Console.WriteLine("err when navigate to : ");
                Console.WriteLine(exc.Message);
            }
            return null;
        }
    }
}