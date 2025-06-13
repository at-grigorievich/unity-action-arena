using System;
using UnityEngine;

namespace Settings
{
    public interface IArenaSize
    {
        int PlayersOnArena { get; }
    }

    public interface IArenaRespawnDelay
    {
        float RespawnDelayInSec { get; }
        
        public TimeSpan RespawnSpan { get; }
    }

    [CreateAssetMenu(fileName = "arena-settings", menuName = "Configs/New Arena Settings", order = 0)]
    public sealed class ArenaSettings : ScriptableObject, IArenaSize, IArenaRespawnDelay
    {
        [field: SerializeField] public float RespawnDelayInSec { get; private set; }
        [field: SerializeField] public int PlayersOnArena { get; private set; }

        public TimeSpan RespawnSpan => TimeSpan.FromSeconds(RespawnDelayInSec);
    }
}