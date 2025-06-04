using UnityEngine;

namespace ATG.Attack
{
    public sealed class RaycastPool
    {
        private readonly RaycastHit[] _hits;

        public RaycastPool(int hitBufferSize)
        {
            _hits = new RaycastHit[hitBufferSize];
        }

        public int Hit(Vector3 origin, Vector3 direction, float range, out RaycastHit[] hits)
        {
            Debug.DrawRay(origin, direction * range, Color.red);
            
            int hitCount = Physics.RaycastNonAlloc(origin, direction, _hits, range);
            
            hits = _hits;
            return hitCount;
        }
    }
}