using System;
using System.Collections.Generic;
using ATG.Character.Animator;
using ATG.Character.Attack;
using ATG.Character.Health;
using ATG.Character.Move;
using ATG.Items;
using ATG.Items.Equipment;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace ATG.Character
{
    [Serializable]
    public abstract class CharacterCreator<T> where T : CharacterPresenter
    {
        [SerializeField] private CharacterView view;

        public void Create(IContainerBuilder builder)
        {
            builder.Register<T>(Lifetime.Singleton)
                .WithParameter<CharacterView>(view)
                .AsSelf()
                .AsImplementedInterfaces();
        }
    }
    
    [Serializable] public sealed class LobbyCharacterCreator: CharacterCreator<LobbyCharacterPresenter> {}
    [Serializable] public sealed class PlayerCharacterCreator: CharacterCreator<PlayerPresenter> {}
    [Serializable] public sealed class BotCharacterCreator:  CharacterCreator<BotPresenter> {}
    
    public abstract class CharacterPresenter: IInitializable, IDisposable
    {
        protected readonly CharacterView _view;
        protected readonly CharacterModel _characterModel;
        
        protected readonly Equipment _equipment;
        
        protected readonly IEquipmentObserver _equipmentModelObserver;
        protected readonly IEquipmentObserver _equipmentViewObserver;
        
        protected readonly IMoveableService _moveService;
        
        protected CharacterPresenter(CharacterView view)
        {
            _view = view;
            _characterModel = new CharacterModel(100, 100, 1, 3, 1);
            
            _equipment = new Equipment();
            _equipmentModelObserver = new CharacterEquipEffectObserver(_equipment, _characterModel);
            _equipmentViewObserver = new CharacterEquipViewObserver(_equipment, _view);

            _moveService = new NavMeshMoveService(_view.NavAgent, _characterModel.Speed);
        }

        public virtual void Initialize()
        {
            _view.Initialize();
            
            _equipmentModelObserver.Initialize();
            _equipmentViewObserver.Initialize();
        }
        
        public virtual void Dispose()
        {
            _equipmentModelObserver.Dispose();
            _equipmentViewObserver.Dispose();
        }

        public void TakeOnEquipments(IEnumerable<Item> items)
        {
            foreach (var item in items)
            {
                _equipment.TakeOnItem(item);               
            }
        }
    }
}
