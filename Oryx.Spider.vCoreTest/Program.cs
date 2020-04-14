using OpenQA.Selenium;
using OpenQA.Selenium.PhantomJS;
using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;

namespace Oryx.Spider.vCoreTest
{
    class Program
    {
        static void Main(string[] args)
        {
            var sessionKey = "wSession";
            var sessionValue = "00E85263AC13FF6F99BF9D3942A1E130E88FC963";
            var sessionDomain = ".weehui.com";
            var sessionPath = "/";
            Console.WriteLine("Hello World!");
            var cookie = new Cookie(sessionKey, sessionValue, sessionDomain, sessionPath, DateTime.Now.AddDays(3));

            var runner = new PhantomRunner();
            var driver = (PhantomJSDriver)runner.Create("http://www.weehui.com/user/", cookie);
             
            //driver.ExecuteScript("document.getElementById('name').value='407815932@qq.com'");
            //driver.ExecuteScript("document.getElementById('password').value='123456'");
            //driver.ExecuteScript("document.querySelector('.btnRow .btnCommon').click()");
            var body = driver.FindElement(By.TagName("body"));
            var html = body.GetAttribute("innerHTML");
            var cookies = driver.Manage().Cookies.AllCookies;
            //foreach (var item in cookies)
            //{
            //    Console.WriteLine(item.Name + " : " + item.Value);
            //}

            Console.WriteLine(html);
            Console.WriteLine("========================");
            var cookie2 = new Cookie(cookies[0].Name, cookies[0].Value, cookies[0].Domain, cookies[0].Path, cookies[0].Expiry);
            var driver2 = (PhantomJSDriver)runner.Create("http://www.weehui.com/user/", cookie);
            //driver2.Manage().Cookies.DeleteAllCookies();
            //driver2.Manage().Cookies.AddCookie(cookie)
            driver2.Navigate().GoToUrl("http://www.weehui.com/user/");
            var body2 = driver2.FindElement(By.TagName("body"));
            var html2 = body2.GetAttribute("innerHTML");
            //var cookies2 = driver2.Manage().Cookies.AllCookies;
            //foreach (var item in cookies2)
            //{
            //    Console.WriteLine(item.Name + " : " + item.Value);
            //}
            Console.WriteLine(html2);
            Console.WriteLine("========================");
            var cookie3 = new Cookie(cookies[0].Name, cookies[0].Value, cookies[0].Domain, cookies[0].Path, cookies[0].Expiry);
            var driver3 = (PhantomJSDriver)runner.Create("http://www.weehui.com/user/", cookie);

            driver3.Navigate().GoToUrl("http://www.weehui.com/user/");
            var body3 = driver3.FindElement(By.TagName("body"));
            var html3 = body3.GetAttribute("innerHTML");

            Console.WriteLine(html3);

            Console.WriteLine("========================");
            var cookie4 = new Cookie(cookies[0].Name, cookies[0].Value, cookies[0].Domain, cookies[0].Path, cookies[0].Expiry);
            var driver4 = (PhantomJSDriver)runner.Create("http://www.weehui.com/user/", cookie);
            //driver4.Manage().Cookies.DeleteAllCookies();

            driver4.Navigate().GoToUrl("http://www.weehui.com/user/");
            var body4 = driver4.FindElement(By.TagName("body"));
            var html4 = body4.GetAttribute("innerHTML");

            Console.WriteLine(html4);
            Console.ReadLine();
        }
    }
}
