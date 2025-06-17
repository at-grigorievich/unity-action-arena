using System;
using ATG.Pause;
using UnityEngine;
using VContainer.Unity;

namespace ATG.Input
{
    public sealed class InputService: IInputable, ITickable, IPausable
    {
        private bool _lastClicked = false;
        private bool _isPaused = false;
        
        public event Action<bool> OnLMBClicked;

        public void Tick()
        {
            if(_isPaused == true) return;
            
            bool curClicked = UnityEngine.Input.GetMouseButtonDown(0);

            if (_lastClicked != curClicked)
            {
                OnLMBClicked?.Invoke(curClicked);
            }
            
            _lastClicked = curClicked;
        }
        
        public Vector2 GetDirection()
        {
            if (_isPaused == true) return Vector2.zero;
            
            float xInput = UnityEngine.Input.GetAxis("Horizontal");
            float yInput = UnityEngine.Input.GetAxis("Vertical");

            return new Vector3(xInput, yInput);
        }

        public void SetPauseStatus(bool isPaused)
        {
            _isPaused = isPaused;
        }
    }
}