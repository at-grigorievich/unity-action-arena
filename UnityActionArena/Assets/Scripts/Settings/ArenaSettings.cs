using UnityEngine;

namespace Settings
{
    public interface IArenaSize
    {
        int PlayersOnArena { get; }
    }

    public interface IArenaRespawnDelay
    {
        int CharacterRespawnDelayMs { get; }
    }

    public interface IStaminaReset
    {
        int DefaultReduceAmount { get; }
        int DefaultResetAmount { get; }
        float DelayAfterReduce { get; }
    }
    
    [CreateAssetMenu(fileName = "arena-settings", menuName = "Configs/New Arena Settings", order = 0)]
    public sealed class ArenaSettings: ScriptableObject, IArenaSize, IArenaRespawnDelay, IStaminaReset
    {
        [field: SerializeField] public int PlayersOnArena { get; private set; }
        [field: SerializeField] public int CharacterRespawnDelayMs { get; private set; }
        [field: SerializeField] public int DefaultReduceAmount { get; private set; }
        [field: SerializeField] public int DefaultResetAmount { get; private set; }
        [field: SerializeField] public float DelayAfterReduce { get; private set; }
    }
}