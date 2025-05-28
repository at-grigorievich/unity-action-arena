using ATG.Character.Animator;
using ATG.Character.Attack;
using ATG.Character.Equipment;
using ATG.Character.Health;

namespace ATG.Character
{
    public abstract class CharacterPresenter
    {
        protected readonly CharacterView _view;
        
        protected readonly IHealthService _healthService;
        protected readonly IAttackService _attackService;
        protected readonly IAnimatorService _animatorService;
        protected readonly IEquipmentService _equipmentService;
        
        protected CharacterPresenter(CharacterView view)
        {
            _view = view;

            _healthService = null;
            _attackService = null;
            _animatorService = null;
            _equipmentService = null;
        }
    }
}
