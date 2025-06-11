using System;
using System.Collections.Generic;
using System.Threading;
using Cysharp.Threading.Tasks;
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
        private readonly int _respawnDelayMillis;
        private readonly SpawnPointSet _spawnPoints;
        private readonly Dictionary<ISpawnable, CancellationTokenSource> _ctsActive;

        public SpawnService(SpawnPointSet spawnPoints, IArenaRespawnDelay respawnDelay)
        {
            _respawnDelayMillis = respawnDelay.CharacterRespawnDelayMs;
            _spawnPoints = spawnPoints;
            
            _ctsActive = new Dictionary<ISpawnable, CancellationTokenSource>();
        }
        
        public void SpawnInstantly(ISpawnable obj)
        {
            ISpawnPoint selectedSpawnPoint = _spawnPoints.GetRandomPoint();
            obj.Spawn(selectedSpawnPoint.GetRandomPointInRadiusXZ(), selectedSpawnPoint.Rotation);
        }

        public void SpawnAfterDelay(ISpawnable obj)
        {
            if (_ctsActive.ContainsKey(obj) == true)
            {
                Debug.LogWarning("spawn require already registered");
                RemoveTokenSourceByKey(obj);
            }

            CancellationTokenSource newCts = new();
            _ctsActive.Add(obj, newCts);

            SpawnAfterDelayAsync(obj, newCts).Forget();
        }

        private async UniTask SpawnAfterDelayAsync(ISpawnable spawnedObj, CancellationTokenSource cts)
        {
            await UniTask.Delay(_respawnDelayMillis, cancellationToken: cts.Token);
            
            SpawnInstantly(spawnedObj);
            RemoveTokenSourceByKey(spawnedObj);
        }
        
        public void Dispose()
        {
            foreach (var token in _ctsActive.Values)
            {
                token?.Cancel();
                token?.Dispose();
            }
            
            _ctsActive.Clear();
        }

        private void RemoveTokenSourceByKey(ISpawnable spawnable)
        {
            if(_ctsActive.TryGetValue(spawnable, out var cts) == false) return;

            cts.Cancel();
            cts.Dispose();
            
            _ctsActive.Remove(spawnable);
        }
    }
}