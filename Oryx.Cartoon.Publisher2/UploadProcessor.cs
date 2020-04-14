using Microsoft.EntityFrameworkCore;
using Oryx.CommonDbOperation;
using Oryx.Content.Model;
using Oryx.Utilities;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Oryx.Cartoon.Publisher2
{
    public class UploadProcessor
    {
        public async Task Start(UploadParttern parttern)
        {
            var cateName = parttern.Name;
            var dbCtx = buildDbContext();
            await CreateCategory(dbCtx, cateName);

            var chapterPaths = Directory.GetDirectories(parttern.RootPath);
            for (int pathsIndex = 0; pathsIndex < chapterPaths.Length; pathsIndex++)
            {
                var chapterPath = chapterPaths[pathsIndex];
                var chapterName = parttern.ContentNameProcessor(chapterPath);
                await CreateChapter(dbCtx, cateName, chapterName, pathsIndex);

                var detailImgs = Directory.GetFiles(chapterPath);
                for (int imgIndex = 0; imgIndex < detailImgs.Length; imgIndex++)
                {
                    var imgFilePaht = detailImgs[imgIndex];
                    Console.WriteLine($"{pathsIndex}/{chapterPaths.Length} =>{imgIndex}/{detailImgs.Length}");
                    await SvaeChapterDetail(dbCtx, cateName, chapterName, imgFilePaht, imgIndex);
                    //imgTaskList.Add(taskResult);
                }
            }
        }

        public async Task CreateCategory(CommonDbContext dbCtx, string Name)
        {
            var cateModel = await dbCtx.Categories.FirstOrDefaultAsync(x => x.Name == Name);

            if (cateModel == null)
            {
                await dbCtx.Categories.AddAsync(new Content.Model.Categories
                {
                    Id = Guid.NewGuid(),
                    Name = Name,
                    CreateTime = DateTime.Now,
                    Tags = new List<Tags> {
                            new   Tags {  Name = "日漫" },
                            //new   Tags {  Name = "偶像" },
                            //new Tags{ Name ="兄妹"},
                            //new Tags{ Name ="日常"},
                            //new Tags{ Name ="恋爱"}
                       }
                });
                await dbCtx.SaveChangesAsync();
            }
        }

        public async Task CreateChapter(CommonDbContext dbCtx, string cateName, string Name, int order)
        {
            var category = await dbCtx.Categories.FirstOrDefaultAsync(x => x.Name == cateName);

            var hasContent = await dbCtx.ContentEntry.AnyAsync(x => x.Category.Name == cateName && x.Title == Name);

            if (!hasContent)
            {
                await dbCtx.ContentEntry.AddAsync(new ContentEntry
                {
                    Id = Guid.NewGuid(),
                    Category = category,
                    Title = Name,
                    Order = order,
                    CreateTime = DateTime.Now
                });

                await dbCtx.SaveChangesAsync();
            }
        }

        public async Task SvaeChapterDetail(CommonDbContext dbCtx, string cateName, string contentName, string fileName, int order)
        {
            try
            {
                var content = await dbCtx.ContentEntry.Include(x => x.MediaResource).FirstOrDefaultAsync(x => x.Category.Name == cateName && x.Title == contentName);

                var category = await dbCtx.Categories.FirstOrDefaultAsync(x => x.Name == cateName);

                if (content.MediaResource == null)
                {
                    content.MediaResource = new List<FileEntry> { };
                }
                var name = $"{cateName}/{contentName}/{order}img.jpg";

                if (!content.MediaResource.Any(x => x.Name == name))
                {
                    await QiniuTool.UploadImage(fileName, name);
                    content.MediaResource.Add(new FileEntry
                    {
                        Name = name,
                        ActualPath = "https://mioto.milbit.com/" + name,
                        CreateTime = DateTime.Now,
                        Order = order,
                        Tag = cateName
                    });
                }

                await dbCtx.SaveChangesAsync();
            }
            catch (Exception exc)
            {
                Console.WriteLine(exc.Message);
            }
        }

        private CommonDbContext buildDbContext()
        {
            var option = new DbContextOptions<CommonDbContext>();
            var optBuilder = new DbContextOptionsBuilder(option);
            optBuilder.UseMySql("server=139.224.219.2;database=ShopApp;user=root;password=Linengneng123#;Character Set=utf8", opts =>
            {
                opts.CommandTimeout(60);
                opts.EnableRetryOnFailure(3);
                opts.MaxBatchSize(1000);
                opts.MigrationsAssembly("Oryx.CartoonPublisher");
            });
            return new CommonDbContext(option);
        }
    }
}
