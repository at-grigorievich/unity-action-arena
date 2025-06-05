using UnityEngine;

namespace ATG.Attack
{
    public static class RaycastPool
    {
        private static int BUFFER_SIZE = 25;
        private static RaycastHit[] _hits;

        static RaycastPool()
        {
            _hits = new RaycastHit[BUFFER_SIZE];
        }

        public static int Hit(Vector3 origin, Vector3 direction, float range, out RaycastHit[] hits)
        {
            Debug.DrawRay(origin, direction * range, Color.red);
            
            int hitCount = Physics.RaycastNonAlloc(origin, direction, _hits, range);

            hits = _hits;
            return hitCount;
        }
    }
}