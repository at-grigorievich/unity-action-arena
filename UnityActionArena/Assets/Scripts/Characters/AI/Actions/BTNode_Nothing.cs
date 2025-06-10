using MBT;
using UnityEngine;

namespace ATG.Character.AI
{
    [AddComponentMenu("")]
    [MBTNode("Bot Actions/Nothing")]
    public class BTNode_Nothing: Leaf
    {
        [SerializeField] private MBT.Status result = MBT.Status.Running;
        
        public override NodeResult Execute()
        {
            return new NodeResult(result);
        }
    }
}