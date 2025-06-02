using System;
using System.Collections.Generic;
using ATG.Character;

namespace ATG.Items.Inventory
{
    public class HeroConsumeEffectsObserver: IDisposable
    {
        private readonly Inventory _inventory;
        private readonly CharacterModel _hero;
        
        public HeroConsumeEffectsObserver(Inventory inventory, CharacterModel hero)
        {
            _inventory = inventory;
            _hero = hero;
            
            _inventory.OnItemConsumed += OnItemConsumed;
        }
        
        private void OnItemConsumed(Item item)
        {
            if(item.TryGetComponents(out IEnumerable<HeroEffectComponent> effects) == false) return;
            
            foreach (var heroEffectComponent in effects)
            {
                heroEffectComponent.AddEffect(_hero);
            }
        }
        
        public void Dispose()
        {
            _inventory.OnItemConsumed -= OnItemConsumed;
        }
    }
}