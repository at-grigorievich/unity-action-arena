using System;
using System.Collections.Generic;
using ATG.Observable;
using UnityEngine;

namespace ATG.Attack
{
    [Serializable]
    public sealed class RaycastAttackServiceCreator
    {
        [SerializeField] private AttackPoint attackPoint;

        public IAttackService Create(IReadOnlyObservableVar<float> range)
        {
            return new OverlapCapsuleCastAttackService(attackPoint, range);
        }
    }
    
    public sealed class OverlapCapsuleCastAttackService: IAttackService
    {
        private readonly IReadOnlyObservableVar<float> _range;

        private readonly OverlapCapsuleCastAttack _attack;
        
        public event Action<IAttackable> OnRequestToDealDamage
        {
            add => _attack.OnRequestToDealDamage += value;
            remove => _attack.OnRequestToDealDamage -= value;
        }

        public OverlapCapsuleCastAttackService(AttackPoint attackPoint, IReadOnlyObservableVar<float> range)
        {
            _range = range;
            _attack = new OverlapCapsuleCastAttack(attackPoint);
        }
        
        public void InitOwner(IAttackable owner) => _attack.InitOwner(owner);

        public void TakeSwing()
        {
            //Debug.Log("take swing");
            _attack.Start(_range.Value);
        }

        public void Stop()
        {
            _attack.Stop();
        }

        public IReadOnlyCollection<IAttackable> EndSwing()
        {
            //Debug.Log("end swing");
            return _attack.StopAndGetResult();
        }
        
        public void Tick() => _attack.Tick();
    }
}