using System;
using System.Collections.Generic;
using System.Text;

namespace Oryx.SpiderCartoon.Core.V3.SpiderInstances
{
    public class SpiderGuma
    {
        public string ImportUrl = "http://www.gumua.com/";

        public string categoryUrlReg = @"\/Manhua\/\d+\.html";

        public string contentDetailUrlReg = @"\/Manhua\/\d+_\d+\.html";

        public string childrendContentElementCssSelector = ".d_menu a";

        public string contentdetailImageListCssSelecotr = ".r_img  img";
    }
}
