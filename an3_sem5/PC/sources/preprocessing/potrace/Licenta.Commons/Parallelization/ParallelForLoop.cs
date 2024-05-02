using System;
using System.Collections.Concurrent;
using System.Security.Policy;
using System.Threading.Tasks;

namespace Licenta.Commons.Parallelization
{
    public class ParallelForLoop
    {
        public static double Sum(int start, int end, Func<int, double> element)
        {
            double result = 0;

            RunPartitioned(start, end, (s, e, l) =>
            {
                double partialSum = 0;
                for (int i = s; i < e; i++) partialSum += element(i);
                lock (l) result += partialSum;
            });

            return result;
        }

        public static void RunPartitioned(int start, int end, Action<int, int, object> partitionLogic)
        {
            var locker = new object();
            Parallel.ForEach(Partitioner.Create(start, end), range =>
            {
                partitionLogic(range.Item1, range.Item2, locker);
            });
        }

        public static void RunPartitioned(int start, int end, Action<int> iterationLogic)
        {
            Parallel.ForEach(Partitioner.Create(start, end), range =>
            {
                for (int i = range.Item1; i < range.Item2; i++) iterationLogic(i);                
            });
        }


        public static void Run(Action<int> iterationLogic, int start, int end, int step = 1, int tasksCount = 3, int maxTasksLength = -1)
        {
            var tm = new TaskManager(maxTasksLength < 0 ? tasksCount : maxTasksLength).RunAsync();
            var tg = tm.CreateTaskGroup();

            Action oneTask(int i0) => new Action(() =>
            {
                for (int i = i0; i < end; i += step * tasksCount)
                    iterationLogic(i);
            });

            for (int t = 0; t < tasksCount; t++)
            {
                tg.AddTask(oneTask(start + t * step));
            }

            tg.WaitAll();
            tm.Stop();
        }
    }
}
