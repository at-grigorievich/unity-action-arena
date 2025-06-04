using ATG.Animator;
using ATG.Attack;
using ATG.Move;
using UnityEngine;

namespace ATG.Character
{
    public sealed class BotPresenter: CharacterPresenter
    {
        public readonly TargetNavigationPointSet NavigationPoints;
        
        public BotPresenter(CharacterView view, TargetNavigationPointSet navigationSet, RaycastPool raycastPool) 
            : base(raycastPool, view)
        {
            NavigationPoints = navigationSet;
        }

        public override void SetActive(bool isActive)
        {
            Idle();
            base.SetActive(isActive);
        }
        
        public void Idle()
        {
            _moveService.Stop();
            _animator.SelectState(AnimatorTag.Idle);
        }
        
        public void MoveTo(Vector3 position)
        {
            _moveService.MoveTo(position);
            _animator.SelectState(AnimatorTag.Run);
        }
    }
}