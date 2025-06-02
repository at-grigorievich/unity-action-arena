using System;
using UnityEngine;
using VContainer.Unity;

namespace ATG.Input
{
    public sealed class InputService: IInputable, ITickable
    {
        private bool _lastClicked = false;
        
        public event Action<bool> OnLMBClicked;

        public void Tick()
        {
            bool curClicked = UnityEngine.Input.GetMouseButtonDown(0);

            if (_lastClicked != curClicked)
            {
                OnLMBClicked?.Invoke(curClicked);
            }
            
            _lastClicked = curClicked;
        }
        
        public Vector2 GetDirection()
        {
            float xInput = UnityEngine.Input.GetAxis("Horizontal");
            float yInput = UnityEngine.Input.GetAxis("Vertical");

            return new Vector3(xInput, yInput);
        }
    }
}