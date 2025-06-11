using UnityEngine;

namespace ATG.Attack
{
    public readonly struct AttackDamageData
    {
        public readonly float AttackerRate;
        public readonly int Damage;
        
        public readonly Vector3 AttackerPosition;
        public readonly Vector3 AttackerDirection;
        
        public AttackDamageData(Transform attackerTransform, int damage, float rate)
        {
            AttackerPosition = attackerTransform.position;
            AttackerDirection = attackerTransform.forward;
            
            AttackerRate = rate;
            Damage = damage;
        }
    }
}