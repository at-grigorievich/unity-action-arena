using System;
using System.Collections.Generic;
using ATG.Character;

namespace ATG.Items.Equipment
{
    public sealed class HeroEquipEffectObserver: IEquipmentObserver, IDisposable
    {
        private readonly Equipment _equipment;
        private readonly CharacterModel _hero;

        public HeroEquipEffectObserver(Equipment equipment, CharacterModel hero)
        {
            _equipment = equipment;
            _hero = hero;
            
            _equipment.OnItemTakeOn += OnItemTakeOn;
            _equipment.OnItemTakeOff += OnItemTakeOff;
        }
        
        public void OnItemTakeOn(Item item)
        {
            if(item.TryGetComponents(out IEnumerable<HeroEffectComponent> effects) == false) return;

            foreach (var heroEffect in effects)
            {
                heroEffect.AddEffect(_hero);
            }
        }

        public void OnItemTakeOff(Item item)
        {
            if(item.TryGetComponents(out IEnumerable<HeroEffectComponent> effects) == false) return;

            foreach (var heroEffect in effects)
            {
                heroEffect.RemoveEffect(_hero);
            }
        }
        
        public void Dispose()
        {
            _equipment.OnItemTakeOn -= OnItemTakeOn;
            _equipment.OnItemTakeOff -= OnItemTakeOff;
        }
    }
}