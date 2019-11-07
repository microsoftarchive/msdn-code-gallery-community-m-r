using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Threading;
using Spike.Concurrency;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Linq;

namespace Spike.Concurrency.Tests
{
    [TestClass()]
    public class ListRepositoryTest
    {
        [TestMethod()]
        public void UpdateModelTest()
        {
            ListRepository.InitialiseRepository(100);

            List<Task<TimeSpan>> dictionaryTasks = new List<Task<TimeSpan>>();
            List<Task<TimeSpan>> lockedTasks = new List<Task<TimeSpan>>();
            List<Task<TimeSpan>> concurrencyTasks = new List<Task<TimeSpan>>();

            for (int model = 0; model < 100; model++)
            {
                string key = string.Format("Model:{0}", model);

                for (int thread = 0; thread < 30; thread++)
                {
                    Task<TimeSpan> task = UpdateDictionaryModel(key);
                    task.Start();
                    dictionaryTasks.Add(task);

                    task = UpdateLockedDictionaryModel(key);
                    task.Start();
                    lockedTasks.Add(task);

                    task = UpdateConcurrentDictionaryModel(key);
                    task.Start();
                    concurrencyTasks.Add(task);                    
                }
            }

            // waitall will throw an aggregate exception for each failed task
            // Task<TimeSpan>.WaitAll(tasks.ToArray());

            while (dictionaryTasks.Any(t => t.Status == TaskStatus.Running)
                   || lockedTasks.Any(t => t.Status == TaskStatus.Running)
                   || concurrencyTasks.Any(t => t.Status == TaskStatus.Running))
            {
                Thread.CurrentThread.Join(200);
            }
                        
            ListRepository repository = new ListRepository();

            var dictionaryModels = repository.GetDictionaryModels();
            var lockedModels = repository.GetLockedDictionaryModels();
            var concurrentModels = repository.GetConcurrentDictionaryModels();

            Debug.WriteLine("100 keys were created.");
            Debug.WriteLine(string.Format("\tDictionary Model Count:\t\t\t\t{0}", dictionaryModels.Count));
            Debug.WriteLine(string.Format("\tLocked Dictionary Model Count:\t\t{0}", lockedModels.Count));
            Debug.WriteLine(string.Format("\tConcurrent Dictionary Model Count:\t{0}", concurrentModels.Count));

            Debug.WriteLine("Each key was updated 30 times.");
            Debug.WriteLine(string.Format("\tAre all Dictionary Models set to 30:\t\t\t\t{0}", dictionaryModels.All(m => m.Count == 30)));
            Debug.WriteLine(string.Format("\tAre all Locked Dictionary Models set to 30:\t\t\t{0}", lockedModels.All(m => m.Count == 30)));
            Debug.WriteLine(string.Format("\tAre all Concurrent Dictionary Models set to 30:\t\t{0}", concurrentModels.All(m => m.Count == 30)));

            Debug.WriteLine("Roughly speaking, what is the average elapsed time for an update to each model.");
            Debug.WriteLine(string.Format("\tAverage update for Dictionary Models:\t\t\t\t{0}", dictionaryTasks.Where(t => t.IsCompleted && !t.IsFaulted).Average(t => t.Result.TotalMilliseconds)));
            Debug.WriteLine(string.Format("\tAverage update for Locked Dictionary Models:\t\t{0}", lockedTasks.Average(t => t.Result.TotalMilliseconds)));
            Debug.WriteLine(string.Format("\tAverage update for Concurrent Dictionary Models:\t{0}", concurrencyTasks.Average(t => t.Result.TotalMilliseconds)));

            Debug.WriteLine("How many tasks failed?");
            Debug.WriteLine(string.Format("\tNumber of Dictionary Model Task Failures:\t\t\t\t{0}", dictionaryTasks.Count(t => t.IsFaulted)));
            Debug.WriteLine(string.Format("\tNumber of Locked Dictionary Model Task Failures:\t\t{0}", lockedTasks.Count(t => t.IsFaulted)));
            Debug.WriteLine(string.Format("\tNumber of Concurrent Dictionary Model Task Failures:\t{0}", concurrencyTasks.Count(t => t.IsFaulted)));

            Assert.Inconclusive();
        }

        [DebuggerStepThrough]
        public Task<TimeSpan> UpdateDictionaryModel(string key)
        {
            return new Task<TimeSpan>(() =>
            {
                ListRepository repository = new ListRepository();

                Random r = new Random();
                Thread.CurrentThread.Join(r.Next(200));

                var startTime = DateTime.Now;

                repository.UpdateModel(key);

                return DateTime.Now.Subtract(startTime);
            });
        }

        [DebuggerStepThrough]
        public Task<TimeSpan> UpdateLockedDictionaryModel(string key)
        {
            return new Task<TimeSpan>(() =>
            {
                ListRepository repository = new ListRepository();

                Random r = new Random();
                System.Threading.Thread.CurrentThread.Join(r.Next(200));

                var startTime = DateTime.Now;

                repository.UpdateLockedModel(key);

                return DateTime.Now.Subtract(startTime);
            });
        }

        [DebuggerStepThrough]
        public Task<TimeSpan> UpdateConcurrentDictionaryModel(string key)
        {
            return new Task<TimeSpan>(() =>
            {                
                ListRepository repository = new ListRepository();

                Random r = new Random();
                System.Threading.Thread.CurrentThread.Join(r.Next(200));

                var startTime = DateTime.Now;

                repository.UpdateConcurrentDictionary(key);

                return DateTime.Now.Subtract(startTime);
            });
        }
    }
}
