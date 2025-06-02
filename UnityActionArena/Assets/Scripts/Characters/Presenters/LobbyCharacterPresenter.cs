using System.Collections.Generic;
using ATG.Items;
using ATG.Items.Inventory;

namespace ATG.Character
{
    public sealed class LobbyCharacterPresenter: CharacterPresenter
    {
        private readonly Inventory _inventory;
        
        public IEnumerable<Item> EquippedItems => _equipment.Items.Values;
        
        public LobbyCharacterPresenter(CharacterView view) : base(view)
        {
            _inventory = new Inventory();
        }
    }
}