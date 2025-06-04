using System;
using System.Collections.Generic;
using ATG.Animator;
using ATG.Attack;
using ATG.Health;
using ATG.Items;
using ATG.Items.Equipment;
using ATG.Move;
using ATG.Spawn;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace ATG.Character
{
    [Serializable]
    public abstract class CharacterCreator<T> where T : CharacterPresenter
    {
        [SerializeField] protected CharacterView view;

        public virtual void Create(IContainerBuilder builder)
        {
            builder.Register<T>(Lifetime.Singleton)
                .WithParameter<CharacterView>(view)
                .AsSelf()
                .AsImplementedInterfaces();
        }
    }
    
    [Serializable] public sealed class LobbyCharacterCreator: CharacterCreator<LobbyCharacterPresenter> {}
    [Serializable] public sealed class PlayerCharacterCreator: CharacterCreator<PlayerPresenter> {}
    [Serializable] public sealed class BotCharacterCreator: CharacterCreator<BotPresenter> {}
    
    public abstract class CharacterPresenter: IInitializable, IDisposable, ISpawnable
    {
        protected readonly CharacterView _view;
        protected readonly CharacterModel _characterModel;
        
        protected readonly Equipment _equipment;
        
        private readonly IEquipmentObserver _equipmentModelObserver;
        private readonly IEquipmentObserver _equipmentViewObserver;
        
        protected readonly IMoveableService _moveService;
        protected readonly IAnimatorWrapper _animator;
        protected readonly IAttackService _attackService;
        
        protected bool _isActive;
        
        protected CharacterPresenter(RaycastPool raycastPool, CharacterView view)
        {
            _view = view;
            _characterModel = new CharacterModel(100, 100, 1, 3, 1);
            
            _equipment = new Equipment();
            _equipmentModelObserver = new CharacterEquipEffectObserver(_equipment, _characterModel);
            _equipmentViewObserver = new CharacterEquipViewObserver(_equipment, _view);

            _moveService = new NavMeshMoveService(_view.NavAgent, _characterModel.Speed);

            _attackService = _view.AttackServiceCreator.Create(raycastPool, _characterModel.Range);
            _animator = _view.AnimatorWrapperCreator.Create();
        }
        
        public virtual void Initialize()
        {
            _view.Initialize(this);
            
            _equipmentModelObserver.Initialize();
            _equipmentViewObserver.Initialize();
        }
        
        public virtual void Dispose()
        {
            _equipmentModelObserver.Dispose();
            _equipmentViewObserver.Dispose();
        }

        public virtual void SetActive(bool isActive)
        {
            _isActive = isActive;
            
            _moveService.Stop();
            
            _animator.SetActive(_isActive);
            
            SetVisible(isActive);
            SetPhysActive(isActive);
        }

        public void SetVisible(bool isVisible) => _view.SetVisible(isVisible);
        public void SetPhysActive(bool isActive) => _view.SetPhysActive(isActive);
        
        public virtual void Spawn(Vector3 spawnPosition, Quaternion spawnRotation)
        {
            _moveService.PlaceTo(spawnPosition, spawnRotation);
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
