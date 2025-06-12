using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using Sirenix.OdinInspector;
using TMPro;
using UnityEngine;

namespace ATG.UI
{
    public class CounterOutput: UIElement<string>, IDisposable
    {
        [SerializeField] private TMP_Text output;
        [SerializeField] private bool withBounceEffect;
        [SerializeField, ShowIf("withBounceEffect")] private float bounceDuration;
        [SerializeField, ShowIf("withBounceEffect")] private Vector3 minBounceScale, maxBounceScale;

        private CancellationTokenSource _cts;

        private void OnDestroy()
        {
            Dispose();
        }

        public override void Show(object sender, string data)
        {
            output.text = data;

            if (withBounceEffect == true)
            {
                if(_cts != null) return;
                
                _cts = new CancellationTokenSource();
                BounceEffect(_cts.Token).Forget();
            }
        }

        public override void Hide()
        {
            Dispose();
            output.transform.localScale = Vector3.one;
        }

        private async UniTask BounceEffect(CancellationToken token)
        {
            Transform target = output.transform;

            float halfDuration = bounceDuration / 2f;
            float time = 0f;

            // Увеличение масштаба до maxBounceScale
            while (time < halfDuration)
            {
                if (token.IsCancellationRequested) return;

                float t = time / halfDuration;
                target.localScale = Vector3.Lerp(minBounceScale, maxBounceScale, t);
                time += Time.deltaTime;
                await UniTask.Yield(PlayerLoopTiming.Update, token);
            }

            target.localScale = maxBounceScale;

            // Уменьшение обратно до 1 (или minBounceScale, если ты хочешь другой bounce)
            time = 0f;
            while (time < halfDuration)
            {
                if (token.IsCancellationRequested) return;

                float t = time / halfDuration;
                target.localScale = Vector3.Lerp(maxBounceScale, Vector3.one, t);
                time += Time.deltaTime;
                await UniTask.Yield(PlayerLoopTiming.Update, token);
            }

            target.localScale = Vector3.one;
            
            _cts?.Cancel();
            _cts?.Dispose();
            _cts = null;
        }

        public void Dispose()
        {
            _cts?.Cancel();
            _cts?.Dispose();
            _cts = null;
        }
    }
}