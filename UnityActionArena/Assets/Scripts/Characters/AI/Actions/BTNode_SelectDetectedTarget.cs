using ATG.EnemyDetector;
using MBT;
using UnityEngine;

namespace ATG.Character.AI
{
    [AddComponentMenu("")]
    [MBTNode("Bot Actions/Select Detected Target")]
    public class BTNode_SelectDetectedTarget: BTBot_Action
    {
        [SerializeField] private EnemyDetectorType detectorType;
        
        public override NodeResult Execute()
        {
            if (_bot.HasDetectedEnemies.Value == false) return NodeResult.failure;

            if (_bot.TryDetectTargetByType(detectorType, out IDetectable selected) == false)
            {
                return NodeResult.failure;
            }
            
            _bot.SetTargetEnemy(selected);
            return NodeResult.success;
        }
    }
}