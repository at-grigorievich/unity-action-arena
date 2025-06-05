using UnityEngine;

namespace ATG.Attack
{
    public static class OverlapCapsuleCastPool
    {
        private static int BUFFER_SIZE = 25;
        private static Collider[] _hits;

        static OverlapCapsuleCastPool()
        {
            _hits = new Collider[BUFFER_SIZE];
        }

        public static int Hit(Vector3 origin, Vector3 direction, float range, int mask, out Collider[] hits)
        {
#if UNITY_EDITOR
            Debug.DrawRay(origin, direction * range, Color.red);      
#endif
            Vector3 capsuleStart = origin;
            Vector3 capsuleEnd = origin + direction * (range);
            
            int capsuleHitCount = Physics.OverlapCapsuleNonAlloc(
                capsuleStart,
                capsuleEnd,
                0.2f,
                _hits,
                mask
            );

            hits = _hits;

            return capsuleHitCount;
        }
    }
}