using System.Collections.Generic;
using UnityEngine;
using VContainer.Unity;

namespace ATG.Attack
{
    public sealed class RaycastAttack: ITickable
    {
        private readonly AttackPoint _attackPoint;
        private readonly HashSet<IAttackable> _attackablesByLast;

        private IAttackable _owner;
        
        private float _range;

        private bool _allowAttack = false;

        public RaycastAttack(AttackPoint attackPoint)
        {
            _attackPoint = attackPoint;
            _attackablesByLast = new HashSet<IAttackable>();
        }
        
        public void InitOwner(IAttackable owner) => _owner = owner;
        
        public void Start(float range)
        {
            _attackablesByLast.Clear();
            
            _range = range;
            _allowAttack = true;
        }
        
        public IEnumerable<IAttackable> EndAndGetResult()
        {
            _allowAttack = false;
            return _attackablesByLast;
        }
        
        public void Tick()
        {
            if(_allowAttack == false) return;

            RaycastHit[] hits;
            
            int hitCount = RaycastPool.Hit(_attackPoint.Position, _attackPoint.Forward, _range, out hits);
            
            if(hitCount <= 0) return;
            
            for (int i = 0; i < hitCount; i++)
            {
                RaycastHit hit = hits[i];
                
                //Debug.Log("Hit: " + hit.collider.name + " at distance " + hit.distance);

                Transform hitTransform = hit.collider.transform;

                if(hitTransform.TryGetComponent(out IAttackable attackable) == false) continue;
                if(ReferenceEquals(_owner, attackable) == true) continue;
                
                //Debug.Log($"{hitTransform.name} is attackable");
                
                _attackablesByLast.Add(attackable);
            }
        }
    }
}