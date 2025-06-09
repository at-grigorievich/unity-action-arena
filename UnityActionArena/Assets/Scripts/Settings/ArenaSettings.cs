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
    
    [CreateAssetMenu(fileName = "arena-settings", menuName = "Configs/New Arena Settings", order = 0)]
    public sealed class ArenaSettings: ScriptableObject, IArenaSize, IArenaRespawnDelay
    {
        [field: SerializeField] public int PlayersOnArena { get; private set; }
        [field: SerializeField] public int CharacterRespawnDelayMs { get; private set; }
    }
}