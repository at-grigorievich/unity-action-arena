using System;
using System.Collections.Generic;
using ATG.Animator;
using ATG.Items;
using ATG.Items.Equipment;
using ATG.Move;
using ATG.Spawn;
using UnityEngine;
using VContainer.Unity;

namespace ATG.Character
{
    public abstract class CharacterPresenter: IInitializable, IDisposable, ISpawnable
    {
        protected readonly CharacterView _view;
        protected readonly CharacterModel _characterModel;
        
        protected readonly Equipment _equipment;
        
        protected readonly IMoveableService _move;
        protected readonly IAnimatorWrapper _animator;
        
        private readonly IEquipmentObserver _equipmentModelObserver;
        private readonly IEquipmentObserver _equipmentViewObserver;
        
        protected bool _isActive;
        
        protected CharacterPresenter(CharacterView view, CharacterModel model, 
            IAnimatorWrapper animator, IMoveableService move)
        {
            _view = view;
            _characterModel = model;
            
            _equipment = new Equipment();
            _equipmentModelObserver = new CharacterEquipEffectObserver(_equipment, _characterModel);
            _equipmentViewObserver = new CharacterEquipViewObserver(_equipment, _view);
            
            _animator = animator;
            _move = move;
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
            
            _move.Stop();
            
            _animator.SetActive(_isActive);
            
            SetVisible(isActive);
            SetPhysActive(isActive);
        }
        
        public void SetVisible(bool isVisible) => _view.SetVisible(isVisible);
        public void SetPhysActive(bool isActive) => _view.SetPhysActive(isActive);
        
        public virtual void Spawn(Vector3 spawnPosition, Quaternion spawnRotation)
        {
            _move.PlaceTo(spawnPosition, spawnRotation);
        }
        
        public virtual void TakeOnEquipments(IEnumerable<Item> items)
        {
            foreach (var item in items)
            {
                _equipment.TakeOnItem(item);               
            }
        }
    }
}
