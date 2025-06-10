using MBT;
using UnityEngine;

namespace ATG.Character.AI
{
    [AddComponentMenu("")]
    [MBTNode("Bot Conditions/Can Attack")]
    public class BTCondition_CanAttack: BTBot_Condition
    {
        protected override bool CheckCondition()
        {
            return _bot.CanAttack;
        }
    }
}