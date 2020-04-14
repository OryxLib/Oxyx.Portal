using System;
using System.Collections.Generic;
using System.Text;

namespace Oryx.Cartoon.Publisher2
{
    public class UploadParttern
    {
        public string Name { get; set; }

        public string RootPath { get; set; }
 
        public Func<string, string> ContentNameProcessor { get; set; }



    }
}
