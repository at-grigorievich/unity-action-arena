using System;
using System.Collections.Generic;
using UnityEngine;
using VContainer.Unity;

namespace ATG.Attack
{
    public sealed class OverlapCapsuleCastAttack: ITickable
    {
        private readonly AttackPoint _attackPoint;
        private readonly HashSet<IAttackable> _attackablesByLast;
        private readonly int _mask;
        
        private IAttackable _owner;
        
        private float _range;

        private bool _allowAttack = false;

        public event Action<IAttackable> OnRequestToDealDamage; 
        
        public OverlapCapsuleCastAttack(AttackPoint attackPoint)
        {
            _attackPoint = attackPoint;
            _attackablesByLast = new HashSet<IAttackable>();
            _mask = LayerMask.GetMask("Default");
        }
        
        public void InitOwner(IAttackable owner) => _owner = owner;
        
        public void Start(float range)
        {
            _attackablesByLast.Clear();
            
            _range = range;
            _allowAttack = true;
        }
        
        public IReadOnlyCollection<IAttackable> EndAndGetResult()
        {
            _allowAttack = false;
            return _attackablesByLast;
        }
        
        public void Tick()
        {
            if(_allowAttack == false) return;

            Collider[] hits;
            
            int hitCount = OverlapCapsuleCastPool.Hit(_attackPoint.Position, _attackPoint.Forward, _range, _mask, out hits);
            
            if(hitCount <= 0) return;
            
            for (int i = 0; i < hitCount; i++)
            {
                Collider hit = hits[i];
                
                Transform hitTransform = hit.transform;

                if(hitTransform.TryGetComponent(out IAttackable attackable) == false) continue;
                if(ReferenceEquals(_owner, attackable) == true) continue;
                
                //Debug.Log("Hit: " + hit.name);
                
                if (_attackablesByLast.Contains(attackable) == false)
                {
                    OnRequestToDealDamage?.Invoke(attackable);
                }
                _attackablesByLast.Add(attackable);
            }
        }
    }
}