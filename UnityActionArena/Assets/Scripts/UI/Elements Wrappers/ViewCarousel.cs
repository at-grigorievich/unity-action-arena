using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace ATG.UI
{
    [Serializable]
    public sealed class ViewCarouselFactory
    {
        [SerializeField] private RectTransform carouselRoot;
        [SerializeField] private float elementWidth;
        [SerializeField] private float transitionDuration;

        public ViewCarousel Create()
        {
            return new ViewCarousel(carouselRoot, transitionDuration, elementWidth);
        }
    }
    
    public class ViewCarousel: IDisposable
    {
        private readonly RectTransform _carouselElement;
        private readonly float _transitionDuration;
        private readonly float _width;

        public event Action OnTransitionCompleted;

        public bool InTransition { get; private set; } = false;
        
        private CancellationTokenSource _cts;
        
        public ViewCarousel(RectTransform carouselElement, float transitionDuration, float width)
        {
            _carouselElement = carouselElement;
            _transitionDuration = transitionDuration;
            _width = width;
        }

        public void MoveTo(int index)
        {
            Dispose();
            
            _cts = new CancellationTokenSource();
            TransitionTo(index, _cts.Token).Forget();
        }
        
        public void Dispose()
        {
            InTransition = false;
            
            _cts?.Cancel();
            _cts?.Dispose();
            _cts = null;
        }

        private async UniTask TransitionTo(int index, CancellationToken token)
        {
            float elapsed = 0f;
            
            float startX = _carouselElement.anchoredPosition.x;
            float endX = -1f * (index * _width);

            Vector2 position = _carouselElement.anchoredPosition;
            
            InTransition = true;
            
            while (elapsed < _transitionDuration)
            {
                elapsed += Time.deltaTime;
                float t = Mathf.Clamp01(elapsed / _transitionDuration);
                float newX = Mathf.Lerp(startX, endX, t);
                
                position.x = newX;
                
                _carouselElement.anchoredPosition = position;
                
                await UniTask.Yield(token);
            }

            position.x = endX;
            _carouselElement.anchoredPosition = position;
            
            InTransition = false;
            OnTransitionCompleted?.Invoke();
        }
    }
}