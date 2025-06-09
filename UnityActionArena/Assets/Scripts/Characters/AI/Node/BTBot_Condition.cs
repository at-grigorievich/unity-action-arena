using ATG.Observable;
using MBT;
using UnityEngine;

namespace ATG.Character.AI
{
    public abstract class BTBot_Condition<T> : Condition
    {
        [SerializeField] private CharacterView botView;
        
        protected IObservableVar<T> Variable;
        
        private ObserveDisposable _dis;
        
        public bool invert = false;
        public Abort abort;
        
        protected virtual void Start()
        {
            if (botView.MyPresenter is not BotPresenter bot)
            {
                throw new UnityException("Presenter is not BotPresenter");
            }

            Variable = InitializeVariable(bot);
        }
        
        protected abstract IObservableVar<T> InitializeVariable(BotPresenter presenter);
        protected abstract bool CheckCondition();
        
        public override bool Check()
        {
            return (CheckCondition() == true) ^ invert;
        }
        
        public override void OnAllowInterrupt()
        {
            if (abort != Abort.None)
            {
                ObtainTreeSnapshot();
                _dis = Variable.Subscribe(OnVariableChange);
            }
        }

        public override void OnDisallowInterrupt()
        {
            if (abort != Abort.None)
            {
                _dis?.Dispose();
                _dis = null;
            }
        }

        private void OnVariableChange(T newValue)
        {
            EvaluateConditionAndTryAbort(abort);
        }
    }
}