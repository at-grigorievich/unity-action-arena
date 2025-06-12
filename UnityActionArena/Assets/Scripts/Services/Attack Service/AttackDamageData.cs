using UnityEngine;

namespace ATG.Attack
{
    public readonly struct AttackDamageData
    {
        public readonly string AttackerName;
        
        public readonly float AttackerRate;
        public readonly int Damage;
        
        public readonly Vector3 AttackerPosition;
        public readonly Vector3 AttackerDirection;
        
        public AttackDamageData(string attackerName, Transform attackerTransform, int damage, float rate)
        {
            AttackerName = attackerName;
            
            AttackerPosition = attackerTransform.position;
            AttackerDirection = attackerTransform.forward;
            
            AttackerRate = rate;
            Damage = damage;
        }
    }
}