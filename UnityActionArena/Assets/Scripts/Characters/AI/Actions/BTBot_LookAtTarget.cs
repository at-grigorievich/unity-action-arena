using MBT;
using UnityEngine;

namespace ATG.Character.AI
{
    [AddComponentMenu("")]
    [MBTNode("Bot Actions/Look At Target")]
    public class BTBot_LookAtTarget: BTBot_Action
    {
        public override NodeResult Execute()
        {
            if(_bot.WeakestDetectedEnemy == null) return NodeResult.failure;

            Transform target = _bot.WeakestDetectedEnemy.GetEnemyData().Transform;
            _bot.LookAt(target.position);
           
            return NodeResult.success;
        }
    }
}