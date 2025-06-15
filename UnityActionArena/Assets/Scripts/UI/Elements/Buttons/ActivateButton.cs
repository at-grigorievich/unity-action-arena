using UnityEngine;

namespace ATG.UI
{
    public class ActivateButton<T>: ScaledButton<T>
    {
        [SerializeField] private UISkinSwitcher activeSkin;
        [SerializeField] private UISkinSwitcher disactiveSkin;

        public void Activate()
        {
            _canvasGroup.blocksRaycasts = true;
            activeSkin.Select();
        }

        public void Deactivate()
        {
            _canvasGroup.blocksRaycasts = false;
            disactiveSkin.Select();
        }
        
    }
}