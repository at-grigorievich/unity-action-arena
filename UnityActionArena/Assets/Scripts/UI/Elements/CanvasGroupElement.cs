using UnityEngine;

namespace ATG.UI
{
    [RequireComponent(typeof(CanvasGroup))]
    public class CanvasGroupElement<T>: UIElement<T>
    {
        private CanvasGroup canvasGroup;

        protected override void Awake()
        {
            base.Awake();
            canvasGroup = GetComponent<CanvasGroup>();
        }

        public override void Show(object sender, T data)
        {
            ChangeAlpha(1f);
        }
        
        public override void Hide()
        {
            ChangeAlpha(0f);
        }
        
        private void ChangeAlpha(float alpha) => canvasGroup.alpha = alpha;
    }
}