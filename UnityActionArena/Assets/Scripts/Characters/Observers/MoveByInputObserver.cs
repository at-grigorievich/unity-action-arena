﻿using System;
using ATG.Animator;
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
        
        private readonly Transform _cameraTransform;

        private bool _isActive;
        
        public MoveByInputObserver(Transform characterTransform, IInputable input,
            IMoveableService moveService, IAnimatorWrapper animatorService)
        {
            _characterTransform = characterTransform;
            
            _input = input;
            
            _moveService = moveService;
            _animatorService = animatorService;

            if (Camera.main != null) _cameraTransform = Camera.main.transform;
            else throw new NullReferenceException("Camera.main is null");
        }
        
        public void SetActive(bool isActive)
        {
            _isActive = isActive;
        }

        public void Tick()
        {
            if(_isActive == false) return;
            
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