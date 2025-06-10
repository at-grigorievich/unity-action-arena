using System;
using System.Collections.Generic;
using ATG.Observable;
using UnityEngine;

namespace ATG.EnemyDetector
{
    public sealed class RangeEnemyDetector: IEnemyDetector
    {
        private const float RANGE_MULTIPLIER = 2.0f;
        
        private IDetectable _owner;
        private readonly IReadOnlyObservableVar<float> _range;
        private readonly Transform _ownerTransform;
        private readonly int _mask;

        public RangeEnemyDetector(IReadOnlyObservableVar<float> range, IDetectable owner)
        {
            _range = range;
            _owner = owner;
            _ownerTransform = owner.GetEnemyData().Transform;
            
            _mask = LayerMask.GetMask("Default");
        }
        
        public IEnumerable<IDetectable> Detect()
        {
            int hitCount = OverlapSphereCastPool.Cast(_ownerTransform.position, _range.Value * RANGE_MULTIPLIER, 
                _mask, out Collider[] colliders);

            if (hitCount <= 0) return Array.Empty<IDetectable>();
            
            List<IDetectable> detectables = new List<IDetectable>();
            
            for (int i = 0; i < hitCount; i++)
            {
                Collider collider = colliders[i];
                
                if(collider.TryGetComponent(out IDetectable detected) == false) continue;
                if(ReferenceEquals(_owner, detected) == true) continue;
                
                detectables.Add(detected);
            }
            
            return null;
        }
        
    }
}