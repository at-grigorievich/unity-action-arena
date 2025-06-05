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

        public IAttackService Create(IReadOnlyObservableVar<float> range)
        {
            return new RaycastAttackService(attackPoint, range);
        }
    }
    
    public sealed class RaycastAttackService: IAttackService
    {
        private readonly IReadOnlyObservableVar<float> _range;

        private readonly RaycastAttack _attack;

        public RaycastAttackService(AttackPoint attackPoint, IReadOnlyObservableVar<float> range)
        {
            _range = range;
            _attack = new RaycastAttack(attackPoint);
        }

        public void InitOwner(IAttackable owner) => _attack.InitOwner(owner);

        public void TakeSwing()
        {
            Debug.Log("take swing");
            _attack.Start(_range.Value);
        }

        public IEnumerable<IAttackable> EndSwing()
        {
            Debug.Log("end swing");
            return _attack.EndAndGetResult();
        }
        
        public void Tick() => _attack.Tick();
    }
}