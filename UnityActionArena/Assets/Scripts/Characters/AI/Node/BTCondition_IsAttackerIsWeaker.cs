using MBT;
using UnityEngine;

namespace ATG.Character.AI
{
    [AddComponentMenu("")]
    [MBTNode("Bot Conditions/Is Enemy Is Weaker")]
    public class BTCondition_IsAttackerIsWeaker: BTBot_Condition
    {
        protected override bool CheckCondition()
        {
            if (_bot.LastReceivedDamage.HasValue == false) return false;
			
            float botRate = _bot.Rate;
            float attackerRate = _bot.LastReceivedDamage.Value.AttackerRate;

            //Debug.Log($"bot rate: {botRate}; enemyRate: {attackerRate}");
            
            return botRate > attackerRate;
        }
    }
}