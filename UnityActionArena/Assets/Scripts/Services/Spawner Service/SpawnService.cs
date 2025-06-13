using System;
using System.Collections.Generic;
using ATG.DateTimers;
using Settings;
using UnityEngine;
using VContainer;

namespace ATG.Spawn
{
    [Serializable]
    public sealed class SpawnServiceCreator
    {
        [SerializeField] private SpawnPointSet spawnPointSet;

        public void Create(IContainerBuilder builder)
        {
            builder.Register<SpawnService>(Lifetime.Singleton)
                .WithParameter(spawnPointSet)
                .AsImplementedInterfaces();
        }
    }
    
    public sealed class SpawnService: ISpawnService, IDisposable
    {
        private readonly IArenaRespawnDelay _config;
        private readonly SpawnPointSet _spawnPoints;
        private readonly Dictionary<ISpawnable, CooldownTimer> _data;

        public SpawnService(SpawnPointSet spawnPoints, IArenaRespawnDelay config)
        {
            _config = config;
            _spawnPoints = spawnPoints;
            
            _data = new Dictionary<ISpawnable, CooldownTimer>();
        }

        public bool GetSpawnTimer(ISpawnable spawnOwner, out CooldownTimer result)
        {
            result = null;
            
            if (_data.TryGetValue(spawnOwner, out var value) == false) return false;
            
            result = value;
            return true;
        }

        public void SpawnInstantly(ISpawnable obj)
        {
            ISpawnPoint selectedSpawnPoint = _spawnPoints.GetRandomPoint();
            obj.Spawn(selectedSpawnPoint.GetRandomPointInRadiusXZ(), selectedSpawnPoint.Rotation);
        }

        public void SpawnAfterDelay(ISpawnable obj)
        {
            if (_data.ContainsKey(obj) == true)
            {
                Debug.LogWarning("spawn require already registered");
                return;
            }

            CooldownTimer timer = new CooldownTimer(_config.RespawnSpan);

            void OnTimerFinished(CooldownTimer t)
            {
                SpawnInstantly(obj);
                _data.Remove(obj);
                t.ClearCallbacks();
            }

            timer.OnTimerFinished += OnTimerFinished;
            
            _data.Add(obj, timer);
            timer.Start();
        }
        
        public void Dispose()
        {
            foreach (var timer in _data.Values)
            {
                timer.Dispose();
                timer.ClearCallbacks();
            }
            _data.Clear();
        }
    }
}