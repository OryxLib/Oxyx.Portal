using Newtonsoft.Json;
using OpenQA.Selenium;
using OpenQA.Selenium.PhantomJS;
using Oryx.SpiderCore.Interfaces;
using Oryx.SpiderCore.SpiderQueryModel;
using Oryx.SpiderCore.Ultilities;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Oryx.SpiderCore
{
    public class Spider
    {
        private string configDir = AppDomain.CurrentDomain.BaseDirectory + "AppConfig";

        public delegate void EveryGetResultHandler(List<SpiderResultDicionary> result);

        public event EveryGetResultHandler OnEveryGetResult;

        public delegate void AllGetResultHandler();

        public event AllGetResultHandler OnAllGetResult;

        ThreadExcutor<QueryParttern> excutor = new ThreadExcutor<QueryParttern>();

        static Queue<PhantomJSDriver> driverQueue = new Queue<PhantomJSDriver>();

        public int HandlerNumber { get; set; } = 10;
        public int HandlerNum { get; set; }

        ~Spider()
        {
            while (driverQueue.Count > 0 && driverQueue.Peek() != null)
            {
                driverQueue.Dequeue().Quit();
            }
        }

        public void Run(QueryParttern queryParttern)
        {
            throw new NotImplementedException();
        }

        public Spider()
        {
            //queryConfig = LoadConfig();
        }

        public void Query(QueryParttern parttern, SpiderResultDicionary prevParent = null)
        {

            var currentResultList = new List<SpiderResultDicionary>();

            try
            {
                var runner = new PhantomRunner();
                var driver = (PhantomJSDriver)runner.Create(parttern.CurrentUrl);
                if (driver == null)
                {
                    Console.WriteLine("driver null");
                    Console.WriteLine(parttern.CurrentUrl);
                    return;
                }
                driverQueue.Enqueue(driver);
                //process current 
                //var resultDic = new List<SpiderResultDicionary>();
                if (!string.IsNullOrEmpty(parttern.StateNum))
                {
                    currentResultList.Add(new SpiderResultDicionary
                    {
                        QueryName = "statet",
                        QueryResult = new List<SpiderQueryResult> {
                               new SpiderQueryResult
                               {
                                    KeyName = "state",
                                    QueryResult =new List<string> {
                                        parttern.StateNum
                                    }
                               }
                          }
                    });
                }
                if (parttern.QueryTarget != null)
                {
                    var resultList = new List<SpiderQueryResult>();
                    foreach (var queryTarge in parttern.QueryTarget)
                    {
                        var result = new SpiderQueryResult();
                        foreach (var queryItem in queryTarge.Query)
                        {
                            result.KeyName = queryTarge.PartternName;
                            result.QueryResult = driver.GetSpiderResults(queryItem);
                            Thread.Sleep(30);
                        }
                        resultList.Add(result);
                    }
                    currentResultList.Add(new SpiderResultDicionary
                    {
                        Parent = prevParent,
                        QueryName = driver.Title,
                        QueryResult = resultList
                    });

                    //将结果以事件的形式抛出
                    if (OnEveryGetResult != null)
                    {
                        //只有当false时才可调用(防止重复结果调用)
                        if (!parttern.IsQueryTagetToChildren)
                        {
                            OnEveryGetResult(currentResultList);
                        }
                    }
                }

                SpiderResultDicionary transformResultDict = null;

                if (currentResultList != null && currentResultList.Count > 0)
                {
                    transformResultDict = currentResultList[0];
                }

                //处理加载更多/ 模拟点击分页, 模拟下滑 , 模拟page路由
                if (parttern.LoadMore != null)
                {
                    switch (parttern.LoadMore.Operation)
                    {
                        case "click":
                            var clickElement = driver.FindElement(By.CssSelector(parttern.LoadMore.LoadMoreParttern));
                            if (clickElement != null)
                            {
                                for (int i = 0; i < parttern.LoadMore.ExcuteTime; i++)
                                {
                                    clickElement.Click();
                                    Thread.Sleep(parttern.LoadMore.WatingTimeSeconds * 1000);
                                }
                            }
                            break;
                        case "script":
                            for (int i = 0; i < parttern.LoadMore.ExcuteTime; i++)
                            {
                                driver.ExecuteScript(parttern.LoadMore.LoadMoreParttern);
                                Thread.Sleep(parttern.LoadMore.WatingTimeSeconds * 1000);
                            }
                            break;
                        case "url":
                            var nextPage = driver.FindElement(By.CssSelector(".ui-page-inner a:last-child"));
                            if (nextPage.Text.Contains("下一页"))
                            {
                                var currentParttern = parttern.Clone() as QueryParttern;
                                currentParttern.CurrentUrl = nextPage.GetAttribute("href");
                                var kvActionList = new List<KeyValuePair<Action<QueryParttern>, QueryParttern>>();
                                var kvActionItem = new KeyValuePair<Action<QueryParttern>, QueryParttern>(_nextParttern =>
                                {
                                    Query(_nextParttern, transformResultDict);
                                }, currentParttern);
                                kvActionList.Add(kvActionItem);
                                excutor.ExcuteWait(kvActionList, HandlerNumber);
                            }
                            break;
                    }
                }

                //处理下一页
                if (parttern.PageParameter != null)
                {
                    var pageNaviElements = driver.FindElements(By.CssSelector(".paginate a"));
                    foreach (var eleItem in pageNaviElements)
                    {
                        var targetHref = eleItem.GetAttribute("href");
                        if (targetHref == "#")
                        {
                            continue;
                        }

                        var targetClass = eleItem.GetAttribute("class");

                        if (targetClass.Contains("pg_prev"))
                        {
                            continue;
                        }

                        if (targetClass.Contains("page"))
                        {
                            Query(new QueryParttern
                            {
                                CurrentUrl = targetHref,
                                PageParameter = "page",
                                NextUrlParttern = parttern.NextUrlParttern,
                                NextParttern = parttern.NextParttern
                            }, transformResultDict);
                        }
                        else
                        {
                            Query(new QueryParttern
                            {
                                CurrentUrl = targetHref,
                                NextUrlParttern = parttern.NextUrlParttern,
                                NextParttern = parttern.NextParttern
                            }, transformResultDict);
                        }
                    }
                }

                //process nextUrl
                if (parttern.NextUrlParttern != null)
                {
                    var targetNextElements = driver.FindElements(By.CssSelector(parttern.NextUrlParttern));
                    //#warning test , please remove take function()
                    var targetNextUrlArr = targetNextElements.Select(x => x.GetAttribute("href"));
                    var actionList = new List<Action<QueryParttern>>();
                    List<KeyValuePair<Action<QueryParttern>, QueryParttern>> kvActionList = new List<KeyValuePair<Action<QueryParttern>, QueryParttern>>();
                    foreach (var urlItem in targetNextUrlArr)
                    {
                        var nextParttern = parttern.NextParttern.Clone() as QueryParttern;
                        nextParttern.CurrentUrl = urlItem;
                        var kvActionItem = new KeyValuePair<Action<QueryParttern>, QueryParttern>(_nextParttern =>
                        {
                            Query(_nextParttern, transformResultDict);
                        }, nextParttern);
                        kvActionList.Add(kvActionItem);
                    }
                    //ThreadExcutor<QueryParttern>.ExcuteAsync(kvActionList).ConfigureAwait(false);

                    excutor.ExcuteWait(kvActionList, HandlerNumber);
                }

                driver.Quit();
            }
            catch (Exception exc)
            {
                Console.WriteLine(exc.Message);
            }
        }

        List<QueryParttern> LoadConfig()
        {
            var fileList = Directory.GetFiles(configDir);
            List<QueryParttern> queryPartternList = new List<QueryParttern>();
            foreach (var filePath in fileList)
            {
                var fileStr = File.ReadAllText(filePath);
                queryPartternList.Add(JsonConvert.DeserializeObject<QueryParttern>(fileStr));
            }
            return queryPartternList;
        }
    }
}
