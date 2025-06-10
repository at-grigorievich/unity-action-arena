using MBT;
using UnityEngine;

namespace ATG.Character.AI
{
    [AddComponentMenu("")]
    [MBTNode("Bot Actions/Start Attack")]
    public class BTBot_StartAttack: BTBot_Action
    {
        public override NodeResult Execute()
        {
            if(_bot.EnoughStamina == false) return NodeResult.failure;
            
            _bot.Attack();
            return NodeResult.success;
        }
    }
}