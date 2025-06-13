using System;
using ATG.Character;
using ATG.KillCounter;
using TMPro;
using UnityEngine;
using VContainer;

using IObjectResolver = VContainer.IObjectResolver;

namespace ATG.UI
{
    public class PlayRootView: RootUIView, IDisposable
    {
        [SerializeField] private TMP_Text playerNickOutput;
        [SerializeField] private PlayerRateView playerRateView;
        [SerializeField] private PlayerStatisticView playerStatisticView;
        
        private PlayerPresenter _player;
        private IKillCounter _playerKillCounter;
        private IEarnPerKillCounter _playerEarnCounter;
        
        public override UiTag Tag => UiTag.ArenaPlay;
        
        [Inject]
        public override void Initialize(IObjectResolver resolver)
        {
            if(resolver.TryResolve(out _player) == false)
                throw new VContainerException(typeof(PlayerPresenter),"Failed to resolve player presenter");
            
            if(resolver.TryResolve(out _playerKillCounter) == false)
                throw new VContainerException(typeof(IKillCounter),"Failed to resolve IKillCounter");
            
            if(resolver.TryResolve(out _playerEarnCounter) == false)
                throw new VContainerException(typeof(IEarnPerKillCounter),"Failed to resolve IEarnPerKillCounter");
            
            playerNickOutput.text = _player.Nick;
            
            base.Initialize(resolver);
        }

        public override void Show()
        {
            base.Show();
            
            playerRateView.Show(this, new PlayerRateUIData(_player.HealthRate, _player.StaminaRate));
            playerStatisticView.Show(this, 
                new PlayerStatisticUIData(_player.Nick, _playerKillCounter, _playerEarnCounter));
        }

        public override void Hide()
        {
            base.Hide();
            
            playerRateView.Hide();
            playerStatisticView.Hide();
        }

        public override void Dispose()
        {
            playerRateView.Dispose();
            playerStatisticView.Dispose();
        }
    }
}