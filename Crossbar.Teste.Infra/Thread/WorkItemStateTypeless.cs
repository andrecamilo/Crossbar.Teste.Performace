namespace Crossbar.Teste.Infra
{
    public class WorkItemStateTypeless : IWorkItemStateTypeless
    {
        public WorkItem WorkItem { get; set; }

        public bool IsStopped => WorkItem.IsCompleted;

        public object Result => WorkItem.Result;

        public WorkItemStateTypeless(WorkItem workItem)
        {
            WorkItem = workItem;
        }

        public void Dispose() { }
    }
}