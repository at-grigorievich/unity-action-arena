using UnityEngine;

namespace ATG.Move
{
    public class TargetNavigationPointSet: MonoBehaviour, INavigatablePoint
    {
        private TargetNavigationPoint[] _set;

        private void Awake()
        {
            _set = GetComponentsInChildren<TargetNavigationPoint>();
        }

        public Vector3 GetRandomPointInRadiusXZ()
        {
            return GetRndPoint().GetRandomPointInRadiusXZ();
        }

        private INavigatablePoint GetRndPoint()
        {
            int rndIndex = UnityEngine.Random.Range(0, _set.Length);
            return _set[rndIndex];
        }
    }
}