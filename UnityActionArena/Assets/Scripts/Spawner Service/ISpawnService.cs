using System;

namespace ATG.Spawn
{
    public interface ISpawnService
    {
        void SpawnInstantly(ISpawnable obj);
        void SpawnAfterDelay(ISpawnable obj);
    }
}