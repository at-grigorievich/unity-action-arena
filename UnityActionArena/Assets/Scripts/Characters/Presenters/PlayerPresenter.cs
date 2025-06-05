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

        public override void SetActive(bool isActive)
        {
            base.SetActive(isActive);
            _moveObserver.SetActive(isActive);
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
        }
    }
}