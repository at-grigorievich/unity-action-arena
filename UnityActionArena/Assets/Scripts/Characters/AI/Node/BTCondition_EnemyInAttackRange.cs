using ATG.EnemyDetector;
using MBT;
using UnityEngine;

namespace ATG.Character.AI
{
    [AddComponentMenu("")]
    [MBTNode("Bot Conditions/Enemy In Attack Range")]
    public class BTCondition_EnemyInAttackRange: BTBot_Condition
    {
        protected override bool CheckCondition()
        {
            EnemyData enemyData = _bot.WeakestDetectedEnemy.GetEnemyData();
            Vector3 enemyPosition = enemyData.Transform.position;

            float distance = Vector3.Distance(_bot.Position, enemyPosition);
            
            return distance <= _bot.AttackRange;
        }
    }
}