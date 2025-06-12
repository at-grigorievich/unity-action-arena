using System;
using ATG.Character;
using ATG.KillCounter;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;
using VContainer;

using IObjectResolver = VContainer.IObjectResolver;

namespace ATG.UI
{
    public class PlayRootView: RootUIView, IDisposable
    {
        [SerializeField] private TMP_Text playerNickOutput;
        [SerializeField] private CounterOutput killCountOutput;
        [SerializeField] private PlayerRateView playerRateView;

        private PlayerPresenter _player;
        private PlayerKillCounterOutput _playerKillCounter;
        
        public override UiTag Tag => UiTag.ArenaPlay;
        
        [Inject]
        public override void Initialize(IObjectResolver resolver)
        {
            if(resolver.TryResolve(out _player) == false)
                throw new VContainerException(typeof(PlayerPresenter),"Failed to resolve player presenter");
            
            if(resolver.TryResolve(out IKillCounter killCounter) == false)
                throw new VContainerException(typeof(PlayerPresenter),"Failed to resolve IKillCounter");
            
            playerNickOutput.text = _player.Nick;
            _playerKillCounter = new PlayerKillCounterOutput(_player.Nick, killCountOutput, killCounter);
            base.Initialize(resolver);
        }

        public override void Show()
        {
            base.Show();
            
            playerRateView.Show(this, new PlayerRateUIData(_player.HealthRate, _player.StaminaRate));
            _playerKillCounter.Show();
        }

        public override void Hide()
        {
            base.Hide();
            
            playerRateView.Hide();
            _playerKillCounter.Dispose();
        }

        public void Dispose()
        {
            playerRateView.Dispose();
            _playerKillCounter.Dispose();
        }
    }
}