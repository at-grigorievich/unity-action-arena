using System.Collections.Generic;
using VContainer.Unity;

namespace ATG.Attack
{
    public interface IAttackService: ITickable
    {
        void TakeSwing();
        IEnumerable<IAttackable> EndSwing();
    }
}