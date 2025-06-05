using ATG.Animator;
using ATG.Attack;
using ATG.Input;

namespace Characters.Observers
{
    public sealed class AttackByInputObserver
    {
        private readonly IInputable _input;
        private readonly IAttackService _attack;
        private readonly IAnimatorWrapper _animator;
        
        public void SetActive(bool isActive)
        {
            if (isActive == true)
            {
                _input.OnLMBClicked += OnLMBClicked;
            }
            else
            {
                _input.OnLMBClicked -= OnLMBClicked;
            }
        }

        private void OnLMBClicked(bool obj)
        {
            if(obj == false) return;
            if(_attack.IsAvailable == false) return;
            
            _animator.SelectState(AnimatorTag.Attack);
        }
    }
}