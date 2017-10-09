using System.Threading;

namespace Crossbar.Teste.Infra
{
    public class WorkItem
    {
        private object result;
        public bool IsCompleted { get; set; }
        internal ThreadPool.WorkItemCallback Delegate { get; set; }
        public object DelegateInputParameters { get; set; }
        public WorkItemStateTypeless WorkItemStateTypeless { get; set; }
        public ThreadRunner SingleThreadRunner { get; set; }

        public object Result
        {
            get
            {
                // SpinWait for the workItem to finish.
                var spinWait = new SpinWait();
                while (!IsCompleted)
                {
                    spinWait.SpinOnce();
                    Thread.MemoryBarrier();
                }
                return result;
            }
            set { result = value; }
        }

        internal WorkItem()
        {
        }

        internal WorkItem(ThreadPool.WorkItemCallback functionDelegate, object delegateInputParameters)
        {
            Delegate = functionDelegate;
            DelegateInputParameters = delegateInputParameters;
            WorkItemStateTypeless = new WorkItemStateTypeless(this);
        }

        public ThreadPool.CallbackFunction AsyncCallback { get; set; }
    }
}