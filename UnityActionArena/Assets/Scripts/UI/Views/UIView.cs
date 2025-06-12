using UnityEngine;

namespace UI.Views
{
    [RequireComponent(typeof(Canvas))]
    public abstract class UIView<T>: MonoBehaviour
    {
        protected Canvas _canvas;
        
        protected virtual void Awake()
        {
            _canvas = GetComponent<Canvas>();
        }

        public virtual void Show(object sender, T data)
        {
            _canvas.enabled = true;    
        }

        public virtual void Hide()
        {
            _canvas.enabled = false;
        }
        
        public virtual void SetData(T data) {}
    }
}