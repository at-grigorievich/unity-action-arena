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

        public bool IsAvailable { get; private set; } = true;
        
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
            IsAvailable = false;
            _attack.Start(_range.Value);
        }

        public void Reset()
        {
            IsAvailable = true;
        }

        public IReadOnlyCollection<IAttackable> EndSwing()
        {
            //Debug.Log("end swing");
            IsAvailable = true;
            return _attack.EndAndGetResult();
        }
        
        public void Tick() => _attack.Tick();
    }
}