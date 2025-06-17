using System;
using ATG.Input;
using ATG.Pause;
using ATG.SceneManagement;
using UnityEngine;
using VContainer.Unity;

namespace ATG.UI.Service
{
    public sealed class SettingsUIObserver: IInitializable, ITickable, IDisposable
    {
        private readonly InputKeyDownObserver _escapeKeyObserver;
        private readonly SceneLoader _lobbyLoader;

        private readonly IPauseService _pauseService;
        private readonly UIRootLocatorService _uiLocator;

        private SettingsRootView _settingsView;
        private bool _alreadyOpenned;
        
        public SettingsUIObserver(SceneLoader lobbyLoader, UIRootLocatorService uiLocator,
            IPauseService pauseService)
        {
            _lobbyLoader = lobbyLoader;
            _uiLocator = uiLocator;
            _pauseService = pauseService;

            _escapeKeyObserver = new InputKeyDownObserver(KeyCode.Escape);
        }
        
        public void Initialize()
        {
            if(_uiLocator.TryGetView(UiTag.Settings, out _settingsView) == false)
                throw new NullReferenceException("No settings view found");
            
            
            _settingsView.OnLobbyButtonPressed += LoadLobbyScene;
            _escapeKeyObserver.OnClicked += OnClicked;
        }

        public void Tick()
        {
            _escapeKeyObserver.Tick();
        }
        
        public void Dispose()
        {
            _settingsView.OnLobbyButtonPressed -= LoadLobbyScene;
            _escapeKeyObserver.OnClicked -= OnClicked;
        }
        
        private void OnClicked()
        {
            if (_alreadyOpenned == true)
            {
                _pauseService.SetPauseStatus(false);
                _uiLocator.TryHideView(UiTag.Settings);
            }
            else
            {
                _pauseService.SetPauseStatus(true);
                _uiLocator.TryShowView(UiTag.Settings);
            }

            _alreadyOpenned = !_alreadyOpenned;
        }
        
        private void LoadLobbyScene()
        {
            Dispose();
            _lobbyLoader.Load();
        }
    }
}