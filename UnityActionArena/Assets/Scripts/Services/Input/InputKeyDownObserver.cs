using System;
using UnityEngine;
using VContainer.Unity;

namespace ATG.Input
{
    public class InputKeyDownObserver: ITickable
    {
        private readonly KeyCode _key;
        
        public event Action OnClicked;

        public InputKeyDownObserver(KeyCode key)
        {
            _key = key;            
        }
        
        public void Tick()
        {
            if(UnityEngine.Input.GetKeyDown(_key) == false) return;
            OnClicked?.Invoke();
        }
    }
}