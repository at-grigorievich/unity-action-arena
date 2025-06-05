using System.Collections.Generic;
using VContainer.Unity;

namespace ATG.Attack
{
    public interface IAttackService: ITickable
    {
        bool IsAvailable { get; }
        
        void InitOwner(IAttackable owner);
        void TakeSwing();
        IReadOnlyCollection<IAttackable> EndSwing();
    }
}