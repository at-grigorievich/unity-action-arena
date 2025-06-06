using System.Collections.Generic;
using ATG.Animator;
using ATG.Attack;
using ATG.Camera;
using ATG.Input;
using ATG.Move;
using Characters.Observers;

namespace ATG.Character
{
    public sealed class PlayerPresenter: ArenaCharacterPresenter
    {
        private readonly CinemachineWrapper _cinemachine;
        
        private readonly MoveByInputObserver _moveObserver;
        private readonly AttackByInputObserver _attackObserver;
        
        public PlayerPresenter(CharacterView view, CharacterModel model,
            IAnimatorWrapper animator, IMoveableService move, 
            IAttackService attack, CinemachineWrapper cinemachine, 
            IInputable input) 
            : base(view, model, animator, move, attack)
        {
            _cinemachine = cinemachine;
            _moveObserver = new MoveByInputObserver(_view.transform, input, _move, _animator);
            _attackObserver = new AttackByInputObserver(input, _attack, _animator);
        }

        public override void Initialize()
        {
            base.Initialize();

            _attackObserver.OnAttackCompleted += OnAttackCompleted;
        }

        public override void SetActive(bool isActive)
        {
            base.SetActive(isActive);
            
            _moveObserver.SetActive(isActive);
            _attackObserver.SetActive(isActive);
            
            _cinemachine.SelectPlayerTarget(isActive);
        }

        public override void Tick()
        {
            base.Tick();
            if(_isActive == false) return;
            _moveObserver.Tick();
        }
        
        public override void Dispose()
        {
            base.Dispose();
            
            _moveObserver.SetActive(false);
            _attackObserver.OnAttackCompleted -= OnAttackCompleted;
        }

        protected override void OnHealthIsOverHandle()
        {
            base.OnHealthIsOverHandle();
            _moveObserver.SetActive(false);
            _attackObserver.SetActive(false);
            _cinemachine.SelectPlayerTarget(false);
        }

        private void OnAttackCompleted(IReadOnlyCollection<IAttackable> attackables)
        {
            //Debug.Log(attackables.Count());
            /*foreach (var attackable in attackables)
            {
                
            }*/
        }
    }
}