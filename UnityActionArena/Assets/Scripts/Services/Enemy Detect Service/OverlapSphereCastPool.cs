using UnityEngine;

namespace ATG.EnemyDetector
{
    public static class OverlapSphereCastPool
    {
        private static int BUFFER_SIZE = 40;
        private static Collider[] _hits;

        static OverlapSphereCastPool()
        {
            _hits = new Collider[BUFFER_SIZE];
        }
        
        public static int Cast(Vector3 origin, float radius, int mask, out Collider[] hits)
        {
#if UNITY_EDITOR
            Debug.DrawRay(origin, origin + Vector3.up, Color.red);    
            Debug.DrawLine(origin + Vector3.up, origin + Vector3.up +  Vector3.forward * radius, Color.yellow);
#endif

            int hitCount = Physics.OverlapSphereNonAlloc(origin, radius, _hits, mask);
            
            hits = _hits;

            return hitCount;
        }
    }
}