using System;
using System.Threading;
using ATG.Camera;
using ATG.Character;
using ATG.Items.Equipment;
using ATG.Spawn;
using Cysharp.Threading.Tasks;
using VContainer.Unity;

namespace Entry_Points
{
    public sealed class ArenaEntryPoint: IPostInitializable, IAsyncStartable, IDisposable
    {
        private readonly StaticEquipmentSource _equipmentSrc;
        
        private readonly PlayerPresenter _player;
        private readonly BotPool _botPool;
        
        private readonly ISpawnService _spawnService;
        
        private readonly CancellationTokenSource _cts;
        
        public ArenaEntryPoint(PlayerPresenter player, BotPool botPool, StaticEquipmentSource equipmentSrc,
            ISpawnService spawnService)
        {
            _player = player;
            _botPool = botPool;
            
            _equipmentSrc = equipmentSrc;
            _spawnService = spawnService;
            
            _cts = new CancellationTokenSource();
        }
        
        public void PostInitialize()
        {
            _player.SetActive(false);
            _botPool.SetActiveAll(false);
        }
        
        public async UniTask StartAsync(CancellationToken cancellation = default)
        {
            await UniTaskSpawnAsync();
            ActivatePlayerAndBots();
        }
        
        public void Dispose()
        {
            _cts.Cancel();
            _cts.Dispose();
        }
        
        private async UniTask UniTaskSpawnAsync()
        {
            await PlayerFirstSpawnAsync();
            await BotPoolFirstSpawnAsync();
        }
        
        private async UniTask PlayerFirstSpawnAsync()
        {
            _player.TakeOnEquipments(_equipmentSrc.GetItems());
            _player.SetPhysActive(true);
            
            await UniTask.Yield(cancellationToken: _cts.Token);
            
            _spawnService.SpawnInstantly(_player);
        }

        private async UniTask BotPoolFirstSpawnAsync()
        {
            foreach (var bot in _botPool.Set)
            {
                bot.SetPhysActive(true);
                
                await UniTask.Yield(cancellationToken: _cts.Token);
                
                _spawnService.SpawnInstantly(bot);
            }
        }

        private void ActivatePlayerAndBots()
        {
            _player.SetActive(true);
            _botPool.SetActiveAll(true);
        }
        
    }
}