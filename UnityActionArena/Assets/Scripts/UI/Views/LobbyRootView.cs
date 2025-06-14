using ATG.User;
using UnityEngine;
using VContainer;

namespace ATG.UI
{
    public sealed class LobbyRootView: RootUIView
    {
        private const int _startedIndex = 0;
        private const int _maxIndex = 2;

        [SerializeField] private LobbyMainView mainView;
        [SerializeField] private LobbyShopView shopView;
        [Space(5)]
        [SerializeField] private ViewCarouselFactory carouselFactory;
        
        private ViewCarousel _carousel;

        private UserPresenter _user;

        private int _currentIndex;
        
        public override UiTag Tag => UiTag.Lobby;

        [Inject]
        public override void Initialize(IObjectResolver resolver)
        {
            if(resolver.TryResolve(out _user) == false)
                throw new VContainerException(typeof(UserPresenter),"Failed to resolve user presenter");
            
            _carousel = carouselFactory.Create();
            
            base.Initialize(resolver);
        }

        public override void Show()
        {
            base.Show();
            
            _currentIndex = _startedIndex;
            _carousel.MoveTo(_currentIndex);
        }

        public override void Hide()
        {
            base.Hide();
            _carousel.Dispose();
            mainView.Hide();
            shopView.Hide();
        }

        public override void Dispose()
        {
            base.Dispose();
            _carousel.Dispose();
            mainView.Dispose();
            shopView.Dispose();
        }

        [ContextMenu("Move Next")]
        public void SlideToNext()
        {
            if(_carousel.InTransition == true) return;
            
            HideSlide();
            _currentIndex = (_currentIndex + 1) % _maxIndex;
            
            ShowSlide();
            _carousel.MoveTo(_currentIndex);
        }
        
        [ContextMenu("Move Previous")]
        public void SlideToPrevious()
        {
            if(_carousel.InTransition == true) return;
            
            HideSlide();
            _currentIndex = (_currentIndex - 1 + _maxIndex) % _maxIndex;
            
            ShowSlide();
            _carousel.MoveTo(_currentIndex);
        }
        

        private void ShowSlide()
        {
            switch (_currentIndex)
            {
                case 0:
                    mainView.Show(this, new LobbyMainViewData(
                        _user.Id.ToString(), _user.Name, _user.Currency));
                    break;
                case 1:
                    shopView.Show(this, transform);
                    break;
            }
        }
        
        private void HideSlide()
        {
            switch (_currentIndex)
            {
                case 0:
                    mainView.Hide();
                    break;
                case 1:
                    shopView.Hide();
                    break;
            }
        }
    }
}