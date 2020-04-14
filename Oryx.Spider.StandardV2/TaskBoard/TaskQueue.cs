using Oryx.Spider.StandardV2.Interfaces;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Text;

namespace Oryx.Spider.StandardV2.TaskBoard
{
    public class TaskQueue<T> : ConcurrentQueue<TaskWrapper<T>>
        where T : SpiderTaskInterface
    {
    }

    public class TaskWrapper<T>
        where T : SpiderTaskInterface
    {
        public TaskWrapper(T instance)
        {
            Instance = instance;
        }

        public T Instance { get; set; }

        public TaskStatus Status { get; set; }
    }

    public enum TaskStatus
    {
        Running,
        Waiting
    }
}
