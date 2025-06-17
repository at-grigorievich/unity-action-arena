using System;
using ATG.Observable;
using ATG.SceneManagement;
using TMPro;
using UI.Views;
using UnityEngine;

namespace ATG.UI
{
    public readonly struct LobbyMainViewData
    {
        public readonly string Name;
        public readonly string Id;

        public readonly SceneLoader SceneLoader;
        
        public readonly IReadOnlyObservableVar<int> Currency;

        public LobbyMainViewData(string id, string name, SceneLoader sceneLoader, IReadOnlyObservableVar<int> currency)
        {
            Name = name;
            Id = id;
            SceneLoader = sceneLoader;
            Currency = currency;
        }
    }
    
    public sealed class LobbyMainView: UIView<LobbyMainViewData>, IDisposable
    {
        [SerializeField] private TMP_Text nameOutput;
        [SerializeField] private TMP_Text idOutput;
        [SerializeField] private CounterOutput currencyOutput;
        [SerializeField] private PlayButton playButton;

        private PlayerCurrencyOutput _userCurrency;
        
        private SceneLoader _sceneLoader;
        
        protected override void Awake()
        {
            base.Awake();

            _userCurrency = new PlayerCurrencyOutput(currencyOutput);
        }
        
        public override void Show(object sender, LobbyMainViewData data)
        {
            base.Show(sender, data);
            
            _sceneLoader = data.SceneLoader;
            
            nameOutput.text = data.Name;
            idOutput.text = data.Id;
            _userCurrency.Show(data.Currency);
            
            playButton.Show(this, string.Empty);
            playButton.OnClicked += OnPlayClicked;
        }

        public override void Hide()
        {
            base.Hide();
            _userCurrency.Hide();
            playButton.Hide();
            playButton.OnClicked -= OnPlayClicked;
        }

        public void Dispose()
        {
            _userCurrency.Dispose();
            playButton.OnClicked -= OnPlayClicked;
        }
        
        private void OnPlayClicked()
        {
            if(_sceneLoader == null) return;
            
            _sceneLoader.Load();
            
            playButton.OnClicked -= OnPlayClicked;
            playButton.Hide();
        }
    }
}