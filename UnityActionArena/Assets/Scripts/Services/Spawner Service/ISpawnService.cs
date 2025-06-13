using ATG.DateTimers;

namespace ATG.Spawn
{
    public interface ISpawnService
    {
        public bool GetSpawnTimer(ISpawnable spawnOwner, out CooldownTimer result);
        
        void SpawnInstantly(ISpawnable obj);
        void SpawnAfterDelay(ISpawnable obj);
    }
}