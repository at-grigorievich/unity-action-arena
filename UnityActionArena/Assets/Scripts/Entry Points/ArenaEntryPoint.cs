using ATG.Character;
using ATG.Items.Equipment;
using ATG.Spawn;
using VContainer.Unity;

namespace Entry_Points
{
    public sealed class ArenaEntryPoint: IPostInitializable, IStartable
    {
        private readonly StaticEquipmentSource _equipmentSrc;
        
        private readonly PlayerPresenter _player;
        private readonly BotPool _botPool;
        
        private readonly ISpawnService _spawnService;
        
        public ArenaEntryPoint(PlayerPresenter player, BotPool botPool, StaticEquipmentSource equipmentSrc,
            ISpawnService spawnService)
        {
            _player = player;
            _botPool = botPool;
            
            _equipmentSrc = equipmentSrc;
            _spawnService = spawnService;
        }
        
        public void PostInitialize()
        {
            _player.SetActive(false);
            
            PlayerFirstSpawn();
            BotPoolFirstSpawn();
        }
        
        public void Start()
        {
            _player.SetActive(true);
            foreach (var bot in _botPool.Set)
            {
                bot.SetActive(true);
            }
        }

        private void PlayerFirstSpawn()
        {
            _player.TakeOnEquipments(_equipmentSrc.GetItems());
            _spawnService.SpawnInstantly(_player);
        }

        private void BotPoolFirstSpawn()
        {
            foreach (var bot in _botPool.Set)
            {
                _spawnService.SpawnInstantly(bot);
            }
        }
    }
}