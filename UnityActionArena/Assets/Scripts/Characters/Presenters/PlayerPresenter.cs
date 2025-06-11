using ATG.Animator;
using ATG.Attack;
using ATG.Camera;
using ATG.Input;
using ATG.Move;
using ATG.Observable;
using ATG.Stamina;
using Characters.Observers;

namespace ATG.Character
{
    public sealed class PlayerPresenter: ArenaCharacterPresenter
    {
        private readonly CinemachineWrapper _cinemachine;
        
        private readonly MoveByInputObserver _moveObserver;
        private readonly AttackByInputObserver _attackObserver;

        private readonly CompositeObserveDisposable _dis;
        
        public PlayerPresenter(CharacterView view, CharacterModel model,
            IAnimatorWrapper animator, IMoveableService move, 
            IAttackService attack, CinemachineWrapper cinemachine, 
            IInputable input, IStaminaService stamina) 
            : base(view, model, animator, move, attack, stamina)
        {
            _cinemachine = cinemachine;
            _moveObserver = new MoveByInputObserver(_view.transform, input, _move, _animator);
            _attackObserver = new AttackByInputObserver(input, _attack, _animator, _stamina);
            
            _dis = new CompositeObserveDisposable();
        }

        public override void Initialize()
        {
            base.Initialize();
            
            _getDamageObserver.IsDamaged.Subscribe(isDamagedNow =>
            {
                _attackObserver.SetActive(!isDamagedNow);
                _moveObserver.SetActive(!isDamagedNow);
            }).AddTo(_dis);
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
            
            _dis.Dispose();
            
            _moveObserver.SetActive(false);
        }
    }
}