using System;
using ATG.Animator;
using ATG.Attack;
using ATG.Health;
using ATG.KillCounter;
using ATG.Move;
using ATG.Observable;
using ATG.Stamina;
using Characters.Observers;
using UnityEngine;
using VContainer.Unity;

namespace ATG.Character
{
    public abstract class ArenaCharacterPresenter: CharacterPresenter, ITickable, IDieCountable
    {
        protected readonly IAttackService _attack;
        protected readonly IHealthService<int> _health;
        protected readonly IStaminaService _stamina;

        protected readonly GetDamageObserver _getDamageObserver;

        protected CompositeObserveDisposable _dis;

        private AttackDamageData? _lastDamager;
        
        public event Action<AttackDamageData> OnDieCountRequired;
        
        protected ArenaCharacterPresenter(ArenaCharacterView view, CharacterModel model, 
            IAnimatorWrapper animator, IMoveableService move, 
            IAttackService attack, IStaminaService stamina) 
            : base(view, model, animator, move)
        {
            _attack = attack;
            _stamina = stamina;
            
            _health = new HealthService(model.Health);
            _getDamageObserver = new GetDamageObserver(_animator, _move, _health);
        }

        public override void Initialize()
        {
            base.Initialize();
            
            _attack.InitOwner(_view);
            
            _view.OnAttacked += RequestToGetDamageHandle;
            _attack.OnRequestToDealDamage += RequestToDealDamageHandle;
            _health.OnHealthIsOver += OnHealthIsOverHandle;
            
            _health.Initialize();
            _stamina.Initialize();
        }

        public override void SetActive(bool isActive)
        {
            base.SetActive(isActive);
            
            _getDamageObserver.SetActive(isActive);

            if (isActive)
            {
                _dis = new CompositeObserveDisposable();
            }
            else
            {
                _dis?.Dispose();
                _dis = null;
            }
        }

        public override void Dispose()
        {
            base.Dispose();
            
            _dis?.Dispose();
            _dis = null;
            
            _view.OnAttacked -= RequestToGetDamageHandle;
            _attack.OnRequestToDealDamage -= RequestToDealDamageHandle;
            _health.OnHealthIsOver -= OnHealthIsOverHandle;
        }

        public virtual void Tick()
        {
            if(_isActive == false) return;
            
            _stamina.Tick();
            _attack.Tick();
        }

        public override void Spawn(Vector3 spawnPosition, Quaternion spawnRotation)
        {
            base.Spawn(spawnPosition, spawnRotation);
            _lastDamager = null;
            _health.Reset();
            _stamina.Reset();
        }

        public override void TakeOnEquipments(params Items.Item[] items)
        {
            base.TakeOnEquipments(items);
            _health.Initialize();
            _stamina.Initialize();
        }

        protected virtual void RequestToDealDamageHandle(IAttackable obj)
        {
            obj.TakeHitByAttacker(new AttackDamageData(Nick,
                _view.transform, 
                _characterModel.Damage.Value, 
                _characterModel.GetRate()));
        }

        protected virtual void RequestToGetDamageHandle(AttackDamageData damageData)
        {
            _lastDamager = damageData;
            _getDamageObserver.ReceiveDamage(_lastDamager.Value);
        }
        
        protected virtual void OnHealthIsOverHandle()
        {
            SetActive(false);
            InformAboutDie();
        }

        private void InformAboutDie()
        {
            RequireSpawn();
            
            if (_lastDamager.HasValue == true)
            {
                OnDieCountRequired?.Invoke(_lastDamager.Value);
            }
        }
    }
}