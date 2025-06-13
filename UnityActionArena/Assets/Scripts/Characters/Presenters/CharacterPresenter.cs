using System;
using System.Collections.Generic;
using ATG.Animator;
using ATG.EnemyDetector;
using ATG.Items;
using ATG.Items.Equipment;
using ATG.Move;
using ATG.Spawn;
using UnityEngine;
using VContainer.Unity;

namespace ATG.Character
{
    public abstract class CharacterPresenter: IInitializable, IDisposable, ISpawnable, IDetectable
    {
        protected readonly CharacterView _view;
        protected readonly CharacterModel _characterModel;
        
        protected readonly Equipment _equipment;
        
        protected readonly IMoveableService _move;
        protected readonly IAnimatorWrapper _animator;
        
        private readonly IEquipmentObserver _equipmentModelObserver;
        private readonly IEquipmentObserver _equipmentViewObserver;
        
        protected bool _isActive;

        public Vector3 Position => _view.Position;
        public Vector3 Forward => _view.transform.forward;
        public Quaternion Rotation => _view.Rotation;
        
        public string Nick => _characterModel.Name;
        public float Rate => _characterModel.GetRate();

        public event Action OnSpawned;
        public event Action<ISpawnable> OnSpawnRequired;
        
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
            
            _view.Initialize(this);
        }
        
        public virtual void Initialize()
        {
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
            
            _move.SetActive(isActive);
            _animator.SetActive(_isActive);
            
            //SetVisible(isActive);
            SetPhysActive(isActive);
        }
        
        public void SetVisible(bool isVisible) => _view.SetVisible(isVisible);
        public void SetPhysActive(bool isActive) => _view.SetPhysActive(isActive);

        public virtual void Spawn(Vector3 spawnPosition, Quaternion spawnRotation)
        {
            _move.PlaceTo(spawnPosition, spawnRotation);
            SetActive(true);
            
            OnSpawned?.Invoke();
        }
        
        public virtual void TakeOnEquipments(IEnumerable<Item> items)
        {
            foreach (var item in items)
            {
                _equipment.TakeOnItem(item);               
            }
        }

        protected void RequireSpawn() => OnSpawnRequired?.Invoke(this);
        
        public EnemyData GetEnemyData()
        {
            return new EnemyData(_view.transform, Rate, _move.CurrentSpeed);
        }
    }
}
