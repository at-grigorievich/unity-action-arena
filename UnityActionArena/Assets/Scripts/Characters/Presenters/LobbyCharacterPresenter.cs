using System.Collections.Generic;
using ATG.Animator;
using ATG.Attack;
using ATG.Items;
using ATG.Items.Inventory;
using ATG.Move;

namespace ATG.Character
{
    public sealed class LobbyCharacterPresenter: CharacterPresenter
    {
        private readonly Inventory _inventory;

        public LobbyCharacterPresenter(CharacterView view, CharacterModel model, 
            IAnimatorWrapper animator, IMoveableService move) 
            : base(view, model, animator, move)
        {
            _inventory = new Inventory();
        }

        public IEnumerable<Item> EquippedItems => _equipment.Items.Values;
    }
}