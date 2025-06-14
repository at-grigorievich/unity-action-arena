using System;
using ATG.Animator;
using ATG.Items.Equipment;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace ATG.Character
{
    [Serializable]
    public sealed class LobbyCharacterCreator
    {
        [SerializeField] private CharacterView view;
        [SerializeField] private AnimatorWrapperCreator animatorCreator;

        public void Create(IContainerBuilder builder)
        {
            builder.Register<LobbyCharacterPresenter>(Lifetime.Singleton)
                .WithParameter<CharacterView>(view)
                .WithParameter<IAnimatorWrapper>(animatorCreator.Create())
                .AsSelf().AsImplementedInterfaces();
        }
    }
    
    public sealed class LobbyCharacterPresenter: IInitializable, IDisposable
    {
        private readonly CharacterView _view;
        
        private readonly Equipment _equipment;
     
        private readonly IAnimatorWrapper _animator;
        private readonly IEquipmentObserver _equipmentViewObserver;
        
        public LobbyCharacterPresenter(CharacterView view, IAnimatorWrapper animator)
        {
            _view = view;
            
            _equipment = new Equipment();
            
            _animator = animator;
            _equipmentViewObserver = new CharacterEquipViewObserver(_equipment, _view);
        }

        public void Initialize()
        {
            _equipmentViewObserver.Initialize();
        }

        public void Dispose()
        {
            _equipmentViewObserver.Dispose();
        }
        
        public void TakeOnEquipments(params Items.Item[] items)
        {
            foreach (var item in items)
            {
                _equipment.TakeOnItem(item);               
            }
        }
    }
}