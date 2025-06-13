using System;
using ATG.Command;

namespace ATG.UI.Service
{
    public abstract class UIRootCommand: ICommand
    {
        protected readonly UIRootLocatorService _locator;
        
        public event Action<bool> OnCompleted;

        public UIRootCommand(UIRootLocatorService locator)
        {
            _locator = locator;
        }
        
        public abstract void Execute();

        public virtual void Dispose()
        {
            // TODO release managed resources here
        }

        protected void Complete()
        {
            OnCompleted?.Invoke(true);
        }
    }
}