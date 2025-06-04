using System;
using System.Collections.Generic;
using ATG.Observable;
using UnityEngine;
using VContainer.Unity;

namespace ATG.Attack
{
    [Serializable]
    public sealed class RaycastAttackServiceCreator
    {
        [SerializeField] private AttackPoint attackPoint;

        public IAttackService Create(RaycastPool raycastPool, IReadOnlyObservableVar<float> range)
        {
            return new RaycastAttackService(attackPoint, raycastPool, range);
        }
    }
    
    public sealed class RaycastAttackService: IAttackService, ITickable
    {
        private readonly IReadOnlyObservableVar<float> _range;

        private readonly RaycastAttack _attack;

        public RaycastAttackService(AttackPoint attackPoint, RaycastPool raycastPool, IReadOnlyObservableVar<float> range)
        {
            _range = range;
            _attack = new RaycastAttack(attackPoint, raycastPool);
        }

        public void TakeSwing()
        {
            _attack.Start(_range.Value);
        }

        public IEnumerable<IAttackable> EndSwing()
        {
            return _attack.EndAndGetResult();
        }
        
        public void Tick() => _attack.Tick();
    }
}