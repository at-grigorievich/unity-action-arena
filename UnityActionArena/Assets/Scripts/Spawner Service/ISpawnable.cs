using UnityEngine;

namespace ATG.Spawn
{
    public interface ISpawnable
    {
        void Spawn(Vector3 spawnPosition, Quaternion spawnRotation);
    }
}