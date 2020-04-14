using Oryx.Spider.StandardV3.Infrastructures;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Oryx.Spider.StandardV3.Ultilities;
using Oryx.Content.Model;
using OpenQA.Selenium.PhantomJS;
using Oryx.Uploader.Business;
using System.Text.RegularExpressions;
using Oryx.CommonDbOperation;
using Microsoft.EntityFrameworkCore;
using System.Net;
using Oryx.Utilities;

namespace Oryx.News.AutoSpider
{
    public class Reloadfox800ToQiniu
    {
        CommonDbContext dbcontext;

        public Reloadfox800ToQiniu()
        {
            dbcontext = GetDbContext();
        }

        public void Start()
        {
            var allNum = dbcontext.Set<FileEntry>().Where(x => x.ActualPath.Contains("img.fox800.xyz")).Count();
            var wc = new WebClient();
            for (int i = 0; i < allNum;)
            {
                var targetRow = dbcontext.Set<FileEntry>().Where(x => x.ActualPath.Contains("img.fox800.xyz")).Skip(i).Take(100).ToList();
                foreach (var item in targetRow)
                {
                    try
                    {
                        var imgBytes = wc.DownloadData(item.ActualPath);
                        var imgStream = new MemoryStream(imgBytes);
                        var savekey = item.ActualPath.Replace("img.fox800.xyz", "fox800/");
                        QiniuTool.UploadImage(imgStream, savekey).Wait();
                        item.ActualPath = "http://mioto.milbit.com/" + savekey;
                        item.Name = "fox800";
                        dbcontext.SaveChanges();

                        Console.WriteLine("process :  " + i);
                    }
                    catch (Exception exc)
                    {
                        Console.WriteLine(exc.Message);
                    }
                }
                i += 100;
                if (i > allNum)
                {
                    i = allNum - 1;
                }
            }
        }

        private static CommonDbContext GetDbContext()
        {
            var option = new DbContextOptions<CommonDbContext>();
            var optBuilder = new DbContextOptionsBuilder(option);
            optBuilder.UseMySql("server=139.224.219.2;database=ShopApp;user=root;password=Linengneng123#;Character Set=utf8", opts =>
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
