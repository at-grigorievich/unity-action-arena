using System;
using ATG.Character;
using ATG.Spawn;
using VContainer.Unity;

namespace ATG.UI.Service
{
    public sealed class ArenaUIObserver: IInitializable, IDisposable
    {
        private readonly ISpawnable _player;

        private readonly UIRootCommand _openArenaPlayUI;
        private readonly UIRootCommand _openArenaRespawnUI;

        public ArenaUIObserver(PlayerPresenter player, UIRootLocatorService locator)
        {
            _player = player;
            _openArenaPlayUI = new ShowPlayArenaCommand(locator);
            _openArenaRespawnUI = new ShowRespawnArenaCommand(locator);
        }

        public void Initialize()
        {
            _player.OnSpawned += OnPlayerSpawnedNow;
            _player.OnSpawnRequired += OnPlayerSpawnRequired;
        }

        public void Dispose()
        {
            _player.OnSpawned -= OnPlayerSpawnedNow;
            _player.OnSpawnRequired -= OnPlayerSpawnRequired;
            
            _openArenaPlayUI?.Dispose();
            _openArenaRespawnUI?.Dispose();
        }
        
        private void OnPlayerSpawnedNow() => _openArenaPlayUI.Execute();
        private void OnPlayerSpawnRequired(ISpawnable _) => _openArenaRespawnUI.Execute();
    }
}