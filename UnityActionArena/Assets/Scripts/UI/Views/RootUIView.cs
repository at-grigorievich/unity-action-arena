using System;
using UnityEngine;
using VContainer;

using IObjectResolver = VContainer.IObjectResolver;

namespace ATG.UI
{
    [RequireComponent(typeof(Canvas))]
    public abstract class RootUIView: MonoBehaviour
    {
        [SerializeField] private bool showOnInitialize;
        
        private bool _isInitialized;
        
        protected Canvas _canvas;
        
        public abstract UiTag Tag { get; }

        [Inject]
        public virtual void Initialize(IObjectResolver resolver)
        {
            _canvas = GetComponent<Canvas>();
            _isInitialized = true;

            if (showOnInitialize == true)
            {
                Show();
            }
        }
        
        public virtual void Show()
        {
            CheckInitialization();
            _canvas.enabled = true;
        }

        public virtual void Hide()
        {
            CheckInitialization();
            
            _canvas.enabled = false;   
        }

        private void CheckInitialization()
        {
            if(_isInitialized == false)
                throw new Exception($"{transform.name} is not initialized, but try to show");
        }
    }
}