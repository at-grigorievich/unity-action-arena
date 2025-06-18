using ATG.Animator;
using ATG.Attack;
using ATG.EnemyDetector;
using ATG.Move;
using ATG.Observable;
using ATG.Stamina;
using ATG.UI;
using Characters.Observers;
using UnityEngine;

namespace ATG.Character
{
    public sealed class BotPresenter: ArenaCharacterPresenter
    {
        private readonly IEnemyDetectorSensor _enemyDetectorSensor;
        private readonly AttackByMBTObserver _attackObserver;

        private readonly ArenaBotUIView _uiView;
        
        public readonly TargetNavigationPointSet NavigationPoints;
        
        public readonly IObservableVar<bool> HasDetectedEnemies;
        
        public IObservableVar<int> Health => _characterModel.Health;
        public IObservableVar<bool> IsGetDamage => _getDamageObserver.IsDamaged;
        public IObservableVar<bool> IsAttacking => _attackObserver.IsAttacking;
        
        public IDetectable TargetDetectedEnemy { get; private set; } = null;

        public float AttackRange => _characterModel.Range.Value;
        public bool EnoughStamina => _stamina.IsEnough;
        public AttackDamageData? LastReceivedDamage => _getDamageObserver.LastReceivedDamage;
        
        public BotPresenter(ArenaCharacterView view, ArenaBotUIView uiView, CharacterModel model, 
            IAnimatorWrapper animator, IMoveableService move, 
            IAttackService attack, IStaminaService stamina,
            TargetNavigationPointSet navigationPoints) 
            : base(view, model, animator, move, attack, stamina)
        {
            _uiView = uiView;
            
            NavigationPoints = navigationPoints;
            _enemyDetectorSensor = new RangeEnemyDetectorSensor(_characterModel.Range, _view);
            _attackObserver = new AttackByMBTObserver(_attack, _animator, _stamina);
            
            HasDetectedEnemies = new ObservableVar<bool>(false);
        }
        
        public override void SetActive(bool isActive)
        {
            base.SetActive(isActive);
            _attackObserver.SetActive(isActive);

            if (isActive == true)
            {            
                _getDamageObserver.IsDamaged.Subscribe(isDamagedNow =>
                {
                    _attackObserver.SetActive(!isDamagedNow);
                    Stop();
                }).AddTo(_dis);
                
                _uiView.Show(this, new ArenaBotUIData(_health));
            }
            else
            {
                _uiView.Hide();
            }
        }

        public override void Tick()
        {
            base.Tick();
            CheckEnemyBySensor();
        }

        public override void Dispose()
        {
            base.Dispose();
            _attackObserver.Dispose();
        }

        public override void SetVisible(bool isVisible)
        {
            base.SetVisible(isVisible);
            if (isVisible == true)
            {
                _uiView.Show(this, new ArenaBotUIData(_health));
            }
            else
            {
                _uiView.Hide();
            }
        }

        public void Attack()
        {
            _attackObserver.OnAttackRequired();
        }
        
        public void SetTargetEnemy(IDetectable target) =>
            TargetDetectedEnemy = target;
        
        public bool TryDetectTargetByType(EnemyDetectorType detectionType, out IDetectable target) =>
            _enemyDetectorSensor.TryDetect(detectionType, out target);

        public bool CanReachPosition(Vector3 inputPosition, out Vector3 targetPosition) => 
            _move.CanReach(inputPosition, out targetPosition);

        #region Move & Rotate
        public void MoveTo(Vector3 position)
        {
            _move.MoveTo(position);
            _animator.SelectState(AnimatorTag.Run);
        }
        
        public void Stop()
        {
            _animator.SelectState(AnimatorTag.Idle);
            _animator.SelectState(AnimatorTag.None);
            _move.Stop();
        }
        
        public void LookAt(Vector3 position) => _move.LookAt(position);
        #endregion

        private void CheckEnemyBySensor()
        {
            HasDetectedEnemies.Value = _enemyDetectorSensor.CheckDetect();
            if (HasDetectedEnemies.Value == false)
            {
                TargetDetectedEnemy = null;
            }
        }
    }
}