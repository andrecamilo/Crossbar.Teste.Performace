namespace Crossbar.Teste.Infra
{

    public struct WorkItemState<T> : IWorkItemState<T>
    {
        private bool disposed;
        private readonly WorkItemStateTypeless workItemStateTypeless;

        public bool IsStopped => workItemStateTypeless.IsStopped;

        public T Result => (T) workItemStateTypeless.Result;

        public WorkItemState(WorkItemStateTypeless workItemStateTypeless)
        {
            this.workItemStateTypeless = workItemStateTypeless;
            disposed = false;
        }

        public void Dispose(bool disposing)
        {
            if (!disposed)
            {
                if (disposing)
                {
                    workItemStateTypeless.WorkItem.SingleThreadRunner.ThreadPool.ReturnWorkItem(
                        workItemStateTypeless.WorkItem);
                }
                disposed = true;
            }
        }

        public void Dispose()
        {
            Dispose(true);
        }
    }

    public struct WorkItemState : IWorkItemState
    {
        private bool disposed;
        private readonly WorkItemStateTypeless workItemStateTypeless;

        public bool IsStopped => workItemStateTypeless.IsStopped;

        /// <param name="workItemStateTypeless">State of the work item.</param>
        public WorkItemState(WorkItemStateTypeless workItemStateTypeless)
        {
            this.workItemStateTypeless = workItemStateTypeless;
            disposed = false;
        }

        public void Result()
        {
#pragma warning disable 168
            var o = workItemStateTypeless.Result;
#pragma warning restore 168
        }

        public void Dispose(bool disposing)
        {
            if (!disposed)
            {
                if (disposing)
                {
                    workItemStateTypeless.WorkItem.SingleThreadRunner.ThreadPool.ReturnWorkItem(
                        workItemStateTypeless.WorkItem);
                }
                disposed = true;
            }
        }

        public void Dispose()
        {
            Dispose(true);
        }
    }
}