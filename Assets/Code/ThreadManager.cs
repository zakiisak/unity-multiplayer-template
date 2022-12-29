using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Code
{
    public class ThreadManager : MonoBehaviour
    {
        private static ConcurrentQueue<Action> Actions = new ConcurrentQueue<Action>();
        public static ThreadManager Instance;

        public static void RunOnMainThread(Action action)
        {
            lock (Actions)
            {
                Actions.Enqueue(action);
            }
        }

        public static void RunAsync(WaitCallback callback)
        {
            ThreadPool.QueueUserWorkItem(callback);
        }

        public void Awake()
        {
            Instance = this;
        }

        public void Update()
        {
            lock (Actions)
            {
                int count = Actions.Count;
                for (int i = 0; i < count; i++)
                {
                    Action action = null;
                    if (Actions.TryDequeue(out action))
                    {
                        action();
                    }
                }
            }
        }
    }
}