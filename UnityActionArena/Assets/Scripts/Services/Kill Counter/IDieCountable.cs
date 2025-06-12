using System;
using ATG.Attack;

namespace ATG.KillCounter
{
    public interface IDieCountable
    {
        event Action<AttackDamageData> OnDieCountRequired;
    }
}