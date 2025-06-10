using MBT;
using UnityEngine;

namespace ATG.Character.AI
{
    [AddComponentMenu("")]
    [MBTNode("Bot Actions/Stop Moving")]
    
    public class BTBot_StopMoving: BTBot_Action
    {
        public override NodeResult Execute()
        {
            _bot.Stop();
            return NodeResult.success;
        }
    }
}