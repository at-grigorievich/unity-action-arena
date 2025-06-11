using MBT;
using UnityEngine;

namespace ATG.Character.AI
{
    [AddComponentMenu("")]
    [MBTNode("Bot Actions/Stay In Shock After Damage")]
    public class BTBot_StayInShockAfterDamage: BTBot_Action
    {
        public override NodeResult Execute()
        {
            if(_bot.IsGetDamage.Value == true) return NodeResult.running;
            return NodeResult.success;
        }
    }
}