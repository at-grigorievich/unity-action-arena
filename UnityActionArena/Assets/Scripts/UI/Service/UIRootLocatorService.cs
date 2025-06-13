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
    
    public sealed class UIRootLocatorService: IInitializable, IDisposable
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

        public void Initialize()
        {
            var alreadyInstanced = GameObject.FindObjectsOfType<RootUIView>();

            foreach (var view in alreadyInstanced)
            {
                if (_instances.ContainsKey(view.Tag) == true)
                {
                    throw new Exception($"UI Root View with Tag {view.Tag} already exists: {_instances[view.Tag].transform.name}");
                    return;
                }
                
                view.Initialize(_resolver);
                _instances.Add(view.Tag, view);
            }
        }

        public bool TryShowView(UiTag tag)
        {
            if (_instances.ContainsKey(tag) == true)
            {
                _instances[tag].Show();
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
            if(_instances.ContainsKey(tag) == false) return;
            _instances[tag].Hide();
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
            if(_assets.ContainsKey(tag) == false) return false;
            
            var asset = _assets[tag];
            var instance = GameObject.Instantiate(asset);
            
            instance.Initialize(_resolver);
            
            _instances.Add(tag, instance);

            return true;
        }
    }
}