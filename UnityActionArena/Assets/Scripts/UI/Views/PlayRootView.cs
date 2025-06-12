using ATG.Character;
using UnityEngine;
using VContainer;

using IObjectResolver = VContainer.IObjectResolver;

namespace ATG.UI
{
    public class PlayRootView: RootUIView
    {
        [SerializeField] private PlayerRateView playerRateView;

        private PlayerPresenter _player;
        
        public override UiTag Tag => UiTag.ArenaPlay;
        
        [Inject]
        public override void Initialize(IObjectResolver resolver)
        {
            if(resolver.TryResolve(out _player) == false)
                throw new VContainerException(typeof(PlayerPresenter),"Failed to resolve player presenter");
            
            base.Initialize(resolver);
        }

        public override void Show()
        {
            base.Show();
            
            playerRateView.Show(this, new PlayerRateUIData(_player.HealthRate, _player.StaminaRate));
        }

        public override void Hide()
        {
            base.Hide();
            
            playerRateView.Hide();
        }
    }
}