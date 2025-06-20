using ATG.Observable;
using MBT;
using UnityEngine;

namespace ATG.Character.AI
{
    public abstract class BTBot_VariableCondition<T> : BTBot_Condition
    {
        protected IObservableVar<T> Variable;
        
        private ObserveDisposable _dis;
        
        public Abort abort;
        
        protected override void Start()
        {
            base.Start();
            Variable = InitializeVariable(_bot);
        }
        
        protected abstract IObservableVar<T> InitializeVariable(BotPresenter presenter);
        
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

        protected virtual void OnVariableChange(T newValue)
        {
            EvaluateConditionAndTryAbort(abort);
        }
    }
}