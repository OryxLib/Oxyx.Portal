using OpenQA.Selenium;
using Oryx.SpiderCore.Ultilities;
using System;

namespace Oryx.Spider.vCore
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            var runner = new PhantomRunner();
            var driver = runner.Create("http://www.weehui.com/user/");
            var body = driver.FindElement(By.TagName("body"));
            var html = body.GetAttribute("innerHTML");
            Console.WriteLine(html);
            Console.ReadLine();
        }
    }
}
