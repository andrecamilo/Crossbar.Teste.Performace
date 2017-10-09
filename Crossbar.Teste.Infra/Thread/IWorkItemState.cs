using System;

namespace Crossbar.Teste.Infra
{
    public interface IWorkItemState<out T> : IDisposable
    {
        bool IsStopped { get; }
        T Result { get; }
    }

    public interface IWorkItemState : IDisposable
    {
        bool IsStopped { get; }
        void Result();
    }
}