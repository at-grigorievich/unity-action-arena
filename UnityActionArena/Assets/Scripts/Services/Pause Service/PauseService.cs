using System.Collections.Generic;

namespace ATG.Pause
{
    public sealed class PauseService: IPauseService
    {
        private IEnumerable<IPausable> _pausables;
        public bool IsPaused { get; private set; }
        
        public PauseService(IEnumerable<IPausable> pausables)
        {
            _pausables = pausables;
        }
        
        public void Initialize()
        {
            SetPauseStatus(false);
        }
        
        public void Dispose()
        {
            SetPauseStatus(false);
        }
        
        public void SetPauseStatus(bool isPaused)
        {
            IsPaused = isPaused;

            foreach (var pausable in _pausables)
            {
                pausable.SetPauseStatus(isPaused);
            }
        }
    }
}