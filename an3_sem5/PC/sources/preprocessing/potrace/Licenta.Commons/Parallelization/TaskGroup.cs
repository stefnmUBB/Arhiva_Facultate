using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Licenta.Commons.Parallelization
{
    public class TaskGroup
    {
        private readonly TaskManager TaskManager;
        private HashSet<Task> RunningTasks = new HashSet<Task>();

        public TaskGroup(TaskManager taskManager)
        {
            TaskManager = taskManager;
        }

        private void OnTaskAdded(Task t)
        {
            //Console.WriteLine("Task added");
            RunningTasks.Add(t);
        }
        private void OnTaskFinished(Task t)
        {
            //Console.WriteLine("Task finished");
            RunningTasks.Remove(t);
        }

        public Task AddTask(Action action) => TaskManager.AddTask(new Task(action), OnTaskAdded, OnTaskFinished);
        public Task<T> AddTask<T>(Func<T> func) => TaskManager.AddTask(new Task<T>(func), OnTaskAdded, OnTaskFinished) as Task<T>;
        public void WaitAll()
        {
            while (RunningTasks.Count > 0) ;
        }
    }
}
