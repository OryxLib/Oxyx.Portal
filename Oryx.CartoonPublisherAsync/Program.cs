using Autofac;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using Oryx.CommonDbOperation;
using Oryx.Content.Model;
using Oryx.Utilities;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions; 
using System.Threading;
using System.Threading.Tasks;

namespace Oryx.CartoonPublisher
{
    class Program
    {
        static CommonDbContext dbPool = null;

        //static List<Tuple<string, string>> dirPathMapping = new List<Tuple<string, string>>();

        static void Main(string[] args)
        {
            var dirPathMapping = new List<Tuple<string, string>> { 
                //    Tuple.Create(@"D:\BaiduNetdiskDownload\淘宝漫画\A版本\001","蛇精巢穴"),
                //    Tuple.Create(@"D:\BaiduNetdiskDownload\淘宝漫画\A版本\002","我的秘密炮友"),
                //    Tuple.Create(@"D:\BaiduNetdiskDownload\淘宝漫画\A版本\003","制服的诱惑") ,
                //    Tuple.Create(@"D:\BaiduNetdiskDownload\淘宝漫画\A版本\005","邻家少女") ,
                //    Tuple.Create(@"D:\BaiduNetdiskDownload\淘宝漫画\A版本\006","女助教") ,
                //    Tuple.Create(@"D:\BaiduNetdiskDownload\淘宝漫画\A版本\007","脱GG小岛"), 
                //    Tuple.Create(@"D:\BaiduNetdiskDownload\淘宝漫画\A版本\008","校园第1季"), 
                //    Tuple.Create(@"D:\BaiduNetdiskDownload\淘宝漫画\A版本\009","亚哈路第1季"), 
                //    Tuple.Create(@"D:\BaiduNetdiskDownload\淘宝漫画\A版本\010","亚哈路第2季") ,
                //    Tuple.Create(@"D:\BaiduNetdiskDownload\淘宝漫画\A版本\011","诱惑第一季") ,
                //    Tuple.Create(@"D:\BaiduNetdiskDownload\淘宝漫画\A版本\012","诱惑第二季") ,
                //    Tuple.Create(@"D:\BaiduNetdiskDownload\淘宝漫画\A版本\013","愉快的旅行") ,
                //    Tuple.Create(@"D:\BaiduNetdiskDownload\淘宝漫画\A版本\獠牙","獠牙")  
                Tuple.Create(@"D:\BaiduNetdiskDownload\淘宝漫画\B版本\06","出轨"),
                Tuple.Create(@"D:\BaiduNetdiskDownload\淘宝漫画\B版本\07","初恋症候群"),
                Tuple.Create(@"D:\BaiduNetdiskDownload\淘宝漫画\B版本\08","崔強性氣與朴銀慧"),
                Tuple.Create(@"D:\BaiduNetdiskDownload\淘宝漫画\B版本\09","猪圈"),
                Tuple.Create(@"D:\BaiduNetdiskDownload\淘宝漫画\B版本\10","大叔"),
                Tuple.Create(@"D:\BaiduNetdiskDownload\淘宝漫画\B版本\11","堕落游戏"),
                Tuple.Create(@"D:\BaiduNetdiskDownload\淘宝漫画\B版本\12","二十再重来"),
                Tuple.Create(@"D:\BaiduNetdiskDownload\淘宝漫画\B版本\13","法兰克赵"),
                Tuple.Create(@"D:\BaiduNetdiskDownload\淘宝漫画\B版本\14","非一般关系"),
                Tuple.Create(@"D:\BaiduNetdiskDownload\淘宝漫画\B版本\16","激情分享屋"),
                Tuple.Create(@"D:\BaiduNetdiskDownload\淘宝漫画\B版本\18","假戏真做　（TOUCH ME）"),
                Tuple.Create(@"D:\BaiduNetdiskDownload\淘宝漫画\B版本\19","教练教教我"),
                Tuple.Create(@"D:\BaiduNetdiskDownload\淘宝漫画\B版本\21","觉醒"),
                Tuple.Create(@"D:\BaiduNetdiskDownload\淘宝漫画\B版本\24\PDF","恋爱辅助器"),
                Tuple.Create(@"D:\BaiduNetdiskDownload\淘宝漫画\B版本\49","邻家少女第2季"),
                Tuple.Create(@"D:\BaiduNetdiskDownload\淘宝漫画\B版本\71B","情欲灵药"),
                Tuple.Create(@"D:\BaiduNetdiskDownload\淘宝漫画\B版本\捕猎母猪","捕猎母猪"),
            };
            var taskList = new List<Task>();
            foreach (var item in dirPathMapping)
            {
                var imgManual = new UplaodCartoonManual(item.Item1, item.Item2);
                var taskItem = imgManual.BuildCategory();
                taskList.Add(taskItem);
            }
            Task.WaitAll(taskList.ToArray());
            Console.WriteLine("处理完成");
            Console.Read();
            //StartProcess();
            //DeleteCartoon();
            //PDFTool.PDFToImage(@"D:\BaiduNetdiskDownload\淘宝漫画\0200\30ｃｍ第1季-001.pdf", @"D:\BaiduNetdiskDownload\淘宝漫画\0200\30ｃｍ第1季-001", "jpg");
        }



