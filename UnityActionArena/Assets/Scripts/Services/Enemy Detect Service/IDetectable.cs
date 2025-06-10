using UnityEngine;

namespace ATG.EnemyDetector
{
    public readonly struct EnemyData
    {
        public readonly Transform Transform;

        public EnemyData(Transform transform)
        {
            Transform = transform;
        }
    }
    
    public interface IDetectable
    {
        EnemyData GetEnemyData();
    }
}