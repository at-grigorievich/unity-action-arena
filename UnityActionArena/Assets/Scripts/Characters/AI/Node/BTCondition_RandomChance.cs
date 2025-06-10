using MBT;
using UnityEngine;

namespace ATG.Character.AI
{
    [AddComponentMenu("")]
    [MBTNode("Bot Conditions/Random Chance")]
    public class BTCondition_RandomChance: Condition
    {
        [SerializeField] private float trueIfMoreThan = 0.5f;
        
        public override bool Check()
        {
            int randomChanceInt = UnityEngine.Random.Range(0, 10);
            float randomChance = randomChanceInt / 10f;
            
            return randomChance > trueIfMoreThan;
        }
    }
}