        public static void DeleteCartoon()
        {
            var dbcontext = buildDbContext();

            var targetCategoryList = dbcontext.Categories
                .Include(x => x.Tags)
                .Include(x => x.ChildrenCategories)
                .Include(x => x.MediaResource)
                .Include(x => x.ContentList)
                .Include("ChildrenCategories.ContentList")
                .Include("ContentList.MediaResource")
                .Include("ChildrenCategories.MediaResource").Where(x => x.ParentCategory == null && x.Tags.Any(c => c.Name == "SpiderUplaod")).ToList();

            foreach (var topCategory in targetCategoryList)
            {
                //if (topCategory.ChildrenCategories != null && topCategory.ChildrenCategories.Count > 0)
                //{
                //    foreach (var subCategory in topCategory.ChildrenCategories)
                //    {
                //        if (subCategory.ContentList != null && subCategory.ContentList.Count > 0)
                //        {
                //            foreach (var contentItem in subCategory.ContentList)
                //            {
                //                QiniuTool.DeleteImage(contentItem.MediaResource.Select(x => x.ActualPath.Replace("https://mioto.milbit.com/", "")).ToArray()).Wait();
                //                dbcontext.Remove(contentItem.MediaResource);
                //                dbcontext.SaveChanges();
                //            }
                //            dbcontext.RemoveRange(subCategory.ContentList);
                //        }
                //        dbcontext.SaveChanges();
                //    }
                //    dbcontext.RemoveRange(topCategory.ChildrenCategories);
                //    dbcontext.SaveChanges();
                //}
                //else
                //{
                foreach (var contentItem in topCategory.ContentList)
                {
                    if (contentItem.MediaResource != null && contentItem.MediaResource.Count > 0)
                    {
                        QiniuTool.DeleteImage(contentItem.MediaResource.Select(x => x.ActualPath.Replace("https://mioto.milbit.com/", "")).ToArray()).Wait();
                        dbcontext.RemoveRange(contentItem.MediaResource);
                        dbcontext.SaveChanges();
                    }
                }

                dbcontext.RemoveRange(topCategory.ContentList);
                //}
                dbcontext.Remove(topCategory);
                if (topCategory.Tags != null && topCategory.Tags.Count > 0)
                    dbcontext.RemoveRange(topCategory.Tags);

                dbcontext.SaveChanges();
            }

            //foreach (var item in dbcontext.ContentEntry.Include(x => x.MediaResource))
            //{
            //    QiniuTool.DeleteImage(item.MediaResource.Select(x => x.ActualPath.Replace("https://mioto.milbit.com/", "")).ToArray()).Wait();
            //}
        }

        private static void RemoveContent(CommonDbContext dbcontext, Categories categories)
        {
            //dbcontext.Entry(contentList);
            foreach (var contentItem in categories.ContentList)
            {
                QiniuTool.DeleteImage(contentItem.MediaResource.Select(x => x.ActualPath.Replace("https://mioto.milbit.com/", "")).ToArray()).Wait();
                //dbcontext.Entry(contentItem.MediaResource);
                //dbcontext.Remove(contentItem.MediaResource);
            }
            dbcontext.RemoveRange(categories.ContentList);
            dbcontext.SaveChanges();
        }

        private static void AddCartoon()
        {
            var dbcontext = buildDbContext();
            Console.WriteLine("请输入路径, 并按回车: 然后等待程序退出;");
            var path = Console.ReadLine();

            //var path = @"C:\Users\v-huamli\Documents\WeChat Files\lihuaming4000\Files\花美男幼儿园1";
            //var path = @"G:\SystemDiretory\Downloads\1-22";
            var matchRegex = new Regex(@"第(\d+)话");
            var topCategories = Directory.EnumerateDirectories(path).ToList();
            var topCateEntryList = new List<Categories>();

            foreach (var topCate in topCategories)
            {
                var topCatTmpArr = topCate.Split("\\");
                var topCatName = topCatTmpArr[topCatTmpArr.Length - 1];
                var topCateEntry = new Categories
                {
                    CreateTime = DateTime.Now,
                    Id = Guid.NewGuid(),
                    ChildrenCategories = new List<Categories>(),
                    Name = topCatName
                };
                dbcontext.Categories.AddAsync(topCateEntry).Wait();
                dbcontext.SaveChangesAsync().Wait();

                //漫画名
                var secPath = Path.Combine(path, topCate);

                //章节名
                var secondCategories = Directory.EnumerateDirectories(secPath).OrderBy(x =>
                {
                    var matchVal = matchRegex.Match(x);
                    var intVal = matchVal.Groups[1].Value;
                    return int.Parse(intVal);
                }).ToList();
                //var contentList = new List<ContentEntry>();
                var order = 0;
                foreach (var secCate in secondCategories)
                {
                    var secCateTmpArr = secCate.Split("\\");
                    var secCateName = secCateTmpArr[secCateTmpArr.Length - 1];
                    //图片名
                    var thrPath = Path.Combine(secPath, secCate);
                    var fileNameList = Directory.EnumerateFiles(thrPath).ToList();
                    var fileEntryList = new List<FileEntry>();
                    var fileUploadTaskList = new List<Task>();
                    var contentEntry = new ContentEntry
                    {
                        Category = topCateEntry,
                        CreateTime = DateTime.Now,
                        Id = Guid.NewGuid(),
                        Title = secCateName,
                        Order = order++
                    };
                    foreach (var fileName in fileNameList)
                    {
                        var saveKey = "cartoon/" + topCatName
                            + "-" + secCateName + "-" + Path.GetFileName(fileName);
                        var task = QiniuTool.UploadImage(fileName, saveKey);
                        fileEntryList.Add(new FileEntry
                        {
                            ActualPath = "https://mioto.milbit.com/" + saveKey,
                            CreateTime = DateTime.Now,
                            Name = saveKey,
                            Tag = saveKey
                        });
                        fileUploadTaskList.Add(task);
                    }
                    Task.WaitAll(fileUploadTaskList.ToArray());
                    contentEntry.MediaResource = fileEntryList;
                    dbcontext.ContentEntry.AddAsync(contentEntry).Wait();
                    dbcontext.SaveChangesAsync().Wait();
                }
            }

            dbcontext.SaveChangesAsync().Wait();
        }

