using ATG.Items.Inventory;

namespace ATG.Character
{
    public sealed class LobbyCharacterPresenter: CharacterPresenter
    {
        private readonly Inventory _inventory;
        
        public LobbyCharacterPresenter(CharacterView view) : base(view)
        {
            _inventory = new Inventory();
        }
    }
}