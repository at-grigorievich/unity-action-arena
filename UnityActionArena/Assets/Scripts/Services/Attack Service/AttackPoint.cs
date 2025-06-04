using UnityEngine;

namespace ATG.Attack
{
    public class AttackPoint: MonoBehaviour
    {
        public Vector3 Position => transform.position;
        public Vector3 Forward => transform.forward;
    }
}