using ATG.EnemyDetector;
using MBT;
using UnityEngine;

namespace ATG.Character.AI
{
    [AddComponentMenu("")]
    [MBTNode("Bot Conditions/Is Enemy Is Near")]
    public class BTCondition_IsEnemyIsNear: BTBot_Condition
    {
        [SerializeField] private float maxDistance = 1.0f;
        
        protected override bool CheckCondition()
        {
            if (_bot.TryDetectTargetByType(EnemyDetectorType.FIND_BY_NEAREST_DISTANCE,out IDetectable target) == false) return false;
            
            var targetPosition = target.GetEnemyData().Transform.position;
            
            return Vector3.Distance(_bot.Position, targetPosition) <= maxDistance;
        }
    }
}