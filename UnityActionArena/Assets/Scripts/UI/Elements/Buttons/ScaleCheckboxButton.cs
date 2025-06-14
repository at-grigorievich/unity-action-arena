using System;
using UnityEngine;
using UnityEngine.EventSystems;

namespace ATG.UI
{
    [RequireComponent(typeof(RectTransform), typeof(CanvasGroup))]
    public class ScaleCheckboxButton<T>: UIElement<T>, IPointerDownHandler
    {
        private const float _scaleSpeed = 2.5f;
        
        [SerializeField] private RectTransform scaledTarget;
        [SerializeField] private Vector3 selectedScale;

        private RectTransform _rectTransform;
        private CanvasGroup _canvasGroup;
        private bool _isPressed;
        
        public T Data { get; private set; }
        
        public event Action<ScaleCheckboxButton<T>> OnSelected; 
        
        protected override void Awake()
        {
            base.Awake();
            _rectTransform = GetComponent<RectTransform>();
            _canvasGroup = GetComponent<CanvasGroup>();
            _canvasGroup.blocksRaycasts = true;
            _canvasGroup.ignoreParentGroups = true;
        }

        private void Update()
        {
            Vector3 currentScale = scaledTarget.localScale;
            if (_isPressed == false)
            {
                scaledTarget.localScale = Vector3.MoveTowards(currentScale, Vector3.one, _scaleSpeed * Time.deltaTime);
            }
            else
            {
                scaledTarget.localScale = Vector3.MoveTowards(currentScale, selectedScale, _scaleSpeed * Time.deltaTime);
            }
        }
        
        public override void Show(object sender, T data)
        {
            _isPressed = false;
            Data = data;

            scaledTarget.localScale = Vector3.one;
            _canvasGroup.alpha = 1f;
            _canvasGroup.interactable = true;
        }

        public override void Hide()
        {
            _canvasGroup.alpha = 0f;
            _canvasGroup.interactable = false;
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            if(_isPressed == true) return;
            Select();
        }

        public void Select()
        {
            OnSelected?.Invoke(this);
            _isPressed = true;
        }

        public void DoReset()
        {
            _isPressed = false;
        }
    }
}