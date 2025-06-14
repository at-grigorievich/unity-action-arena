using System;
using UnityEngine;
using UnityEngine.EventSystems;

namespace ATG.UI
{
    [RequireComponent(typeof(RectTransform), typeof(CanvasGroup))]
    public class ScaledButton<T>: UIElement<T>, IPointerDownHandler, IPointerUpHandler
    {
        private const float _scaleSpeed = 1.5f;
        private readonly Vector3 _minScale = new Vector3(0.95f, 0.95f, 0.95f);
        private readonly Vector3 _maxScale = new Vector3(1f, 1f, 1f);

        private RectTransform _rectTransform;
        private CanvasGroup _canvasGroup;
        private bool _isPressed;
        
        public event Action OnClicked;
        
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
            Vector3 currentScale = _rectTransform.localScale;
            if (_isPressed == false)
            {
                _rectTransform.localScale = Vector3.MoveTowards(currentScale, _maxScale, _scaleSpeed * Time.deltaTime);
            }
            else
            {
                _rectTransform.localScale = Vector3.MoveTowards(currentScale, _minScale, _scaleSpeed * Time.deltaTime);
            }
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            _isPressed = true;
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            if (_isPressed == true)
            {
                OnClicked?.Invoke();
            }
            _isPressed = false;
        }

        public override void Show(object sender, T data)
        {
            DoReset();
            
            _canvasGroup.alpha = 1f;
            _canvasGroup.interactable = true;
        }

        public override void Hide()
        {
            _canvasGroup.alpha = 1f;
            _canvasGroup.interactable = true;
            _isPressed = false;
        }

        private void DoReset()
        {
            _isPressed = false;
            _rectTransform.localScale = _maxScale;
        }
    }
}