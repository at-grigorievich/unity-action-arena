using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace ATG.UI
{
	[Serializable]
    public sealed class UISkinSwitcher: MonoBehaviour
    {
        [SerializeField] private List<UISkin> skins;

        public void Select()
        {
            foreach (var uiSkin in skins)
            {
                uiSkin.Setup();
            }
        }
        
        [Serializable]
        private sealed class UISkin
        {
            [SerializeField] private Graphic element;
            [SerializeField] private Color color;

            public void Setup() => element.color = color;
        }
    }
}