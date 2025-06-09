using MBT;
using UnityEngine;

namespace ATG.Character.AI
{
    [AddComponentMenu("")]
    [MBTNode("Bot Actions/Stay In Shock")]
    public class BTNode_StayInShock: BTBot_Action
    {
        [SerializeField] private float shockDuration = 2f;
        
        private float _shockTimer;
        
        protected override void Start()
        {
            base.Start();
            _shockTimer = 0f;
        }
        
        public override NodeResult Execute()
        {
            if(_bot == null) return NodeResult.failure;

            if (_shockTimer >= shockDuration)
            {
                _shockTimer = 0;
                _bot.IsGetDamage.Value = false;
                
                return NodeResult.success;
            }
            
            _bot.Stop();
            _shockTimer += Time.deltaTime;
            
            return NodeResult.running;
        }
    }
}