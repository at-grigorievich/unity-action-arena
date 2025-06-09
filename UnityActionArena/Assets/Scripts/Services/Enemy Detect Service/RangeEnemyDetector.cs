using System.Collections.Generic;
using ATG.Observable;
using UnityEngine;

namespace ATG.EnemyDetector
{
    public sealed class RangeEnemyDetector: IEnemyDetector
    {
        private readonly IReadOnlyObservableVar<float> _range;
        private readonly Transform _owner;
        private readonly int _mask;

        public RangeEnemyDetector(IReadOnlyObservableVar<float> range, Transform owner)
        {
            _range = range;
            _owner = owner;
            _mask = LayerMask.GetMask("Default");
        }
        
        public IEnumerable<IEnemy> Detect()
        {
            int hitCount = OverlapSphereCastPool.Cast(_owner.position, _range.Value, _mask, out Collider[] colliders);

            for (int i = 0; i < hitCount; i++)
            {
                
            }
            
            return null;
        }
        
    }
}