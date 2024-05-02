using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Licenta.Commons.Parallelization
{
    public class TaskManager
    {
        struct TaskItem
        {
            public Task Task;
            public Action<Task> Finished;

            public TaskItem(Task task, Action<Task> finished)
            {
                Task = task;
                Finished = finished;
            }
        }

        private readonly int MaxLength;
        private readonly ConcurrentQueue<TaskItem> PendingTasks = new ConcurrentQueue<TaskItem>();        

        public TaskManager(int maxLength = 10)
        {
            MaxLength = maxLength;
        }

        private bool _Running = false;

        private object LockObject = new object();

        public void Run()
        {
            HashSet<TaskItem> RunningTasks = new HashSet<TaskItem>();
            int maxLen = MaxLength;

            _Running = true;

            while (IsRunning()) 
            {
                Thread.Sleep(100);

                while (RunningTasks.Count < maxLen && PendingTasks.TryDequeue(out var item)) 
                {                    
                    RunningTasks.Add(item);
                    item.Task.Start();
                }                
                
                foreach (var item in RunningTasks.ToArray()) 
                    if (item.Task.IsCompleted) 
                    {                        
                        RunningTasks.Remove(item);
                        item.Finished?.Invoke(item.Task);
                    }
            }
        }

        public bool IsRunning()
        {
            lock(LockObject)
            {
                return _Running;
            }
        }

        public void Stop()
        {
            lock(LockObject)
            {
                _Running = false;
            }
        }

        public TaskManager RunAsync()
        {
            Task.Run(Run);
            return this;
        }

        public Task AddTask(Task task, Action<Task> addedCallback, Action<Task> finishedCallback)
        {
            addedCallback?.Invoke(task);
            PendingTasks.Enqueue(new TaskItem(task, finishedCallback));
            return task;
        }

        public TaskGroup CreateTaskGroup() => new TaskGroup(this);
    }
}
