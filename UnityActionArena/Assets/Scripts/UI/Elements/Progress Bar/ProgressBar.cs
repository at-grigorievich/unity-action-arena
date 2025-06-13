using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.UI;

namespace ATG.UI
{
    public readonly struct ProgressBarRate
    {
        public readonly float Rate;
        public readonly bool WithAnimation;
        
        public ProgressBarRate(float curValue, float maxValue, bool withAnimation = false)
        {
            if(maxValue <= 0f)
                throw new ArgumentException("maxValue must be greater than zero");
            
            Rate = Mathf.Clamp01(curValue / maxValue);
            WithAnimation = withAnimation;
        }

        public ProgressBarRate(float rate, bool withAnimation = false)
        {
            if (rate < 0f || rate > 1f)
            {
                Debug.LogWarning($"Rate is {rate}");
            }
            
            Rate = rate;
            WithAnimation = withAnimation;
        }
    }
    
    public class ProgressBar: CanvasGroupElement<ProgressBarRate>, IDisposable
    {
        [SerializeField] private Graphic progressImg;
        [SerializeField] private RectTransform progress;
        [SerializeField] private float animationSpeed;
        
        private float _fullRateWidth;
        
        private CancellationTokenSource _cts;
        
        protected override void Awake()
        {
            base.Awake();
            
            _fullRateWidth = Size.x - 2 * progress.anchoredPosition.x;
        }

        private void OnDestroy()
        {
            _cts?.Cancel();
            _cts?.Dispose();
            _cts = null;
        }

        public override void Show(object sender, ProgressBarRate data)
        {
            base.Show(sender, data);
            SetData(data);
        }

        public override void Hide()
        {
            base.Hide();
            Dispose();
        }

        public override void SetData(ProgressBarRate data)
        {
            base.SetData(data);
            
            Dispose();

            if (data.WithAnimation == true)
            {
                _cts = new CancellationTokenSource();
                UpdateRateAsync(data.Rate, _cts.Token).Forget();
            }
            else
            {
                UpdateRate(data.Rate);
            }
        }

        public void SetAlpha(float alpha)
        {
            Color color = progressImg.color;
            color.a = alpha;
            
            progressImg.color = color;
        }
        
        public void Dispose()
        {
            _cts?.Cancel();
            _cts?.Dispose();
            _cts = null;
        }

        private void UpdateRate(float rate)
        {
            float nextWidth = GetWidthByRate(rate);

            Vector2 currentSizeDelta = progress.sizeDelta;
            currentSizeDelta.x = nextWidth;
            
            progress.sizeDelta = currentSizeDelta;
        }

        private async UniTask UpdateRateAsync(float rate, CancellationToken token)
        {
            float nextWidth = GetWidthByRate(rate);
            
            Vector2 currentSizeDelta = progress.sizeDelta;
            float currentX = currentSizeDelta.x;
            
            while (Mathf.Abs(currentX - nextWidth) > 1f)
            {
                currentX = Mathf.Lerp(currentX, nextWidth, animationSpeed * Time.deltaTime);
                
                currentSizeDelta.x = currentX;
                progress.sizeDelta = currentSizeDelta;
                
                await UniTask.Yield(cancellationToken: token);
            }
            
            currentSizeDelta.x = nextWidth;
            progress.sizeDelta = currentSizeDelta;
        }

        private float GetWidthByRate(float rate)
        {
            if(_fullRateWidth == 0) 
                throw new Exception("element width is zero");
            
            return _fullRateWidth * rate;
        }


        [Button("Update Rate")]
        public void UpdateRateDebug(float rate) => UpdateRate(rate);
    }
}