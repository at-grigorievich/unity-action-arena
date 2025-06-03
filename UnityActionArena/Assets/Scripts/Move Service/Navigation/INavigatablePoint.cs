using UnityEngine;

namespace ATG.Move
{
    public interface INavigatablePoint
    {
        Vector3 GetRandomPointInRadiusXZ();
    }
}