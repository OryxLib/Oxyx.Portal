using Microsoft.EntityFrameworkCore;
using Oryx.CommonDbOperation;
using Oryx.Content.Model;
using Oryx.Uploader.Business;
using Oryx.Utilities;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Oryx.Cartoon.Publisher2
{
    public class TopToonUpload
    {
        const string EntryPath = @"D:\百度云下载\顶通漫画无水印\";
        ContentUploadCreator contentUploadCreator;



        public TopToonUpload()
        {
            contentUploadCreator = new ContentUploadCreator();
        }
        string anchorTitle = "";
        int anchorIndex = 16;
        public void Run()
        {
            //delete key 
            //contentUploadCreator.DeleteQIniu();
            //return;
            var dirList = Directory.EnumerateDirectories(EntryPath).OrderBy(x => x);

            var targetList = dirList.SkipWhile(x => !x.Contains(anchorTitle)).ToList();
            //var targetList = dirList.Where(x => x.Contains(anchorTitle)).ToList();
            foreach (var dir in targetList)
            {
                ProcessCategory(dir);
            }
        }

        public void RunWithFile()
        {
            var fileList = Directory.EnumerateFiles(EntryPath).OrderBy(x => x).Skip(anchorIndex);
            var index = 0;
            foreach (var file in fileList)
            {

               ProcessFileCategory(file);
            }
        }

        private void ProcessFileCategory(string filePath)
        {
            var extention = Path.GetExtension(filePath);
            string outputDirPath = string.Empty;
            switch (extention)
            {
                case ".zip":
                    outputDirPath = ZipTool.Exact(filePath);
                    break;
                case ".rar":
                    outputDirPath = RarTool.Exact(filePath);
                    break;
            }
            if (!string.IsNullOrEmpty(outputDirPath))
            {
                ProcessCategory(outputDirPath);
               Directory.Delete(outputDirPath, true);
            }
        }

        private void ProcessCategory(string dirpath)
        {
            var name = dirpath.Split('\\').Last();
            contentUploadCreator.CreateCategory(name, ContentStatus.Continue, "顶通", "成人", "无水印", "新资源压缩");
            ProcessContent(dirpath);
        }

        private void ProcessContent(string dirPath)
        {
            var name = dirPath.Split('\\').Last();
            var contentFiles = Directory.GetFiles(dirPath).Skip(anchorIndex);
            anchorIndex = 0;//使用一次之后置空

            var contentIndex = 0;
            foreach (var file in contentFiles)
            {
                //file  zip or pdf
                ProcessFile(file, name, contentIndex);
                contentIndex++;
            }

            var directoryList = Directory.GetDirectories(dirPath);

            foreach (var dir in directoryList.Where(x => x.First() != '.' || x.First() != '_'))
            {
                CreateContentAndUploadImage(dir, name, contentIndex);
                contentIndex++;
            }
        }

        string ProcessFile(string filePath, string dirName, int contentOrder)
        {
            var extention = Path.GetExtension(filePath);
            var fileName = string.Empty;


            switch (extention)
            {
                case ".zip":
                    var outpuDirPahtZip = ZipTool.Exact(filePath);
                    CreateContentAndUploadImage(outpuDirPahtZip, dirName, contentOrder);
                    Directory.Delete(outpuDirPahtZip, true);
                    break;
                case ".rar":
                    var outpuDirPahtRar = ZipTool.Exact(filePath);
                    CreateContentAndUploadImage(outpuDirPahtRar, dirName, contentOrder);
                    Directory.Delete(outpuDirPahtRar, true);
                    break;
                case ".pdf":
                    var outpuStreamList = PDFTool.PDFToImage(filePath).Result;
                    CreateContentAndUploadImage(filePath, dirName, outpuStreamList, contentOrder);
                    break;
            }
            return fileName;
        }

        private void CreateContentAndUploadImage(string contentDir, string cateName, int contentOrder)
        {
            var name = contentDir.Split('\\').Last();
            var imgList = Directory.GetFiles(contentDir);
            //var cateName = contentDir.Split('\\').Last();

            contentUploadCreator.CreateContent(name, cateName, contentOrder);
            var index = 0;
            foreach (var imgItem in imgList)
            {
                var fileName = "cartoon/toptoon/" + cateName + "/" + name + "_" + index + ".jpg";
                QiniuTool.UploadImage(imgItem, fileName).Wait();
                contentUploadCreator.SetContentIImg(name, cateName, fileName, index);
                index++;
            }
        }

        private void CreateContentAndUploadImage(string filePath, string cateName, List<Stream> streamList, int contentOrder)
        {
            var name = Path.GetFileNameWithoutExtension(filePath);
            //var cateName = Path.GetDirectoryName(filePath).Split('\\').Last();
            contentUploadCreator.CreateContent(name, cateName, contentOrder);
            var index = 0;
            streamList.ForEach(async imgStream =>
            {
                var fileName = "cartoon/toptoon/" + cateName + "/" + name + "_" + index + ".jpg";
                QiniuTool.UploadImage(imgStream, fileName).Wait();
                contentUploadCreator.SetContentIImg(name, cateName, fileName, index);
                index++;
            });
        }


    }
}
