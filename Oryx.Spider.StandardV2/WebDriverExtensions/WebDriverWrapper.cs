using OpenQA.Selenium;
using OpenQA.Selenium.PhantomJS;
using System;
using System.Collections.Generic;
using System.Text;

namespace Oryx.Spider.StandardV2.WebDriverExtensions
{
    public class WebDriverWrapper
    {
        public PhantomJSDriver WebDriver { get; set; }

        public WebDriverStatus Status { get; set; }
    }

    public enum WebDriverStatus
    {
        NotReady,
        Ready,
        Busy
    }
}
