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
    public class TestContentImageNotFound
    {
        CommonDbContext dbContext;
        public TestContentImageNotFound()
        {
            dbContext = GetDbContext();
        }

        /// <summary>
        /// 转置排序错误
        /// </summary>
        public void RevertGumuaCartoonContentOrder()
        {
            var anchorCategory = dbContext.Set<Categories>().FirstOrDefault(x => x.Name == "都是果贷惹的祸");
            var categoryList = dbContext.Set<Categories>().Include(x => x.ContentList).Where(x => x.ContentList != null && x.ContentList.Count > 0 && x.Status != ContentStatus.Close).ToList();
            foreach (var category in categoryList)
            {
                var contentList = category.ContentList.OrderBy(x => x.Order);
                var contentIndex = contentList.Count();
                //foreach (var content in contentList)
                //{
                //    content.Order = contentIndex;
                //    contentIndex--;
                //}
                //dbContext.SaveChanges();
            }
        }

        /// <summary>
        /// 开始检测资源失效
        /// </summary>
        public void StartCheck()
        {
            var categoryList = dbContext.Set<Categories>().Include(x => x.ContentList).Where(x => x.Status != ContentStatus.Close).ToList();
            foreach (var category in categoryList)
            {
                var content = dbContext.Set<ContentEntry>().Include(x => x.MediaResource).FirstOrDefault(x => x.Category == category);
                if (content == null)
                {
                    category.Status = ContentStatus.Close;
                    dbContext.SaveChanges();
                    continue;
                }
                var img = content.MediaResource.FirstOrDefault();
                if (img == null)
                {
                    category.Status = ContentStatus.Close;
                    dbContext.SaveChanges();
                    continue;
                }
                var isNotFound = CheckNotFound(img.ActualPath);
                if (isNotFound)
                {
                    category.Status = ContentStatus.Close;
                    dbContext.SaveChanges();
                }
            }
        }

        /// <summary>
        /// 判断资源失效漫画并下架
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        private bool CheckNotFound(string url)
        {
            try
            {
                var webClient = new WebClient();
                var responseData = webClient.DownloadData(url);
                if (responseData.Length == 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (WebException exc)
            {
                var err = exc;
                return true;
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
