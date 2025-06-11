using UnityEngine;

namespace ATG.EnemyDetector
{
    public readonly struct EnemyData
    {
        public readonly Transform Transform;
        public readonly float SkillRate;
        public readonly float CurrentSpeed;
        
        public EnemyData(Transform transform, float skillRate, float currentSpeed)
        {
            Transform = transform;
            SkillRate = skillRate;
            CurrentSpeed = currentSpeed;
        }
    }
    
    public interface IDetectable
    {
        EnemyData GetEnemyData();
    }
}