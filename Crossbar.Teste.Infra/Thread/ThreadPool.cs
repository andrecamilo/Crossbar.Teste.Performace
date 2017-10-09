using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Threading;

namespace Crossbar.Teste.Infra
{
    public partial class ThreadPool
    {
        internal delegate object WorkItemCallback(object state);

        public delegate void CallbackFunction();

        private bool isDisposeDoneWorkItemsAutomatically;
        private readonly Queue<ThreadRunner> threads;
        private readonly ConcurrentQueue<ThreadRunner> threadsIdle;
        private int threadsWorking;
        private readonly ConcurrentQueue<WorkItem> workItemQueue = new ConcurrentQueue<WorkItem>();
        private readonly ConcurrentQueue<WorkItem> returnedWorkItems = new ConcurrentQueue<WorkItem>();
        private bool shutDownSignaled;
        private readonly object lockObjectShutDownSignaled = new object();

        public int NumberOfThreads { get; }

        public bool IsDisposeDoneWorkItemsAutomatically
        {
            get
            {
                Thread.MemoryBarrier();
                return isDisposeDoneWorkItemsAutomatically;
            }
            set
            {
                isDisposeDoneWorkItemsAutomatically = value;
                Thread.MemoryBarrier();
            }
        }

        public ThreadPool(int numberOfThreads, string threadsNamePrefix)
        {
            NumberOfThreads = numberOfThreads;
            threads = new Queue<ThreadRunner>();
            threadsIdle = new ConcurrentQueue<ThreadRunner>();

            // allocate threads...
            for (var i = 0; i < NumberOfThreads; i++)
            {
                var threadRunner = new ThreadRunner(this);
                threadRunner.Thread = new Thread(threadRunner.DoWork);
                threadRunner.Thread.Name = threadsNamePrefix + (i + 1);
                threadRunner.Thread.IsBackground = true;

                threads.Enqueue(threadRunner);
                threadsIdle.Enqueue(threadRunner);

                threadRunner.Thread.Start();
            }
        }

        public void ClearWorkItemQueue()
        {
            WorkItem wi;
            while (workItemQueue.TryDequeue(out wi))
            {}
        }

        public int NumberOfItemsLeft()
        {
            Thread.MemoryBarrier();
            return workItemQueue.Count;
        }

        public int NumberOfItemsDone()
        {
            Thread.MemoryBarrier();
            return returnedWorkItems.Count;
        }

        internal void EnqueueWorkItemInternal(WorkItem workItem)
        {
            // look for an idle worker...
            ThreadRunner singleThreadRunner;
            if (threadsIdle.TryDequeue(out singleThreadRunner))
            {
                // hand over the work item...
                workItem.SingleThreadRunner = singleThreadRunner;
                Interlocked.Increment(ref threadsWorking);
                singleThreadRunner.SignalWork(workItem);
            }
            else
            {
                // just enqueue the item since all workers are busy...
                workItemQueue.Enqueue(workItem);
            }
        }

        internal WorkItem DequeueWorkItemInternal(ThreadRunner singleThreadRunner, bool isGetNewOne,
            WorkItem returnedWorkItem = null)
        {
            if (returnedWorkItem != null)
            {
                returnedWorkItems.Enqueue(returnedWorkItem);
            }

            if (!shutDownSignaled && isGetNewOne)
            {
                WorkItem workItem;
                if (workItemQueue.TryDequeue(out workItem))
                {
                    workItem.SingleThreadRunner = singleThreadRunner;
                    return workItem;
                }
            }

            // If we are here, there is no more work to do left...
            // The worker has to be set to idle...
            Interlocked.Decrement(ref threadsWorking);
            threadsIdle.Enqueue(singleThreadRunner);
            return null;
        }

