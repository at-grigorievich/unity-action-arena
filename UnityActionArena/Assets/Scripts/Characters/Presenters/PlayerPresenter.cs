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
            _inputObserver = new CharacterInputObserver(_view.transform, input, _moveService, null);
        }

        public override void Initialize()
        {
            base.Initialize();
            _inputObserver.Initialize();
        }

        public void Tick()
        {
            _inputObserver.Tick();
        }
        
        public override void Dispose()
        {
            base.Dispose();
            _inputObserver.Dispose();
        }
    }
}