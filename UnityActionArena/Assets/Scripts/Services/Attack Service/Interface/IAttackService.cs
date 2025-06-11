using System;
using System.Collections.Generic;
using VContainer.Unity;

namespace ATG.Attack
{
    public interface IAttackService: ITickable
    {
        event Action<IAttackable> OnRequestToDealDamage; 
        
        void InitOwner(IAttackable owner);
        void TakeSwing();
        void Stop();
        IReadOnlyCollection<IAttackable> EndSwing();
    }
}