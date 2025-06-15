using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace ATG.UI
{
    public readonly struct StatProgressBarRate
    {
        public readonly string StatName;
        public readonly float CurrentValue;
        public readonly float MaxValue;
        public readonly bool WithAnimation;

        public StatProgressBarRate(string statName, float currentValue, float maxValue, bool withAnimation)
        {
            StatName = statName;
            CurrentValue = currentValue;
            MaxValue = maxValue;
            WithAnimation = withAnimation;
        }
        
        public ProgressBarRate GetProgressBarRate() =>
            new ProgressBarRate(CurrentValue, MaxValue, WithAnimation);
    }
    
    public class StatProgressBar: UIElement<StatProgressBarRate>, IDisposable
    {
        [SerializeField] private TMP_Text statNameOutput;
        [SerializeField] private TMP_Text statValueOutput;
        [SerializeField] private ProgressBar progressBar;
        
        public override void Show(object sender, StatProgressBarRate data)
        {
            Dispose();
            
            statNameOutput.text = data.StatName;
            statValueOutput.text = data.CurrentValue.ToString();
            progressBar.Show(this, data.GetProgressBarRate());

            gameObject.SetActive(true);
        }

        public override void Hide()
        {
            progressBar.Hide();
            gameObject.SetActive(false);
        }

        public void Dispose()
        {
            progressBar?.Dispose();
        }
    }
}