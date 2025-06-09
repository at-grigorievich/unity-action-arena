using System.Collections.Generic;
using ATG.Animator;
using ATG.Attack;
using ATG.Health;
using ATG.Items;
using ATG.Move;
using ATG.Stamina;
using Settings;
using UnityEngine;
using VContainer.Unity;

namespace ATG.Character
{
    public abstract class ArenaCharacterPresenter: CharacterPresenter, ITickable
    {
        protected readonly IAttackService _attack;
        protected readonly IHealthService<int> _health;
        protected readonly IStaminaService _stamina;
        
        protected ArenaCharacterPresenter(CharacterView view, CharacterModel model, 
            IAnimatorWrapper animator, IMoveableService move, IAttackService attack,
            IStaminaReset staminaReset) 
            : base(view, model, animator, move)
        {
            _attack = attack;
            _health = new HealthService(model.Health);
            _stamina = new AutoResetStaminaService(model.Stamina, staminaReset);
        }

        public override void Initialize()
        {
            base.Initialize();
            
            _attack.InitOwner(_view);
            
            _view.OnAttacked += RequestToGetDamageHandle;
            _attack.OnRequestToDealDamage += RequestToDealDamageHandle;
            _health.OnHealthIsOver += OnHealthIsOverHandle;
        }

        public override void SetActive(bool isActive)
        {
            base.SetActive(isActive);
            _animator.SetActive(isActive);
        }

        public override void Dispose()
        {
            base.Dispose();
            
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
            _health.Reset();
            _stamina.Reset();
        }

        public override void TakeOnEquipments(IEnumerable<Item> items)
        {
            base.TakeOnEquipments(items);
            _health.Initialize();
            _stamina.Initialize();
        }

        protected virtual void RequestToDealDamageHandle(IAttackable obj)
        {
            obj.TakeHitByAttacker(new AttackDamageData(_characterModel.Damage.Value));
        }
        
        protected virtual void RequestToGetDamageHandle(AttackDamageData damageData)
        {
            _animator.SelectState(AnimatorTag.GetDamage);
            _health.Reduce(damageData.Damage);
        }
        
        protected virtual void OnHealthIsOverHandle()
        {
            SetActive(false);
            RequireSpawn();
        }
    }
}