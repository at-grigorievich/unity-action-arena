using System;
using ATG.Character;
using ATG.Spawn;
using VContainer.Unity;

namespace ATG.UI.Service
{
    public sealed class ArenaUIObserver: IInitializable, IDisposable
    {
        private readonly ISpawnable _player;
        private readonly ISpawnService _spawnService;
        
        private readonly UIRootCommand _openArenaPlayUI;
        private readonly UIRootCommand _openArenaRespawnUI;

        public ArenaUIObserver(PlayerPresenter player, ISpawnService spawnService, UIRootLocatorService locator)
        {
            _player = player;
            _spawnService = spawnService;
            
            _openArenaPlayUI = new ShowPlayArenaCommand(locator);
            _openArenaRespawnUI = new ShowRespawnArenaCommand(locator);
        }

        public void Initialize()
        {
            _spawnService.OnStartSpawned += StartSpawn;
            _spawnService.OnFinishSpawned += FinishSpawn;
        }

        public void Dispose()
        {
            _spawnService.OnStartSpawned -= StartSpawn;
            _spawnService.OnFinishSpawned -= FinishSpawn;
            
            _openArenaPlayUI?.Dispose();
            _openArenaRespawnUI?.Dispose();
        }

        private void StartSpawn(ISpawnable spawnable)
        {
            if(ReferenceEquals(spawnable, _player) == false) return;
            _openArenaRespawnUI.Execute();
        }

        private void FinishSpawn(ISpawnable spawnable)
        {
            if(ReferenceEquals(spawnable, _player) == false) return;
            _openArenaPlayUI.Execute();
        }
    }
}