using Microsoft.EntityFrameworkCore;
using Oryx.CommonDbOperation;
using Oryx.Content.Model;
using System.Linq;
using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using BitMiracle.Docotic.Pdf;
using Oryx.Utilities;
using System.Threading;
using System.Threading.Tasks;

namespace Oryx.CartoonPublisher
{
    public class UplaodCartoonManual
    {
        public string DirectoryPath { get; set; }

        public string CategoryName { get; set; }

        public Categories Categories { get; set; }

        public UplaodCartoonManual(string dir, string name)
        {
            DirectoryPath = dir;
            CategoryName = name;
        }

        public async Task BuildCategory()
        {
            var dbContext = buildDbContext();
            Categories = await dbContext.Categories.FirstOrDefaultAsync(x => x.Name == CategoryName);
            if (Categories == null)
            {
                Categories = new Categories
                {
                    Id = Guid.NewGuid(),
                    CreateTime = DateTime.Now,
                    Name = CategoryName
                };
                dbContext.Categories.Add(Categories);
                await dbContext.SaveChangesAsync();
            }

           await BuildChapter(dbContext, CategoryName);
        }

        public async Task BuildChapter(CommonDbContext dbContext, string categoryName)
        {
            var category =await dbContext.Categories.Include("ContentList").Include("ContentList.MediaResource").FirstOrDefaultAsync(x => x.Name == categoryName);
            var PDFFiles = Directory.EnumerateFiles(DirectoryPath);
            var chapterIndex = 1;
            foreach (var pdfItem in PDFFiles)
            {
                var streamStreamList = PDFTool.PDFToImage(pdfItem);
                //chapter level
                var chapterName = $"第{chapterIndex}话";
                var chapterContent = await prepareChapter(dbContext, category, chapterName, chapterIndex);
                var imgIndex = 1;
                foreach (var imgStream in streamStreamList)
                {
                    var imgName = "cartoon/" + chapterName + "/img" + imgIndex + ".jpg";
                    await prepareImage(dbContext, chapterContent, imgStream, imgName);
                    imgIndex++;
                }
                chapterIndex++;
            }
        }

        private static async Task prepareImage(CommonDbContext dbContext, ContentEntry chapterContent, Stream imgStream, string imgName)
        {
            var mediaImg = chapterContent.MediaResource?.FirstOrDefault(x => x.Name == imgName);
            if (mediaImg == null)
            {
                if (chapterContent.MediaResource == null)
                {
                    chapterContent.MediaResource = new List<FileEntry>();
                }
                //图片判重
                if (chapterContent.MediaResource.Any(x => x.Name == imgName))
                {
                    return;
                }
                imgStream.Position = 0;
                QiniuTool.UploadImage(imgStream, imgName).Wait();
                chapterContent.MediaResource.Add(new FileEntry
                {
                    ActualPath = "https://mioto.milbit.com/" + imgName,
                    Name = imgName,
                    CreateTime = DateTime.Now,
                    Id = Guid.NewGuid()
                });
                await dbContext.SaveChangesAsync();
            }
        }

        private static async Task<ContentEntry> prepareChapter(CommonDbContext dbContext, Categories category, string chapterName, int chapterIndex)
        {
            var chapter = category.ContentList.FirstOrDefault(x => x.Title == chapterName);
            if (chapter == null)
            {
                chapter = new ContentEntry
                {
                    Id = Guid.NewGuid(),
                    CreateTime = DateTime.Now,
                    Category = category,
                    Title = chapterName,
                    Order = chapterIndex
                };
                dbContext.ContentEntry.Add(chapter);
                await dbContext.SaveChangesAsync();
                dbContext.Entry(chapter);
            }
            return chapter;
        }

        private static CommonDbContext buildDbContext()
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
