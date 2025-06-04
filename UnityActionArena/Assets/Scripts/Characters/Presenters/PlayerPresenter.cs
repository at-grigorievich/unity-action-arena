using ATG.Attack;
using ATG.Camera;
using ATG.Input;
using Characters.Observers;
using UnityEngine;
using VContainer.Unity;

namespace ATG.Character
{
    public sealed class PlayerPresenter: CharacterPresenter, ITickable
    {
        private readonly CharacterInputObserver _inputObserver;
        private readonly CinemachineWrapper _cinemachine;
        
        public PlayerPresenter(CharacterView view, IInputable input, CinemachineWrapper cinemachine, RaycastPool raycastPool) 
            : base(raycastPool, view)
        {
            Debug.Log(raycastPool == null);
            _inputObserver = new CharacterInputObserver(_view.transform, input, _moveService, _animator);
            _cinemachine = cinemachine;
        }

        public override void SetActive(bool isActive)
        {
            base.SetActive(isActive);
            _inputObserver.SetActive(isActive);
            _cinemachine.SelectPlayerTarget(isActive);
        }

        public void Tick()
        {
            if(_isActive == false) return;
                _inputObserver.Tick();
        }
        
        public override void Dispose()
        {
            base.Dispose();
            _inputObserver.SetActive(false);
        }
    }
}