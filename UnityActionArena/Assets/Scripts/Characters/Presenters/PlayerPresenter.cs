using ATG.Animator;
using ATG.Attack;
using ATG.Camera;
using ATG.Health;
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

        public IHealthRate<int> HealthRate => _health;
        public IStaminaRate StaminaRate => _stamina;
        
        public PlayerPresenter(ArenaCharacterView view, CharacterModel model,
            IAnimatorWrapper animator, IMoveableService move, 
            IAttackService attack, CinemachineWrapper cinemachine, 
            IInputable input, IStaminaService stamina) 
            : base(view, model, animator, move, attack, stamina)
        {
            _cinemachine = cinemachine;
            _moveObserver = new MoveByInputObserver(_view.transform, input, _move, _animator);
            _attackObserver = new AttackByInputObserver(input, _attack, _animator, _stamina);
        }
        
        public override void SetActive(bool isActive)
        {
            base.SetActive(isActive);

            if (_isActive == true) 
            {
                _getDamageObserver.IsDamaged.Subscribe(isDamagedNow =>
                {
                    _attackObserver.SetActive(!isDamagedNow);
                    _moveObserver.SetActive(!isDamagedNow);
                }).AddTo(_dis);
            }
            
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
            _attackObserver.Dispose();
        }
    }
}