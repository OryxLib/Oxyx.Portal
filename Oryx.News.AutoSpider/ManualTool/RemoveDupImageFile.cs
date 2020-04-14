using System;
using System.Collections.Generic;
using System.Text;
using OpenQA.Selenium;
using Oryx.Spider.StandardV3;
using Oryx.Spider.StandardV3.Infrastructures;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Oryx.Spider.StandardV3.Ultilities;
using System.Text.RegularExpressions;
using Oryx.CommonDbOperation;
using Microsoft.EntityFrameworkCore;
using Oryx.UserAccount.Model;
using Oryx.Content.Model;
using System.Linq;
using Oryx.Content.Model.ContentEntryExtension;
using System.Net;

namespace Oryx.News.AutoSpider
{
    public class RemoveDupImageFile
    {
        CommonDbContext dbContext;
        public RemoveDupImageFile()
        {
            dbContext = GetDbContext();
        }

        public void Start()
        {
            var targetImgFiles = dbContext.FileEntry.Where(x => x.Name == "Commission 50集全 无水印").Select(x => x.ActualPath).Distinct().ToList();

            foreach (var imgItem in targetImgFiles)
            {
                var imgs = dbContext.FileEntry.Where(x => x.ActualPath == imgItem).ToList();
                if (imgs.Count == 1)
                {
                    continue;
                }

                var tmpToRemoveImgList = imgs.Skip(1);

                dbContext.RemoveRange(tmpToRemoveImgList);
                dbContext.SaveChanges();
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
