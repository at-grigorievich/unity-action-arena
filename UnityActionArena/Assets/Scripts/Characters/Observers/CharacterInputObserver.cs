using System;
using ATG.Character.Animator;
using ATG.Character.Move;
using ATG.Input;
using UnityEngine;
using VContainer.Unity;

namespace Characters.Observers
{
    public sealed class CharacterInputObserver: IInitializable, IDisposable, ITickable
    {
        private readonly IInputable _input;
        
        private readonly Transform _characterTransform;
        
        private readonly IMoveableService _moveService;
        private readonly IAnimatorService _animatorService;
        
        private readonly Transform _cameraTransform;
        
        public CharacterInputObserver(Transform characterTransform, IInputable input,
            IMoveableService moveService, IAnimatorService animatorService)
        {
            _characterTransform = characterTransform;
            
            _input = input;
            
            _moveService = moveService;
            _animatorService = animatorService;

            if (Camera.main != null) 
                _cameraTransform = Camera.main.transform;
            else throw new NullReferenceException("Camera.main is null");
        }
        
        public void Initialize()
        {
            
        }
        
        public void Tick()
        {
            Vector2 input = _input.GetDirection();
            
            float sqrMagnitude = input.sqrMagnitude;

            if (sqrMagnitude > float.Epsilon)
            {
                Vector3 camForward = _cameraTransform.forward;
                Vector3 camRight = _cameraTransform.right;
                
                camForward.y = camRight.y = 0f;

                camForward.Normalize();
                camRight.Normalize();
                
                Vector3 moveDirection = (camForward * input.y + camRight * input.x).normalized;
                
                Vector3 targetPoint = _characterTransform.position + moveDirection;
                
                _moveService.MoveTo(targetPoint);
            }
            else
            {
                _moveService.Stop();
            }
        }
        
        public void Dispose()
        {
            // TODO release managed resources here
        }
    }
}