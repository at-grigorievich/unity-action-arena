using MBT;
using UnityEngine;

namespace ATG.Character.AI
{
    [AddComponentMenu("")]
    [MBTNode("Bot Conditions/Has Enough Stamina")]
    public class BTCondition_HasStamina: BTBot_Condition
    {
        protected override bool CheckCondition()
        {
            return _bot.EnoughStamina;
        }
    }
}