        private WorkItem GetWorkItem(CallbackFunction asyncCallback)
        {
            WorkItem workItem;
            if (!returnedWorkItems.TryDequeue(out workItem))
            {
                workItem = new WorkItem();
                workItem.WorkItemStateTypeless = new WorkItemStateTypeless(workItem);
            }

            workItem.SingleThreadRunner = null;
            workItem.IsCompleted = false;
            workItem.Result = null;
            workItem.AsyncCallback = asyncCallback;
            return workItem;
        }

        public void ReturnWorkItem(WorkItem returnedWorkItem)
        {
            returnedWorkItems.Enqueue(returnedWorkItem);
        }

        public void WaitForEveryWorkerIdle()
        {
            // A spinWait ensures a yield from time to time, forcing the CPU to do a context switch, thus allowing other processes to finish.
            var spinWait = new SpinWait();
            while (threadsWorking > 0)
            {
                Thread.MemoryBarrier();
                spinWait.SpinOnce();
            }
        }

        public void ClearWorkItemCache()
        {
            WorkItem w;
            while(returnedWorkItems.TryDequeue(out w))
            {}
        }

        public void ShutDown()
        {
            // First, we want to close. So stop dealing new work items...
            lock (lockObjectShutDownSignaled)
            {
                shutDownSignaled = true;
            }

            // signal the shutdown-command to all workers...
            if (threads.Count > 0)
            {
                foreach (var thread in threads)
                {
                    thread.SignalShutDown();
                }
            }
        }

        public void Sleep()
        {
            // signal the pause-command to all workers...
            if (threads.Count > 0)
            {
                foreach (var thread in threads)
                {
                    thread.SignalPause();
                }
            }
        }

        public void Wakeup()
        {
            // signal the resume-command to all workers...
            if (threads.Count > 0)
            {
                foreach (var thread in threads)
                {
                    thread.SignalResume();
                }
            }
        }

        #region action
        public IWorkItemState EnqueueWorkItem(Action workerFunction, CallbackFunction asyncCallback = null)
        {
            var workItem = GetWorkItem(asyncCallback);
            workItem.DelegateInputParameters = new object[] { };
            workItem.Delegate = delegateInputParameters =>
            {
                workerFunction.Invoke();
                return null;
            };

            var workItemState = new WorkItemState(workItem.WorkItemStateTypeless);
            EnqueueWorkItemInternal(workItem);
            return workItemState;
        }

        public IWorkItemState EnqueueWorkItem<T1>(Action<T1> workerFunction, T1 arg1, CallbackFunction asyncCallback = null)
        {
            var workItem = GetWorkItem(asyncCallback);
            workItem.DelegateInputParameters = new object[] { arg1 };
            workItem.Delegate = delegateInputParameters =>
            {
                workerFunction.Invoke(arg1);
                return null;
            };

            var workItemState = new WorkItemState(workItem.WorkItemStateTypeless);
            EnqueueWorkItemInternal(workItem);
            return workItemState;
        }

        #endregion

        #region func

        public IWorkItemState<V> EnqueueWorkItem<V>(Func<V> workerFunction, CallbackFunction asyncCallback = null)
        {
            var workItem = GetWorkItem(asyncCallback);
            workItem.DelegateInputParameters = new object[] { };
            workItem.Delegate = delegateInputParameters => { return workerFunction.Invoke(); };

            var workItemState = new WorkItemState<V>(workItem.WorkItemStateTypeless);
            EnqueueWorkItemInternal(workItem);
            return workItemState;
        }

        public IWorkItemState<V> EnqueueWorkItem<T1, V>(Func<T1, V> workerFunction, T1 arg1,
            CallbackFunction asyncCallback = null)
        {
            var workItem = GetWorkItem(asyncCallback);

            workItem.DelegateInputParameters = new object[] { arg1 };
            workItem.Delegate = delegateInputParameters => workerFunction.Invoke(arg1);

            var workItemState = new WorkItemState<V>(workItem.WorkItemStateTypeless);
            EnqueueWorkItemInternal(workItem);
            return workItemState;
        }
        
        #endregion
    }
}