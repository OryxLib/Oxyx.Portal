using Newtonsoft.Json;
using Oryx.SpiderCartoon.V2.Model;
using Oryx.SpiderCore;
using Oryx.SpiderCore.SpiderQueryModel;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace Oryx.SpiderCartoon.V2
{
    class Program
    {



        static void Main(string[] args)
        {
            ThreadExcutor<string> mainSpiderExcutor = new ThreadExcutor<string>();
            //mainSpiderExcutor.threadInter = 3000;
            var kvpList = new List<KeyValuePair<Action<string>, string>>();

            for (int i = 1; i < 22; i++)
            {
                var kvp = new KeyValuePair<Action<string>, string>(Query, "http://www.weehui.com/cartoon/list/" + i);
                kvpList.Add(kvp);
            }
            mainSpiderExcutor.ExcuteWait(kvpList, 3);
        }

        static void Query(string url)
        {
            var spider = new Spider();
            spider.OnEveryGetResult += Spider_OnEveryGetResult;
            spider.HandlerNum = 32;
            spider.Run(new QueryParttern
            {
                CurrentUrl = url,
                NextUrlParttern = ".rightInfo a",
                NextParttern = new QueryParttern
                {
                    NextUrlParttern = ".chapterList .item a",
                    NextParttern = new QueryParttern
                    {
                        QueryTarget = new List<Parttern> {
                              new Parttern {
                                   PartternName ="名称",
                                    Query =new List<string> {
                                        ".centerTitle@text"
                                    }
                              },
                              new Parttern {
                                  PartternName = "图片",
                                  Query = new List<string> {
                                      ".contentNovel img@data-original"
                                  }
                              }
                         }
                    },
                    IsQueryTagetToChildren = true,
                    QueryTarget = new List<Parttern>
                     {
                        new Parttern{
                            PartternName ="名称",
                            Query=new List<string> {
                                ".container .title@text"
                            }
                        },
                        new Parttern{
                            PartternName = "封面图",
                            Query = new List<string> {
                                   ".coverContent .bg img@src"
                            }
                        },
                        new Parttern{
                            PartternName ="标签",
                            Query= new List<string>{
                                ".tags .label@text"
                            }
                        },
                         new Parttern {
                              PartternName="作品简介",
                              Query=new List<string>{
                                  ".fictionIntr .text@html"
                              }
                         }
                    }
                }
            });
        }

        private static void Spider_OnEveryGetResult(List<SpiderResultDicionary> result)
        {
            var category = new Categories()
            {
                Id = Guid.NewGuid(),
                CreateTime = DateTime.Now
            };
            var taskList = new List<Task>();
            category.ContentList = new List<ContentEntry> { };
            foreach (var item in result)
            {
                Console.WriteLine("父级");
                item.Parent?.QueryResult.ForEach(parentItem =>
                {
                    switch (parentItem.KeyName)
                    {
                        case "名称":
                            category.Name = parentItem.QueryValue;
                            break;
                        case "封面图":
                            var imgBytes = DownloadImg(parentItem.QueryValue);
                            var imgSaveKey = "cartton/" + parentItem.QueryValue.Split('/').Last();
                            Task.WaitAll(QiniuTool.UploadImage(imgBytes, imgSaveKey));
                            category.MediaResource = new List<FileEntry> {
                                 new FileEntry{
                                      ActualPath ="https://mioto.milbit.com/"+imgSaveKey,
                                      CreateTime = DateTime.Now,
                                      Name = imgSaveKey,
                                      Id = Guid.NewGuid()
                                 }
                            };
                            break;
                        case "标签":
                            category.Tags = new List<Tags>();
                            parentItem.QueryResult.ForEach(label =>
                            {
                                category.Tags.Add(new Tags
                                {
                                    Id = Guid.NewGuid(),
                                    Name = label
                                });
                            });
                            break;
                        case "作品简介":
                            category.Description = parentItem.QueryValue.Trim();
                            break;
                    }
                });
                Console.WriteLine("子级");
                Console.WriteLine(item.QueryName);

                foreach (var subitem in item.QueryResult)
                {
                    var contentEntry = new ContentEntry
                    {
                        CreateTime = DateTime.Now
                    };
                    switch (subitem.KeyName)
                    {
                        case "名称":
                            contentEntry.Title = subitem.QueryValue;
                            break;
                        case "图片":
                            subitem.QueryResult.ForEach(imgUrlItem =>
                            {
                                if (string.IsNullOrEmpty(imgUrlItem))
                                {
                                    return;
                                }
                                var imgBytes = DownloadImg(imgUrlItem);
                                var imgSaveKey = "cartton/" + imgUrlItem.Split('/').Last();
                                Task.WaitAll(QiniuTool.UploadImage(imgBytes, imgSaveKey));
                                contentEntry.MediaResource = new List<FileEntry> {
                                     new FileEntry{
                                          ActualPath ="https://mioto.milbit.com/"+imgSaveKey,
                                          CreateTime = DateTime.Now,
                                          Name = imgSaveKey,
                                          Id = Guid.NewGuid()
                                     }
                                };
                            });
                            break;
                        default:
                            break;
                    }
                    category.ContentList.Add(contentEntry);
                }
            }

            JsonSerializerSettings jsonSerializerSettings = new JsonSerializerSettings();
            jsonSerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
            var resultJson = JsonConvert.SerializeObject(category, jsonSerializerSettings);
            lock (fileLock)
            {
                StreamWriter fileWrite = File.AppendText(AppContext.BaseDirectory + "jsonResult.json");
                //Stream fileStream = File.OpenWrite(AppContext.BaseDirectory + "jsonResult.json");
                //StreamWriter fileWrite = new StreamWriter(fileStream);
                fileWrite.WriteLine(resultJson);
                fileWrite.Close();
            }
        }

        static object fileLock = new object();

        private static byte[] DownloadImg(string url)
        {
            if (!string.IsNullOrEmpty(url))
            {
                var wc = new WebClient();
                return wc.DownloadData(url);
            }
            else
            {
                return default(byte[]);
            }
        }
    }
}
