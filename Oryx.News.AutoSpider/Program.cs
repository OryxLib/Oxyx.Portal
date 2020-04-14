using Oryx.News.AutoSpider.ManualTool;
using System;

namespace Oryx.News.AutoSpider
{
    class Program
    {
        static void Main(string[] args)
        {
            //Dongmanzhijia dongmanzhijia = new Dongmanzhijia();
            //dongmanzhijia.Start();

            //var qiniuer = new Reloadfox800ToQiniu();
            //qiniuer.Start();
            //var uploader = new Manhuadaquan();
            //uploader.Start();
            //空漫画检查
            var checker = new TestContentImageNotFound();
            checker.StartCheck();
            //checker.RevertGumuaCartoonContentOrder(); 
            //var remover = new RemoveDupImageFile();
            //remover.Start();

            //var setter = new SetTopCategoryNewsAndCartoon();
            //setter.StartSet();
            //var acgTask = new SohuACGTask();
            //acgTask.SetCategoryTas();
            //Console.ReadLine();

            //var yaoqiManhua = new SpiderYaoqiManhua();
            //yaoqiManhua.Start().Wait();
            //Console.ReadLine();
            Console.WriteLine("End~");
        }
    }
}
