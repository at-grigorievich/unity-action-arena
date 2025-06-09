using MBT;
using UnityEngine;

namespace ATG.Character.AI
{
    [AddComponentMenu("")]
    [MBTNode("Bot Actions/Nothing")]
    public class BTNode_Nothing: Leaf
    {
        public override NodeResult Execute()
        {
            return NodeResult.running;
        }
    }
}