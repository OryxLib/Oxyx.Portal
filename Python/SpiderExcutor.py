import threading
import time
from queue import Queue
#import asyncio


class TaskExcutor():
    TaskQueue = Queue()
    IntervalTime = 3  # second
    IsRunning = False

    def TaskEnqueueDelegate(self, func, param):
        # print(param)
        self.TaskQueue.put((func, param))

    def TaskEnqueue(self, func, param):
        # print('===')
        # print(param)
        ted = threading.Thread(
            target=self.TaskEnqueueDelegate, args=(func, param))
        ted.start()
        if not self.IsRunning:
            threadRun = threading.Thread(target=self.TaskRunning)
            threadRun.start()
    # def TaskEnqueue (self,func, param):
    #     self.TaskQueue.append((func, param))
    #     self.TaskRunning()

    def TaskRunning(self):
        if not self.IsRunning:
            while not self.TaskQueue.empty():
                try: 
                    self. IsRunning = True
                    taskFun, taskParam = self.TaskQueue.get()
                    taskFun(taskParam)
                    time.sleep(self.IntervalTime)
                except TypeError:
                    pass
