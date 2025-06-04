using System.Collections.Generic;

namespace ATG.Attack
{
    public interface IAttackService
    {
        void TakeSwing();
        IEnumerable<IAttackable> EndSwing();
    }
}