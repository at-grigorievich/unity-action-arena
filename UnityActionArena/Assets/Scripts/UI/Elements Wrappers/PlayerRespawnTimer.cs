using System.Threading;
using ATG.DateTimers;
using Cysharp.Threading.Tasks;
using TMPro;
using UnityEngine;

namespace ATG.UI
{
    public sealed class PlayerRespawnTimer
    {
        private readonly FillRoundedProgressBar _progressBar;
        private readonly TMP_Text _timerOutput;
            
        private CooldownTimer _respawnTimer;

        private CancellationTokenSource _cts;
        
        public PlayerRespawnTimer(FillRoundedProgressBar progressBar, TMP_Text timerOutput)
        {
            _progressBar = progressBar;
            _timerOutput = timerOutput;
        }

        public void Show(CooldownTimer respawnTimer)
        {
            _cts = new CancellationTokenSource();
            
            _respawnTimer = respawnTimer;
            _respawnTimer.OnTimerChanged += OnTimerChangedTextOutput;
            
            OnTimerRateChanged(_cts.Token).Forget();
        }

        public void Hide()
        {
            Dispose();
            _progressBar.Hide();
        }
        
        public void Dispose()
        {
            _cts?.Cancel();
            _cts?.Dispose();
            _cts = null;
            
            if (_respawnTimer != null)
            {
                _respawnTimer.OnTimerChanged -= OnTimerChangedTextOutput;
                _respawnTimer = null;
            }
        }

        private async UniTask OnTimerRateChanged(CancellationToken token)
        {
            while (true)
            {
                float total = (float)_respawnTimer.CooldownTime.TotalSeconds;
                float timeLeft = (float)_respawnTimer.TimeLeft.TotalSeconds;
                
                float rate = 1f - (timeLeft / total);
                
                _progressBar.Show(this, rate);
                await UniTask.Yield(token);
            }
        }
        
        private void OnTimerChangedTextOutput(CooldownTimerInfo obj)
        {
            var secondsLeft = Mathf.CeilToInt((float)obj.TimeLeft.TotalSeconds);
            _timerOutput.text = $"{secondsLeft} seconds";
        }
    }
}