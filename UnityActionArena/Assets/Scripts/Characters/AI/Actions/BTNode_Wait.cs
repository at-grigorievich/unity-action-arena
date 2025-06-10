using MBT;
using UnityEngine;

namespace ATG.Character.AI
{
    [AddComponentMenu("")]
    [MBTNode("Bot Actions/Wait")]
    public class BTNode_Wait: Leaf
    {
        [SerializeField] private float duration = 1f;

        private float _currentTimer = 0f;
        
        public override NodeResult Execute()
        {
            if (_currentTimer >= duration)
            {
                _currentTimer = 0f;
                return NodeResult.success;
            }
            
            _currentTimer += Time.deltaTime;
            return NodeResult.running;
        }
    }
}