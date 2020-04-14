using Microsoft.EntityFrameworkCore;
using OpenQA.Selenium;
using Oryx.Spider.StandardV3.Infrastructures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Oryx.Spider.StandardV3
{
    public class TaskExutor
    {
        private TaskExutor()
        {
            spiderDbContext = new SpiderDbContext();
            SpiderTaskDelegate = new Dictionary<string, Func<SpiderLog, IWebDriver, Task>>();
        }

        public Dictionary<string, Func<SpiderLog, IWebDriver, Task>> SpiderTaskDelegate { get; set; }

        private SpiderDbContext spiderDbContext;
        private static TaskExutor _Instance;
        private static bool TaskIsRunning;
        private int currentId = 0;
        public int speedRun = 0;

        public static TaskExutor Instance
        {
            get
            {
                if (_Instance == null)
                {
                    _Instance = new TaskExutor();
                }
                return _Instance;
            }
        }

        public void ClearUpdata(string dataKey)
        {
            var targetData = spiderDbContext.SpiderLogs.Where(x => x.Type == dataKey);
            spiderDbContext.RemoveRange(targetData);
            spiderDbContext.SaveChanges();
        }

        private async Task Run(SpiderLog spiderLog = null)
        {
            //SpiderLog spiderLog;
            if (spiderLog == null)
            {
                if (currentId < 1)
                {
                    spiderLog = await NextTask();
                }
                else
                {
                    spiderLog = await NextTask(currentId);
                }
            }


            while (spiderLog != null)
            {
                var runner = new PhantomJsRunner(null, "", "");
                var webDriver = runner.WebDriver;
                var hasErr = false;
                try
                {
                    var needProcess = SpiderTaskDelegate.ContainsKey(spiderLog.Type);
                    if (needProcess)
                    {
                        webDriver.Navigate().GoToUrl(spiderLog.TargetUrl);
                        await SpiderTaskDelegate[spiderLog.Type](spiderLog, webDriver);
                    }
                }
                catch (Exception exc)
                {
                    Console.WriteLine(exc.Message);
                    hasErr = true;
                }
                currentId = spiderLog.Id;
                webDriver.Quit();
                Thread.Sleep(speedRun);
                spiderLog.ReloadSuccess = !hasErr;
                await UpdateTask(spiderLog);
                spiderLog = await NextTask(currentId);
            }

            TaskIsRunning = false;
        }
        public async Task SetTask(string url, string type)
        {
            var exited = await spiderDbContext.SpiderLogs.AnyAsync(x => x.TargetUrl == url);
            if (!exited)
            {
                await spiderDbContext.SpiderLogs.AddAsync(new SpiderLog
                {
                    TargetUrl = url,
                    Type = type
                });
                await spiderDbContext.SaveChangesAsync();
            }

            if (!TaskIsRunning)
            {
                TaskIsRunning = true;
                Run();
            }
        }

        public async Task SetTaskWithParentUrl(string url, string parentUrl, string categoryName, int index, string type)
        {
            var exited = await spiderDbContext.SpiderLogs.AnyAsync(x => x.TargetUrl == url);
            if (!exited)
            {
                await spiderDbContext.SpiderLogs.AddAsync(new SpiderLog
                {
                    TargetUrl = url,
                    ParentUrl = parentUrl,
                    ParentName = categoryName,
                    Order = index,
                    Type = type
                });
                await spiderDbContext.SaveChangesAsync();
            }

            if (!TaskIsRunning)
            {
                TaskIsRunning = true;
                Run();
            }
        }

        public async Task SetTaskRange(List<string> urlList, string type)
        {
            var spiderLogRange = new List<SpiderLog>();
            urlList.ForEach(item =>
            {
                spiderLogRange.Add(new SpiderLog
                {
                    TargetUrl = item,
                    Type = type
                });
            });
            await spiderDbContext.SpiderLogs.AddRangeAsync(spiderLogRange);
            await spiderDbContext.SaveChangesAsync();
        }

        public async Task<SpiderLog> NextTask()
        {
            return await spiderDbContext.SpiderLogs.FirstOrDefaultAsync(x => !x.ReloadSuccess);
        }

        public async Task<SpiderLog> NextTask(int currentId)
        {
            return await spiderDbContext.SpiderLogs.FirstOrDefaultAsync(x => x.Id > currentId && !x.ReloadSuccess);
        }

        public async Task UpdateTask(SpiderLog spiderLog)
        {
            spiderDbContext.Update(spiderLog);
            await spiderDbContext.SaveChangesAsync();
        }

        public async Task Reload(string taskType)
        {
            var hasTaskdeletgate = SpiderTaskDelegate.ContainsKey(taskType);
            if (hasTaskdeletgate)
            {
                if (!TaskIsRunning)
                {
                    TaskIsRunning = true;
                    var spiderLog = await spiderDbContext.SpiderLogs.FirstOrDefaultAsync(x => x.Type == taskType);
                    Run(spiderLog);
                }
            }
        }
    }
}
