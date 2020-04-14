using OpenQA.Selenium;
using Oryx.Spider.StandardV2.WebDriverExtensions;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Oryx.Spider.StandardV2.TaskBoard
{
    public class TaskExutor
    {
        private TaskQueue<TaskItem> TaskQueue { get; set; }
        public delegate void TaskExcuteResultHanndler(TaskItem taskitem);
        public event TaskExcuteResultHanndler OnTaskResult;
        private TaskExutor()
        {
            TaskQueue = new TaskQueue<TaskItem>();
        }
        private static TaskExutor _instance;

        public static TaskExutor Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new TaskExutor();
                }

                return _instance;
            }
        }

        public void SetTask(TaskSpider taskSpider, Action<IWebElement, Dictionary<string, string>> GetResult)
        {
            queueNumber++;
            taskSpider.TaskItem.Instance.GetResult = GetResult;
            TaskQueue.Enqueue(taskSpider.TaskItem);
            TaskRun();
        }

        public void SetTask(TaskSpider taskSpider, Func<IWebElement, Dictionary<string, string>, WebDriverWrapper, Task> GetResult)
        {
            queueNumber++;
            taskSpider.TaskItem.Instance.GetResultWithTask = GetResult;

            TaskQueue.Enqueue(taskSpider.TaskItem);
            TaskRun();
        }

        public void SetTask(TaskSpider task)
        {
            queueNumber++;
            TaskQueue.Enqueue(task.TaskItem);
            TaskRun();
        }
        static bool isRunning = false;
        private static int psSum = 0;
        private static int maxPsNum = 5;
        static int queueNumber = 0;
        static int errorNumber = 0;
        static int processNumber = 0;
        int intervalTime = 1 * 1000;
        private void TaskRun()
        {
            if (!isRunning)
            {
                isRunning = true;
                while (TaskQueue.Any())
                {
                    if (psSum >= maxPsNum)
                    {
                        continue;
                    }
                    psSum++;
                    var taskResult = Task.Run(async () =>
                     {
                         TaskWrapper<TaskItem> taskItem = null;
                         try
                         {
                             if (TaskQueue.TryDequeue(out taskItem))
                             {
                                 await taskItem.Instance.TaskExcute();
                                 //if (OnTaskResult != null)
                                 //{
                                 //    OnTaskResult(taskItem.Instance);
                                 //}
                             }
                         }
                         catch (Exception exc)
                         {
                             var body = taskItem.Instance.WebElement.GetAttribute("innerHtml");
                             Console.WriteLine("Exception :=========================================================================");
                             Console.WriteLine(body);
                             Console.WriteLine("=========================================================================");

                             if (taskItem.Instance.RetryTime < 3)
                             {
                                 taskItem.Instance.RetryTime++;
                                 TaskQueue.Enqueue(taskItem);
                             }
                             File.AppendAllText(AppDomain.CurrentDomain.BaseDirectory + "errLog.txt", taskItem.Instance.Url + "|" + string.Join("_", taskItem.Instance.State.Select(x => x.Key + "_" + x.Value).ToList()));

                             errorNumber++;
                             //if (taskItem != null)
                             //{
                             //    TaskQueue.Enqueue(taskItem);
                             //}
                             //UpdateProxy();
                             //Console.WriteLine("===========================");
                             //Console.WriteLine();
                             //Console.WriteLine("Bug:" + exc.Message);
                             //Console.WriteLine();
                             //Console.WriteLine("===========================");
                         }
                     });
                    taskResult.ContinueWith(_task =>
                    {
                        if (_task.IsCompleted)
                            psSum--;
                    });
                    Thread.Sleep(intervalTime);
                    Console.CursorTop = Console.WindowTop;
                    Console.CursorLeft = 0;
                    processNumber++;
                    Console.WriteLine($"{processNumber}/{queueNumber} , Err: {errorNumber}");
                }
                isRunning = false;
            }
        }

        public void UpdateProxy()
        {
            var wc = new System.Net.WebClient();
            var proxyList = wc.DownloadString("http://dps.kdlapi.com/api/getdps/?orderid=953622430174277&num=3&pt=1&ut=2&dedup=1&sep=1");
            File.WriteAllText(AppDomain.CurrentDomain.BaseDirectory + "proxyList.txt", proxyList);
        }
    }
}
