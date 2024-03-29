﻿using Oryx.SpiderCore.SpiderQueryModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Oryx.SpiderCore
{
    public class ThreadExcutor<T>
    {
        static bool isRunning = false;

        Queue<KeyValuePair<Action<T>, T>> actionQueue = new Queue<KeyValuePair<Action<T>, T>>();

        static int threadStartNum = 0;

        static int threadInter = 1000;

        /// <summary>
        /// 静态方法
        /// </summary>
        public static void Excute()
        {
            while (ThreadManager.ThreadActionQueue.Count > 0)
            {
                Thread thread = new Thread(() => ThreadManager.ThreadActionQueue.Dequeue());
                thread.Start();
            }
        }

        /// <summary>
        /// 根据配置线程数启动
        /// 实例化启动线程执行器, 每个实例单独配备线程池
        /// </summary> 
        /// <param name="actionList"></param>
        /// <param name="handlerNum"></param>
        /// <returns></returns>
        public void ExcuteWait(List<KeyValuePair<Action<T>, T>> actionList, int handlerNum = 5)
        {
            ManualResetEvent[] manualHandleList;

            actionList.ForEach(item =>
            {
                actionQueue.Enqueue(item);
            });

            if (!isRunning)
            {
                isRunning = true;
                while (actionQueue.Any())
                {
                    manualHandleList = new ManualResetEvent[actionQueue.Count() < handlerNum ? actionQueue.Count() : handlerNum];
                    var actCnt = actionQueue.Count();
                    while (actionQueue.Any() && threadStartNum < handlerNum && threadStartNum < actCnt)
                    {
                        manualHandleList[threadStartNum] = new ManualResetEvent(false);
                        var thread = new Thread(resetIndex =>
                        {
                            Console.WriteLine("start index:" + resetIndex);
                            var _resetIndex = (int)resetIndex;
                            var action = actionQueue.Dequeue();
                            action.Key(action.Value);
                            manualHandleList[_resetIndex].Set();
                        });
                        thread.Start(threadStartNum++);
                        Thread.Sleep(threadInter);
                    }
                    WaitHandle.WaitAll(manualHandleList);
                    threadStartNum = 0;
                }
                isRunning = false;
            }
        }

        public async Task ExcuteAsync(List<KeyValuePair<Action<T>, T>> actionList)
        {
            actionList.ForEach(item =>
            {
                actionQueue.Enqueue(item);
            });
            while (actionQueue.Any())
            {
                var actCnt = actionQueue.Count();
                while (actionQueue.Any() && threadStartNum < actCnt)
                {
                    var action = actionQueue.Dequeue();
                    await Task.Run(() => action.Key(action.Value));
                }
            }
        }
    }
}
