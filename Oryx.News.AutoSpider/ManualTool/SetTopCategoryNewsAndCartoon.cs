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


namespace Oryx.News.AutoSpider.ManualTool
{
    public class SetTopCategoryNewsAndCartoon
    {
        CommonDbContext dbContext;
        public SetTopCategoryNewsAndCartoon()
        {
            dbContext = GetDbContext();
        }

        public void StartSet()
        {
            var cateCartoon = GetTopCategory("漫画");
            var cateNews = GetTopCategory("资讯");

            var categoryList = dbContext.Categories.Include(x => x.Tags).ToList();
            foreach (var category in categoryList)
            {
                if (category.Tags.Any(x => x.Name == "资讯"))
                {
                    category.ParentCategory = cateNews;
                }
                else
                {
                    category.ParentCategory = cateCartoon;
                }
                dbContext.SaveChanges();
            }
        }

        private Categories GetTopCategory(string name)
        {
            Categories cartoonCate = dbContext.Categories.FirstOrDefault(x => x.Name == name);
            if (cartoonCate == null)
            {
                cartoonCate = new Categories
                {
                    Id = Guid.NewGuid(),
                    Name = name,
                    CreateTime = DateTime.Now
                };
                dbContext.Add(cartoonCate);
                dbContext.SaveChanges();
            }
            return cartoonCate;
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
