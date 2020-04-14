using Microsoft.EntityFrameworkCore;
using Oryx.CommonDbOperation;
using Oryx.Content.Model;
using Oryx.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Oryx.Uploader.Business
{
    public class ContentUploadCreator
    {
        CommonDbContext dbContext;

        public ContentUploadCreator()
        {
            dbContext = buildDbContext();
        }

        public void DeleteQIniu()
        {
            var imgKeyList = dbContext.FileEntry.Where(x => x.ActualPath.Contains("cartoon/toptoon/今天在哪嘞(无水印)")).Select(x => x.ActualPath.Replace("http://mioto.milbit.com/", "")).ToArray();
            QiniuTool.DeleteImage(imgKeyList).Wait();
        }


        public void CreateCategory(string name, ContentStatus status, params string[] tags)
        {
            var hasCategory = dbContext.Set<Categories>().Any(x => x.Name == name);

            if (!hasCategory)
            {
                var tagsList = new List<Tags>();
                if (tags != null || tags.Length > 1)
                {
                    foreach (var tag in tags)
                    {
                        tagsList.Add(new Tags
                        {
                            Id = Guid.NewGuid(),
                            Name = tag
                        });
                    }
                }
                var cateCartoon = GetTopCategory("漫画");
                var category = new Categories
                {
                    Id = Guid.NewGuid(),
                    CreateTime = DateTime.Now,
                    Name = name,
                    Tags = tagsList,
                    ParentCategory = cateCartoon,
                    Status = status
                };
                dbContext.Set<Categories>().Add(category);
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

        public void SetContentIImg(string name, string cateName, string fileName, int index)
        {
            var content = dbContext.Set<ContentEntry>().Include("MediaResource").FirstOrDefault(x => x.Category.Name == cateName && x.Title == name);
            if (content.MediaResource == null)
            {
                content.MediaResource = new List<FileEntry>();
            }
            var hasImg = content.MediaResource?.Any(x => x.ActualPath.Contains(fileName)) ?? false;
            if (!hasImg)
            {
                Thread.Sleep(20);
                content.MediaResource.Add(new FileEntry
                {
                    Name = cateName,
                    ActualPath = "http://mioto.milbit.com/" + fileName,
                    Order = index,
                    Id = Guid.NewGuid(),
                    CreateTime = DateTime.Now
                });
                ////await dbContext.FileEntry.Add(new FileEntry
                ////{
                ////    ActualPath = cateName,
                ////    CreateTime = DateTime.Now,
                ////    Id = Guid.NewGuid(),
                ////    Name = cateName
                ////});
                dbContext.SaveChanges();
            }
        }

        public void CreateContent(string name, string cateName, int contentOrder)
        {
            var contentModel = dbContext.Set<ContentEntry>().FirstOrDefault(x => x.Category.Name == cateName && x.Title == name);
            if (contentModel == null)
            {
                var category = dbContext.Set<Categories>().FirstOrDefault(x => x.Name == cateName);
                dbContext.Set<ContentEntry>().Add(new ContentEntry
                {
                    Category = category,
                    Title = name,
                    CreateTime = DateTime.Now,
                    Id = Guid.NewGuid(),
                    Order = contentOrder
                });
                dbContext.SaveChanges();
            }
            else
            {
                if (contentModel.MediaResource != null && contentModel.MediaResource.Count > 0)
                {
                    contentModel.MediaResource.RemoveAll(x => 1 == 1);
                    dbContext.SaveChanges();
                }
            }
        }

           private static CommonDbContext buildDbContext()
        {
            //if (dbPool == null)
            //{
            var option = new DbContextOptions<CommonDbContext>();
            var optBuilder = new DbContextOptionsBuilder(option);
            optBuilder.UseMySql("server=139.224.219.2;database=ShopApp;user=root;password=Linengneng123#;Character Set=utf8;connect timeout=10000", opts =>
            {
                opts.CommandTimeout(60);
                opts.EnableRetryOnFailure(3);
                opts.MaxBatchSize(1000);
                opts.MigrationsAssembly("Oryx.CartoonPublisher");
            });
            return new CommonDbContext(option);
            //}

            //return dbPool;
        }

    }
}
