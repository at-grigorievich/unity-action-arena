using UnityEngine;

namespace ATG.EnemyDetector
{
    public readonly struct EnemyData
    {
        public readonly Transform Transform;
        public readonly float Rate;
        
        public EnemyData(Transform transform, float rate)
        {
            Transform = transform;
            Rate = rate;
        }
    }
    
    public interface IDetectable
    {
        EnemyData GetEnemyData();
    }
}