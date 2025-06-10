using UnityEngine;

namespace ATG.Move
{
    public sealed class TransformMoveService: IMoveableService
    {
        private readonly Transform _transform;

        public TransformMoveService(Transform transform)
        {
            _transform = transform;
        }
        
        public void SetActive(bool isActive) { }

        public void MoveTo(Vector3 position) { }

        public void PlaceTo(Vector3 position, Quaternion rotation)
        {
            _transform.position = position;
            _transform.rotation = rotation;
        }

        public bool CanReach(Vector3 inputPosition, out Vector3 resultPosition)
        {
            resultPosition = inputPosition;
            return true;
        }

        public void Stop() { }
    }
}