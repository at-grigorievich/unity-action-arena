using ATG.Character.Animator;
using ATG.Character.Attack;
using ATG.Character.Health;
using ATG.Items.Equipment;
using VContainer.Unity;

namespace ATG.Character
{
    public abstract class CharacterPresenter: IInitializable
    {
        protected readonly CharacterView _view;

        protected readonly Equipment _equipment;
        
        protected readonly IHealthService _healthService;
        protected readonly IAttackService _attackService;
        protected readonly IAnimatorService _animatorService;
        
        protected CharacterPresenter(CharacterView view)
        {
            _view = view;
            _equipment = new Equipment();
        }

        public void Initialize()
        {
            _view.Initialize();
        }
    }
}
