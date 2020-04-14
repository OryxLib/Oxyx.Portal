using System;
using System.Linq;
using System.Threading.Tasks;

namespace Oryx.Cartoon.Publisher2
{
    class Program
    {
        static void Main(string[] args)
        {
            //var pr = new UploadProcessor();
            //var parttern = Create10();
            //Task.Run(async () => await pr.Start(parttern)).Wait();

            //var parttern2 = Create11();
            //Task.Run(async () => await pr.Start(parttern2)).Wait();

            //var parttern3 = Create12();
            //Task.Run(async () => await pr.Start(parttern3)).Wait();

            //var parttern4 = Create13();
            //Task.Run(async () => await pr.Start(parttern4)).Wait();

            //var parttern5 = Create14();
            //Task.Run(async () => await pr.Start(parttern5)).Wait();


            //var parttern6 = Create15();
            //Task.Run(async () => await pr.Start(parttern6)).Wait();

            //var parttern7 = Create16();
            //Task.Run(async () => await pr.Start(parttern7)).Wait();

            //var parttern9 = Create6();
            //Task.Run(async () => await pr.Start(parttern7)).Wait();
            //var parttern8 = Create17();RunWithFile
            //Task.Run(async () => await pr.Start(parttern8)).Wait();
            TopToonUpload uploderToptoon = new TopToonUpload();
            uploderToptoon.Run();


            Console.WriteLine("End !");
            Console.ReadLine();
        }
        //public static UploadParttern Create17()
        //{ 有子文件夹要处理, 下次处理
        //    return new UploadParttern
        //    {
        //        Name = "地雷震",
        //        RootPath = @"E:\BaiduDownload\地雷震",
        //        ContentNameProcessor = (path) =>
        //        {
        //            return path.Split("_").Last();
        //        }
        //    };
        //}
        public static UploadParttern Create16()
        {
            return new UploadParttern
            {
                Name = "测不准的阿波连同学",
                RootPath = @"E:\BaiduDownload\测不准",
                ContentNameProcessor = (path) =>
                {
                    return path.Split("_").Last();
                }
            };
        }
        public static UploadParttern Create15()
        {
            return new UploadParttern
            {
                Name = "菜花洋果子店的好工作",
                RootPath = @"E:\BaiduDownload\菜花\1-70\《菜花洋果子店的好工作》漫画全集",
                ContentNameProcessor = (path) =>
                {
                    return path.Split("_").Last();
                }
            };
        }
        public static UploadParttern Create14()
        {
            return new UploadParttern
            {
                Name = "在地狱边缘呐喊",
                RootPath = @"E:\BaiduDownload\边缘呐喊\《在地狱边缘呐喊》漫画 第1-32话",
                ContentNameProcessor = (path) =>
                {
                    return path.Split("_").Last();
                }
            };
        }
        public static UploadParttern Create13()
        {
            return new UploadParttern
            {
                Name = "橙、半透明、二度眠",
                RootPath = @"E:\BaiduDownload\半透明\《橙、半透明、二度眠》漫画第1-15话",
                ContentNameProcessor = (path) =>
                {
                    return path.Split("_").Last();
                }
            };
        }
        public static UploadParttern Create12()
        {
            return new UploadParttern
            {
                Name = "妹控进行时",
                RootPath = @"E:\BaiduDownload\阿哲与花纯\23ciyuan@《妹控进行时》漫画",
                ContentNameProcessor = (path) =>
                {
                    return path.Split(" ").Last();
                }
            };
        }
        public static UploadParttern Create11()
        {
            return new UploadParttern
            {
                Name = "愚蠢天使与恶魔共舞",
                RootPath = @"E:\BaiduDownload\阿久津\《愚蠢天使与恶魔共舞》漫画 第1-17话",
                ContentNameProcessor = (path) =>
                {
                    return path.Split("_").Last();
                }
            };
        }
        public static UploadParttern Create10()
        {
            return new UploadParttern
            {
                Name = "札月家的杏子妹妹",
                RootPath = @"E:\BaiduDownload\zhayuejia\《札月家的杏子妹妹》漫画",
                ContentNameProcessor = (path) =>
                {
                    return path.Split("_").Last();
                }
            };
        }
        public static UploadParttern Create9()
        {
            return new UploadParttern
            {
                Name = "妹相伴",
                RootPath = @"E:\BaiduDownload\妹写\1-47\《妹相伴+》漫画",
                ContentNameProcessor = (path) =>
                {
                    return path.Split("_").Last();
                }
            };
        }
        public static UploadParttern Create8()
        {
            return new UploadParttern
            {
                Name = "K.O.I 偶像之王",
                RootPath = @"E:\BaiduDownload\KOI\《K.O.I 偶像之王》漫画 第1-20话",
                ContentNameProcessor = (path) =>
                {
                    return path.Split("_").Last();
                }
            };
        }
        public static UploadParttern Create7()
        {
            return new UploadParttern
            {
                Name = "MY HOME HERO",
                RootPath = @"E:\BaiduDownload\HERO\《MY HOME HERO》漫画第1-15话",
                ContentNameProcessor = (path) =>
                {
                    return path.Split("_").Last();
                }
            };
        }
        public static UploadParttern Create6()
        {
            return new UploadParttern
            {
                Name = "多重人格侦探",
                RootPath = @"E:\BaiduDownload\Dcrgzt(MPD-PSYCHO\[Jpg格式]",
                ContentNameProcessor = (path) =>
                {
                    return path.Split("_").Last();
                }
            };
        }
        public static UploadParttern Create5()
        {
            return new UploadParttern
            {
                Name = "Grand Blue",
                RootPath = @"E:\BaiduDownload\Cooking",
                ContentNameProcessor = (path) =>
                {
                    return path.Split("_").Last();
                }
            };
        }
        public static UploadParttern Create4()
        {
            return new UploadParttern
            {
                Name = "Grand Blue",
                RootPath = @"E:\BaiduDownload\Blue\1-39\《Grand Blue》漫画 第1-39话",
                ContentNameProcessor = (path) =>
                {
                    return path.Split("_").Last();
                }
            };
        }
        public static UploadParttern Create3()
        {
            return new UploadParttern
            {
                Name = "源君物语",
                RootPath = @"E:\BaiduDownload\BAKUMAN 01-20卷全+設定書",
                ContentNameProcessor = (path) =>
                {
                    return path.Split("_").Last();
                }
            };
        }
        public static UploadParttern Create2()
        {
            return new UploadParttern
            {
                Name = "源君物语",
                RootPath = @"E:\BaiduDownload\稻叶\《源君物语》第1-298话",
                ContentNameProcessor = (path) =>
                {
                    return path.Split("_").Last();
                }
            };
        }

        public static UploadParttern Create1()
        {
            return new UploadParttern
            {
                Name = "见面之后5秒开始战斗",
                RootPath = @"E:\BaiduDownload\5秒\23ciyuan@1-50\23ciyuan@《见面之后5秒开始战斗》漫画 第1-50话",
                ContentNameProcessor = (path) =>
                {
                    return path.Split("_").Last();
                }
            };
        }
    }
}
