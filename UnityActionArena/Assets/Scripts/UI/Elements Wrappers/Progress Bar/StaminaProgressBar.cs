using System;
using System.Threading;
using ATG.Observable;
using ATG.Stamina;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace ATG.UI
{
    public class StaminaProgressBar: IDisposable
    {
        private readonly bool _withAnimation;
        private readonly ProgressBar _bar;
        private readonly StaminaProgressBarNotEnoughAnimation _notEnoughAnimation;
        
        private IStaminaRate _staminaRate;
        
        private ObserveDisposable _dis;
        
        public StaminaProgressBar(ProgressBar bar, bool withAnimation)
        {
            _bar = bar;
            _withAnimation = withAnimation;
            _notEnoughAnimation = new();
        }
        
        public void Show(IStaminaRate staminaRate)
        {
            _staminaRate = staminaRate;
            
            _bar.Show(this, new ProgressBarRate(_staminaRate.Rate, _withAnimation));
            _bar.SetAlpha(1f);
            
            _dis = staminaRate.Current.Subscribe(OnStaminaChanged);
            
            _notEnoughAnimation.AnimateIfNotEnough(_staminaRate.IsEnough, _bar);
        }

        public void Dispose()
        {
            _dis?.Dispose();
            _dis = null;

            _staminaRate = null;
            
            _notEnoughAnimation.Dispose();
        }
        
        private void OnStaminaChanged(float _)
        {
            _bar.SetData(new ProgressBarRate(_staminaRate.Rate, _withAnimation));
            _notEnoughAnimation.AnimateIfNotEnough(_staminaRate.IsEnough, _bar);
        }
        
        private class StaminaProgressBarNotEnoughAnimation: IDisposable
        {
            private readonly float _blinkDuration = 0.5f;
            private CancellationTokenSource _cts;
            
            public void AnimateIfNotEnough(bool isEnough, ProgressBar progressBar)
            {
                if(isEnough == false && _cts != null) return;
                
                if (isEnough == true && _cts != null)
                {
                    Reset(progressBar);
                    return;
                }

                if (isEnough == false && _cts == null)
                {
                    _cts = new CancellationTokenSource();
                    AnimateNotEnough(progressBar, _cts.Token).Forget();
                }
            }

            public void Reset(ProgressBar progressBar)
            {
                Dispose();
                progressBar.SetAlpha(1f);
            }
            
            public void Dispose()
            {
                _cts?.Cancel();
                _cts?.Dispose();
                _cts = null;
            }

            private async UniTask AnimateNotEnough(ProgressBar progressBar, CancellationToken token)
            {
                while (true)
                {
                    if(token.IsCancellationRequested == true) break;
                    
                    await FadeAsync(progressBar, 1f, 0f, token);
                    await FadeAsync(progressBar, 0f, 1f, token);
                }
            }
            
            private async UniTask FadeAsync(ProgressBar progressBar, float from, float to, CancellationToken token)
            {
                float elapsed = 0f;
                float alpha = from;
                
                if (_blinkDuration <= 0f)
                {
                    progressBar.SetAlpha(alpha);
                    return;
                }

                while (elapsed < _blinkDuration)
                {
                    if(token.IsCancellationRequested == true) return;

                    elapsed += Time.deltaTime;
                    float t = Mathf.Clamp01(elapsed / _blinkDuration);
                    progressBar.SetAlpha(Mathf.Lerp(from, to, t));

                    await UniTask.Yield(PlayerLoopTiming.Update, cancellationToken: token);
                }

                progressBar.SetAlpha(to);
            }
        }
    }
}