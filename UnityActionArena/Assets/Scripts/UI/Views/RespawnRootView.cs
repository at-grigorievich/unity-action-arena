using System;
using ATG.Character;
using ATG.KillCounter;
using ATG.Spawn;
using TMPro;
using UnityEngine;
using VContainer;

using IObjectResolver = VContainer.IObjectResolver;

namespace ATG.UI
{
    public class RespawnRootView: RootUIView
    {
        [SerializeField] private PlayerStatisticView playerStatisticView;
        [SerializeField] private FillRoundedProgressBar timerProgressBar;
        [SerializeField] private TMP_Text timerOutput;
        
        public override UiTag Tag => UiTag.ArenaRespawn;

        private ISpawnService _spawnService;
        private PlayerPresenter _player;
        
        private IKillCounter _playerKillCounter;
        private IEarnPerKillCounter _playerEarnCounter;
        
        private PlayerRespawnTimer _playerRespawnTimer;

        private void Awake()
        {
            _playerRespawnTimer = new PlayerRespawnTimer(timerProgressBar, timerOutput);
        }

        [Inject]
        public override void Initialize(IObjectResolver resolver)
        {
            if(resolver.TryResolve(out _player) == false)
                throw new VContainerException(typeof(PlayerPresenter),"Failed to resolve player presenter");
            
            if(resolver.TryResolve(out _playerKillCounter) == false)
                throw new VContainerException(typeof(IKillCounter),"Failed to resolve IKillCounter");
            
            if(resolver.TryResolve(out _playerEarnCounter) == false)
                throw new VContainerException(typeof(IEarnPerKillCounter),"Failed to resolve IEarnPerKillCounter");
            
            if(resolver.TryResolve(out _spawnService) == false)
                throw new VContainerException(typeof(ISpawnService),"Failed to resolve ISpawnService");
            
            base.Initialize(resolver);
        }
        
        public override void Show()
        {
            base.Show();
            
            if (_spawnService.GetSpawnTimer(_player, out var spawnTimer) == false)
                throw new Exception("Failed to get spawn timer");
            
            playerStatisticView.Show(this, 
                new PlayerStatisticUIData(_player.Nick, _playerKillCounter, _playerEarnCounter));
            
            _playerRespawnTimer.Show(spawnTimer);
        }
        
        public override void Hide()
        {
            base.Hide();
            
            playerStatisticView.Hide();
            _playerRespawnTimer.Hide();
        }

        public override void Dispose()
        {
            playerStatisticView.Dispose();
            _playerRespawnTimer.Dispose();
        }
    }
}