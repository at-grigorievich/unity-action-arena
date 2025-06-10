using MBT;
using UnityEngine;

namespace ATG.Character.AI
{
    [AddComponentMenu("")]
    [MBTNode("Bot Actions/End Attack")]
    public class BTBot_EndAttack: BTBot_Action
    {
        public override NodeResult Execute()
        {
            if(_bot.IsAttacking.Value == true) return NodeResult.running;
            return NodeResult.success;
        }
    }
}