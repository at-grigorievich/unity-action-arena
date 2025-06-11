using MBT;
using UnityEngine;

namespace ATG.Character.AI
{
    [AddComponentMenu("")]
    [MBTNode("Bot Conditions/Is Enemy Moving")]
    public class BTCondition_IsEnemyMoving: BTBot_Condition
    {
        protected override bool CheckCondition()
        {
            if (_bot.TargetDetectedEnemy == null) return false;

            var data = _bot.TargetDetectedEnemy.GetEnemyData();

            return data.CurrentSpeed > 0f;
        }
    }
}