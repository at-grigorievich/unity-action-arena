using System;
using UnityEngine;
using IObjectResolver = VContainer.IObjectResolver;

namespace ATG.UI
{
    public sealed class SettingsRootView: RootUIView
    {
        [SerializeField] private EventButton toLobbyButton;

        public event Action OnLobbyButtonPressed;

        public override UiTag Tag => UiTag.Settings;

        public override void Initialize(IObjectResolver resolver)
        {
            base.Initialize(resolver);
            _canvas.sortingOrder = 9999;
        }

        public override void Show()
        {
            base.Show();
            toLobbyButton.OnClicked += OnLobbyButtonClicked;
        }

        public override void Hide()
        {
            base.Hide();
            toLobbyButton.OnClicked -= OnLobbyButtonClicked;
        }

        public override void Dispose()
        {
            base.Dispose();
            toLobbyButton.OnClicked -= OnLobbyButtonClicked;
        }

        private void OnLobbyButtonClicked()
        {
            OnLobbyButtonPressed?.Invoke();
        }
    }
}