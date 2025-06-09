using System;
using UnityEngine;

namespace ATG.Spawn
{
    public interface ISpawnable
    {
        event Action<ISpawnable> OnSpawnRequired; 
        void Spawn(Vector3 spawnPosition, Quaternion spawnRotation);
    }
}