        private static CommonDbContext buildDbContext()
        {
            if (dbPool == null)
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
                dbPool = new CommonDbContext(option);
            }

            return dbPool;
        }

        static string Tag;
        static void StartProcess()
        {
            Console.WriteLine("请输入 Tag : ");
            Tag = Console.ReadLine();
            Console.WriteLine("请输入 路径 :");
            var path = Console.ReadLine();
            AnalyseDic(path, "");
            Console.WriteLine("执行完毕, 回车退出");
            Console.ReadLine();
        }

        static void AnalyseDic(string dirPath, string dirName)
        {
            if (!Directory.Exists(dirPath))
            {
                return;
            }

            var subFiles = Directory.GetFiles(dirPath);
            var subDirectories = Directory.GetDirectories(dirPath);
            var dbContext = buildDbContext();


            Categories category = null;
            ContentEntry content = null;
            if (!string.IsNullOrEmpty(dirName))
            {
                category = dbContext.Categories.FirstOrDefault(x => x.Name == dirName);
                content = new ContentEntry
                {
                    Id = Guid.NewGuid(),
                    Title = dirName,
                    CreateTime = DateTime.Now,
                    Category = category,
                    MediaResource = new List<FileEntry> { }
                };
                dbContext.ContentEntry.Add(content);
            }

            if (string.IsNullOrEmpty(dirName))
            {
                foreach (var dir in subDirectories)
                {
                    var dirTmpArr = dir.Split("\\");
                    var curDirName = dirTmpArr[dirTmpArr.Length - 1];
                    if (string.IsNullOrEmpty(dirName))
                    {
                        dbContext.Categories.Add(new Categories
                        {
                            CreateTime = DateTime.Now,
                            Id = Guid.NewGuid(),
                            Name = curDirName,
                            Tags = new List<Tags> {
                            new Tags {
                                Id = Guid.NewGuid(),
                                Name  = Tag
                            }
                        }
                        });
                        dbContext.SaveChanges();
                    }
                    AnalyseDic(dir, curDirName);
                }
            }

            foreach (var file in subFiles)
            {
                var fileName = Path.GetFileName(file);
                var actualFileName = ProcessFile(file, dirName);
                if (!string.IsNullOrEmpty(actualFileName))
                {
                    var fileEntry = new FileEntry
                    {
                        Name = fileName,
                        ActualPath = actualFileName,
                        CreateTime = DateTime.Now,
                        Id = Guid.NewGuid()
                    };
                    if (content == null)
                    {
                        content = new ContentEntry
                        {
                            Id = Guid.NewGuid(),
                            Title = dirName,
                            CreateTime = DateTime.Now,
                            Category = category,
                            MediaResource = new List<FileEntry> { }
                        };
                        dbContext.ContentEntry.Add(content);
                    }
                    content.MediaResource.Add(fileEntry);
                    dbContext.FileEntry.Add(fileEntry);
                }
                if (content.MediaResource.Count > 0)
                {
                    dbContext.SaveChanges();
                }
            }
        }

        static string ProcessFile(string filePath, string dirName)
        {
            var extention = Path.GetExtension(filePath);
            var fileName = "cartoon/" + Path.GetRandomFileName();
            switch (extention)
            {
                case ".zip":
                    var outpuDirPaht = ZipTool.Exact(filePath);
                    AnalyseDic(outpuDirPaht, dirName);
                    break;
                case ".pdf":
                    var outpuStream = PDFTool.PDFToImage(filePath);
                    //QiniuTool.UploadImage(outpuStream, fileName).Wait();
                    break;
                case ".jpg":
                case ".jpeg":
                    QiniuTool.UploadImage(filePath, fileName).Wait();
                    break;
            }

            return fileName;
        }
    }
}
