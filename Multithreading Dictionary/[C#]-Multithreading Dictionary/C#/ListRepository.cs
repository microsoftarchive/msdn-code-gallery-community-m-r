using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;

using System.Collections.Concurrent;
using System.Threading;

namespace Spike.Concurrency
{
    public class ListRepository
    {
        static Dictionary<string, DataCountModel> Dictionary { get; set; }
        static Dictionary<string, DataCountModel> LockedDictionary { get; set; }
        static ConcurrentDictionary<string, DataCountModel> ConcurrentDictionary { get; set; }
        
        static object _lock = new object();

        public static void InitialiseRepository(int capacity)
        {
            Dictionary = new Dictionary<string, DataCountModel>(capacity);
            LockedDictionary = new Dictionary<string, DataCountModel>(capacity);
            ConcurrentDictionary = new ConcurrentDictionary<string, DataCountModel>(4, capacity);            
        }

        [DebuggerStepThrough]
        [DebuggerHidden]
        public void UpdateModel(string key)
        {
            Thread.CurrentThread.Join(200);

            if(Dictionary.ContainsKey(key))
                Dictionary[key].Count++;
            else
                Dictionary.Add(key, new DataCountModel { Count = 1 });
        }

        [DebuggerStepThrough]
        public void UpdateLockedModel(string key)
        {
            Thread.CurrentThread.Join(200);

            lock (_lock)
            {
                if (LockedDictionary.ContainsKey(key))
                    LockedDictionary[key].Count++;
                else
                    LockedDictionary.Add(key, new DataCountModel { Count = 1 });                
            }
        }

       [DebuggerStepThrough] 
            public void UpdateConcurrentDictionary(string key)
            {
                Thread.CurrentThread.Join(200);

                ConcurrentDictionary.AddOrUpdate(key,
                                                 s => new DataCountModel {Count = 1},
                                                 (s, model) =>
                                                     {
                                                         model.Count++;
                                                         return model;
                                                     });
            }

        public List<DataCountModel> GetDictionaryModels()
        {
            return Dictionary.Values.ToList();
        }

        public List<DataCountModel> GetConcurrentDictionaryModels()
        {
            return ConcurrentDictionary.Values.ToList();
        }

        public List<DataCountModel> GetLockedDictionaryModels()
        {
            return LockedDictionary.Values.ToList();
        }

    }
}
