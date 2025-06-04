using UnityEngine;

namespace ATG.Move
{
    public interface IMoveableService
    {
        void SetActive(bool isActive);
        void MoveTo(Vector3 position);
        void PlaceTo(Vector3 position, Quaternion rotation);
        void Stop();
    }
}