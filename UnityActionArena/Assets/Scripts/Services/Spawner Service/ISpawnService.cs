using System;
using ATG.DateTimers;

namespace ATG.Spawn
{
    public interface ISpawnService
    {
        event Action<ISpawnable> OnStartSpawned;
        event Action<ISpawnable> OnFinishSpawned;
        
        public bool GetSpawnTimer(ISpawnable spawnOwner, out CooldownTimer result);
        
        void SpawnInstantly(ISpawnable obj);
        void SpawnAfterDelay(ISpawnable obj);
    }
}