using ATG.Animator;
using ATG.Attack;
using ATG.Move;
using Settings;
using UnityEngine;

namespace ATG.Character
{
    public sealed class BotPresenter: ArenaCharacterPresenter
    {
        public readonly TargetNavigationPointSet NavigationPoints;
        
        public BotPresenter(CharacterView view, CharacterModel model, 
            IAnimatorWrapper animator, IMoveableService move, 
            IAttackService attack, IStaminaReset staminaReset,
            TargetNavigationPointSet navigationPoints) 
            : base(view, model, animator, move, attack, staminaReset)
        {
            NavigationPoints = navigationPoints;
        }

        public override void SetActive(bool isActive)
        {
            Idle();
            base.SetActive(isActive);
        }
        
        public void Idle()
        {
            _move.Stop();
            _animator.SelectState(AnimatorTag.Idle);
        }
        
        public void MoveTo(Vector3 position)
        {
            _move.MoveTo(position);
            _animator.SelectState(AnimatorTag.Run);
        }
    }
}