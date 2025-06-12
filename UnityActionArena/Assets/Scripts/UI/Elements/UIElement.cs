using UnityEngine;

namespace ATG.UI
{
    [RequireComponent(typeof(RectTransform))]
    public abstract class UIElement<T>: MonoBehaviour
    {
        protected RectTransform _rect;

        public Vector2 AnchoredPosition => _rect.anchoredPosition;
        public Vector2 Size => _rect.rect.size;
        
        protected virtual void Awake()
        {
            _rect = GetComponent<RectTransform>();
        }

        public abstract void Show(object sender, T data);
        public abstract void Hide();
        public virtual void SetData(T data) {}
    }
}