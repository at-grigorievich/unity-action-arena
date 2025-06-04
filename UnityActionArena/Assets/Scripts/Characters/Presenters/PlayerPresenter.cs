using ATG.Input;
using Characters.Observers;
using VContainer.Unity;

namespace ATG.Character
{
    public sealed class PlayerPresenter: CharacterPresenter, ITickable
    {
        private readonly CharacterInputObserver _inputObserver;
        
        public PlayerPresenter(CharacterView view, IInputable input) : base(view)
        {
            _inputObserver = new CharacterInputObserver(_view.transform, input, _moveService, _animator);
        }

        public override void SetActive(bool isActive)
        {
            base.SetActive(isActive);
            _inputObserver.SetActive(isActive);
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