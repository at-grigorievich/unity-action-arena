using System;
using ATG.Animator;
using ATG.Attack;
using ATG.Move;
using ATG.Input;
using UnityEngine;
using VContainer.Unity;

namespace Characters.Observers
{
    public sealed class MoveByInputObserver: ITickable
    {
        private readonly IInputable _input;
        
        private readonly Transform _characterTransform;
        
        private readonly IMoveableService _moveService;
        private readonly IAnimatorWrapper _animatorService;
        private readonly IAttackService _attacker;
        
        private readonly Transform _cameraTransform;
        
        public MoveByInputObserver(Transform characterTransform, IInputable input,
            IMoveableService moveService, IAttackService attackService, IAnimatorWrapper animatorService)
        {
            _characterTransform = characterTransform;
            
            _input = input;
            
            _moveService = moveService;
            _animatorService = animatorService;
            _attacker = attackService;

            if (Camera.main != null) _cameraTransform = Camera.main.transform;
            else throw new NullReferenceException("Camera.main is null");
        }
        
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

        public void Tick()
        {
            Vector2 input = _input.GetDirection();
            
            if (input.sqrMagnitude > float.Epsilon)
            {
                Vector3 targetPoint = CalculateTargetPoint(input);
                
                _moveService.MoveTo(targetPoint);
                _animatorService.SelectState(AnimatorTag.Run);
            }
            else
            {
                _moveService.Stop();
                _animatorService.SelectState(AnimatorTag.Idle);
            }
        }
        
        private void OnLMBClicked(bool obj)
        {
            _attacker.TakeSwing();
        }
        
        private Vector3 CalculateTargetPoint(Vector2 input)
        {
            Vector3 camForward = _cameraTransform.forward;
            Vector3 camRight = _cameraTransform.right;
                
            camForward.y = camRight.y = 0f;

            camForward.Normalize();
            camRight.Normalize();
                
            Vector3 moveDirection = (camForward * input.y + camRight * input.x).normalized;
                
            return _characterTransform.position + moveDirection;
        }
    }
}