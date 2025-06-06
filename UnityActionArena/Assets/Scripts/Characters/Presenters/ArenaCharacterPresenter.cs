using ATG.Animator;
using ATG.Attack;
using ATG.Move;
using UnityEngine;
using VContainer.Unity;

namespace ATG.Character
{
    public abstract class ArenaCharacterPresenter: CharacterPresenter, ITickable
    {
        protected readonly IAttackService _attack;
        
        protected ArenaCharacterPresenter(CharacterView view, CharacterModel model, 
            IAnimatorWrapper animator, IMoveableService move, IAttackService attack) 
            : base(view, model, animator, move)
        {
            _attack = attack;
            _attack.InitOwner(_view);
        }

        public override void Initialize()
        {
            base.Initialize();
            _attack.OnRequestToDealDamage += RequestToDealDamageHandle;
        }

        public override void Dispose()
        {
            base.Dispose();
            _attack.OnRequestToDealDamage -= RequestToDealDamageHandle;
        }

        public virtual void Tick()
        {
            if(_isActive == false) return;
            
            _attack.Tick();
        }
        
        private void RequestToDealDamageHandle(IAttackable obj)
        {
            obj.TakeHitByAttacker(new AttackDamageData(_characterModel.Damage.Value));
        }
    }
}