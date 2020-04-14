using Microsoft.EntityFrameworkCore;
using Oryx.CommonDbOperation;
using Oryx.Content.Model;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Oryx.Uploader.Business
{
    public class ContentSpiderCreator
    {
        CommonDbContext dbContext;

        public ContentSpiderCreator()
        {
            dbContext = buildDbContext();

        }

        public void CreateCategory(string name, string coverImg, params string[] tags)
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
                var cateCartoon = dbContext.Set<Categories>().FirstOrDefault(x => x.Name == "漫画");
                dbContext.Set<Categories>().Add(new Categories
                {
                    Id = Guid.NewGuid(),
                    CreateTime = DateTime.Now,
                    Name = name,
                    Tags = tagsList,
                    MediaResource = new List<FileEntry> {
                        new FileEntry {
                            ActualPath = coverImg,
                            Id =Guid.NewGuid(),
                              CreateTime = DateTime.Now,
                               Name = "tmppath"
                        }
                    },
                    ParentCategory = cateCartoon
                });
                dbContext.SaveChanges();
            }
        }

        public void SetContentIImg(string name, string cateName, string fileName, int index)
        {
            var content = dbContext.Set<ContentEntry>().Include("MediaResource").FirstOrDefault(x => x.Category.Name.Contains(cateName) && x.Title.Contains(name));
            if (content.MediaResource == null)
            {
                content.MediaResource = new List<FileEntry>();
            }
            var hasImg = content.MediaResource.Any(x => x.Name == fileName);
            if (!hasImg)
            {
                content.MediaResource.Add(new FileEntry
                {
                    Name = "tmppath",
                    ActualPath = fileName,
                    Order = index,
                    Id = Guid.NewGuid(),
                    CreateTime = DateTime.Now
                });
                dbContext.SaveChanges();
            }
        }

        /// <summary>
        /// 临时方法, 每次都会重建Content
        /// </summary>
        /// <param name="name"></param>
        /// <param name="cateName"></param>
        /// <param name="contentOrder"></param>
        /// <param name="imgResourceTemplate"></param>
        /// <param name="totalCount"></param>
        public void CreateNewCategoryAndContentAndLoadImg(string name, string cateName, int contentOrder, string imgResourceTemplate, int totalCount)
        {
            var contentModel = dbContext.Set<ContentEntry>().FirstOrDefault(x => x.Category.Name == cateName && x.Title == name);
            var fileEntryList = new List<FileEntry>();
            for (int pageIndex = 0; pageIndex < totalCount; pageIndex++)
            {
                var path = string.Format(imgResourceTemplate, pageIndex + 1);
                fileEntryList.Add(new FileEntry
                {
                    ActualPath = path,
                    CreateTime = DateTime.Now,
                    Id = Guid.NewGuid(),
                    Name = "temp",
                    Order = contentOrder,
                    Tag = "妖气漫画temp"
                });
            }

            var category = new Categories
            {
                Name = cateName,
                CreateTime = DateTime.Now,
                MediaResource = new List<FileEntry> {
                       new FileEntry{
                            Id = Guid.NewGuid(),
                             ActualPath = string.Format(imgResourceTemplate, 1),
                             CreateTime= DateTime.Now,
                             Name = "temp"
                        }
                 },
                Id = Guid.NewGuid(),
                Tags = new List<Tags> {
                      new Tags {
                          Id=Guid.NewGuid(),
                          Name = "日漫"
                      },
                      new Tags {
                           Id = Guid.NewGuid(),
                           Name = "邪恶漫画"
                      },
                      new Tags {
                           Id = Guid.NewGuid(),
                           Name = "妖气漫画"
                      }
                 }
            };
            dbContext.Set<Categories>().Add(category);
            dbContext.SaveChanges();
            dbContext.Set<ContentEntry>().Add(new ContentEntry
            {
                Category = category,
                Title = name,
                CreateTime = DateTime.Now,
                Id = Guid.NewGuid(),
                Order = contentOrder,
                MediaResource = fileEntryList
            });
            dbContext.SaveChanges();
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
