using System;
using VContainer.Unity;

namespace ATG.Pause
{
    public interface IPauseService: IInitializable, IDisposable
    {
        public bool IsPaused { get; }
        public void SetPauseStatus(bool isPaused);
    }
}