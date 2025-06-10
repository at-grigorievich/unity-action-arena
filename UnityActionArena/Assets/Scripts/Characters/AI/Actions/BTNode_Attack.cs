using MBT;
using UnityEngine;

namespace ATG.Character.AI
{
    [AddComponentMenu("")]
    [MBTNode("Bot Actions/Attack")]
    public class BTNode_Attack: BTBot_Action
    {
        private bool _alreadyAttacked;
        
        public override NodeResult Execute()
        {
            if(_bot.EnoughStamina == false) return NodeResult.failure;

            if (_bot.IsAttacking.Value == false && _alreadyAttacked == false)
            {
                _bot.Stop();
                _bot.Attack();
                _alreadyAttacked = true;
                return NodeResult.running;
            }

            if (_bot.IsAttacking.Value == true && _alreadyAttacked == true) return NodeResult.running;

            if (_bot.IsAttacking.Value == false && _alreadyAttacked == true)
            {
                _alreadyAttacked = false;
                return NodeResult.success;
            }
            
            return NodeResult.running;
        }
    }
}