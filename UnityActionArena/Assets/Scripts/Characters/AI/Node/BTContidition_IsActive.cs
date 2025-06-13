using MBT;
using UnityEngine;

namespace ATG.Character.AI
{
    [AddComponentMenu("")]
    [MBTNode("Bot Conditions/Is Active")]
    public class BTContidition_IsActive: BTBot_Condition
    {
        protected override bool CheckCondition()
        {
            return _bot.IsActive;
        }
    }
}