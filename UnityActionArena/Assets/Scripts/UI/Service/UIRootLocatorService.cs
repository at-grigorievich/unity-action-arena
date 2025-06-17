using System;
using System.Collections.Generic;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace ATG.UI.Service
{
    [Serializable]
    public sealed class UIRootLocatorCreator
    {
        [SerializeField] private UiRootSetConfig config;

        public void Create(IContainerBuilder builder)
        {
            builder.Register<UIRootLocatorService>(Lifetime.Scoped)
                .WithParameter<UiRootSetConfig>(config)
                .AsSelf().AsImplementedInterfaces();
        }
    }
    
    public sealed class UIRootLocatorService: IStartable, IDisposable
    {
        private readonly IObjectResolver _resolver;
        
        private readonly Dictionary<UiTag, RootUIView> _assets;
        private readonly Dictionary<UiTag, RootUIView> _instances;

        public UIRootLocatorService(IObjectResolver resolver, UiRootSetConfig config)
        {
            _resolver = resolver;
            _assets = new Dictionary<UiTag, RootUIView>(config.Set);
            _instances = new Dictionary<UiTag, RootUIView>();
        }

        public void Start()
        {
            var alreadyInstanced = GameObject.FindObjectsOfType<RootUIView>();

            foreach (var view in alreadyInstanced)
            {
                _instances.TryAdd(view.Tag, view);

                view.Initialize(_resolver);
            }
        }
        
        public bool TryShowView(UiTag tag)
        {
            if (_instances.TryGetValue(tag, out var instance) == true)
            {
                instance.Show();
                return true;
            }

            if (TryInstantiateFromAssets(tag) == true)
            {
                _instances[tag].Show();
                return true;
            }

            Debug.LogWarning($"No UI Root View found for tag {tag}");
            return false;
        }

        public void TryHideView(UiTag tag)
        {
            if(_instances.TryGetValue(tag, out var instance) == false) return;
            instance.Hide();
        }

        public bool TryGetView<T>(UiTag tag, out T view) where T : RootUIView
        {
            if (_instances.TryGetValue(tag, out var instance) == true)
            {
                view = (T)instance;
                return true;
            }
            
            if (TryInstantiateFromAssets(tag) == true)
            {
                view = (T)_instances[tag];
                return true;
            }

            view = null;
            return false;
        }
        
        public void Dispose()
        {
            foreach (var view in _instances.Values)
            {
                view.Dispose();
            }
        }

        private bool TryInstantiateFromAssets(UiTag tag)
        {
            if(_assets.TryGetValue(tag, out var asset) == false) return false;

            var instance = GameObject.Instantiate(asset);
            
            instance.Initialize(_resolver);
            
            _instances.Add(tag, instance);

            return true;
        }
    }
}