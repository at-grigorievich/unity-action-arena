using System;
using System.Collections.Generic;
using VContainer.Unity;

namespace ATG.Attack
{
    public interface IAttackService: ITickable
    {
        bool IsAvailable { get; }
     
        event Action<IAttackable> OnRequestToDealDamage; 
        
        void InitOwner(IAttackable owner);
        void TakeSwing();
        void Reset();
        IReadOnlyCollection<IAttackable> EndSwing();
    }
}