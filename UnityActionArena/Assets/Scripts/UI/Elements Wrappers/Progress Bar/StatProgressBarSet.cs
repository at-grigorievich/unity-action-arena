using System;
using System.Collections.Generic;
using ATG.Items;
using UnityEngine;

namespace ATG.UI
{
    public sealed class StatProgressBarSet: UIElement<Item>, IDisposable
    {
        [SerializeField] private RectTransform setRoot;
        
        private StatProgressBar[] _stats;

        protected override void Awake()
        {
            base.Awake();

            _stats = setRoot.GetComponentsInChildren<StatProgressBar>(includeInactive: true);
        }

        public override void Show(object sender, Item data)
        {
            if (data.TryGetComponents(out IEnumerable<HeroEffectComponent> effects) == false)
                throw new NullReferenceException($"No effect component found on {data.Id}");

            List<HeroEffectComponent> effectList = new List<HeroEffectComponent>(effects);
            
            
            if(effectList.Count > _stats.Length) 
                throw new IndexOutOfRangeException($"There are {effectList.Count} effect components attached to the set");
            
            Dispose();
            
            for (var i = 0; i < _stats.Length; i++)
            {
                if (i >= effectList.Count - 1)
                {
                    _stats[i].Hide();
                    continue;
                }
                
                HeroEffectComponent effect = effectList[i];

                StatProgressBarRate rate = new StatProgressBarRate(effect.EffectName, effect.CurrentValue,
                    HeroEffectComponent.MAX_VALUE, true);
                _stats[i].Show(this, rate);
            }
        }

        public override void Hide()
        {
            foreach (var stat in _stats)
            {
                stat.Hide();
            }
        }

        public void Dispose()
        {
            foreach (var stat in _stats)
            {
                stat.Dispose();
            }
        }
    }
}