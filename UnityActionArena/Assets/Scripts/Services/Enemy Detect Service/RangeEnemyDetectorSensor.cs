using System.Collections.Generic;
using ATG.Observable;
using UnityEngine;
using UnityEngine.SocialPlatforms;

namespace ATG.EnemyDetector
{
    public sealed class RangeEnemyDetectorSensor: IEnemyDetectorSensor
    {
        private const float RANGE_MIN = 3.0f;
        
        private IDetectable _owner;
        private readonly IReadOnlyObservableVar<float> _range;
        private readonly Transform _ownerTransform;
        private readonly int _mask;
        
        private List<IDetectable> _detectables = new List<IDetectable>();
        
        public RangeEnemyDetectorSensor(IReadOnlyObservableVar<float> range, IDetectable owner)
        {
            _range = range;
            _owner = owner;
            _ownerTransform = owner.GetEnemyData().Transform;
            
            _mask = LayerMask.GetMask("Default");
        }

        public bool CheckDetect()
        {
            CastToDetect();
            return _detectables.Count > 0;
        }

        public bool TryDetect(EnemyDetectorType type, out IDetectable detectable)
        {
            detectable = null;
            
            if(_detectables.Count == 0) return false;

            switch (type)
            {
                case EnemyDetectorType.FIND_WEAKEST:
                    detectable = FindWeakestTarget(_detectables);
                    break;
                case EnemyDetectorType.FIND_BY_NEAREST_DISTANCE:
                    detectable = FindNearestTarget(_detectables);
                    break;
            }

            return true;
        }
        
        private IDetectable FindWeakestTarget(List<IDetectable> detectables)
        {
            IDetectable weakestTarget = null;
            float lowestSkillRate = float.MaxValue;

            foreach (var d in detectables)
            {
                float skillRate = d.GetEnemyData().SkillRate;

                if (skillRate < lowestSkillRate)
                {
                    lowestSkillRate = skillRate;
                    weakestTarget = d;
                }
            }
            
            return weakestTarget;
        }

        private IDetectable FindNearestTarget(List<IDetectable> detectables)
        {
            IDetectable nearestTarget = null;
            float shortestDistance = float.MaxValue;

            foreach (var d in detectables)
            {
                var detectableData = d.GetEnemyData();
                float distance = Vector3.Distance(_ownerTransform.position, detectableData.Transform.position);

                if (distance < shortestDistance)
                {
                    shortestDistance = distance;
                    nearestTarget = d;
                }
            }

            return nearestTarget;
        }
        
        private void CastToDetect()
        {
            _detectables.Clear();
            
            int hitCount = OverlapSphereCastPool.Cast(_ownerTransform.position, _range.Value + RANGE_MIN, 
                _mask, out Collider[] colliders);
            
            if (hitCount <= 0) return;
            
            for (int i = 0; i < hitCount; i++)
            {
                Collider collider = colliders[i];
                
                if(collider.TryGetComponent(out IDetectable detected) == false) continue;
                if(ReferenceEquals(_owner, detected) == true) continue;

                _detectables.Add(detected);
            }
        }
    }
}