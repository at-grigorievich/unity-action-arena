using System;
using System.Collections.Generic;
using System.Linq;
using ATG.UI;
using UnityEngine;

namespace ATG.UI.Service
{
    [Serializable]
    public sealed class UIRootPair
    {
        [field: SerializeField] public UiTag Tag { get; private set; }
        [field: SerializeField] public RootUIView ViewAsset { get; private set; }
        
        public KeyValuePair<UiTag, RootUIView> Pair => new KeyValuePair<UiTag, RootUIView>(Tag, ViewAsset);
    }
    
    [CreateAssetMenu(fileName = "UI ROOT Set", menuName = "Configs/New UI Rott Set", order = 0)]
    public sealed class UiRootSetConfig: ScriptableObject
    {
        [SerializeField] private UIRootPair[] set;
        
        public IEnumerable<KeyValuePair<UiTag, RootUIView>> Set => set.Select(e => e.Pair);
    }
}