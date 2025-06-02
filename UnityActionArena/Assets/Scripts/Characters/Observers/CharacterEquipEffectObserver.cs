using System.Collections.Generic;
using ATG.Items;
using ATG.Items.Equipment;

namespace ATG.Character
{
    public sealed class CharacterEquipEffectObserver: IEquipmentObserver
    {
        private readonly Equipment _equipment;
        private readonly CharacterModel _model;

        public CharacterEquipEffectObserver(Equipment equipment, CharacterModel model)
        {
            _equipment = equipment;
            _model = model;
        }
        
        public void Initialize()
        {
            _equipment.OnItemTakeOn += OnItemTakeOn;
            _equipment.OnItemTakeOff += OnItemTakeOff;
        }
        
        public void Dispose()
        {
            _equipment.OnItemTakeOn -= OnItemTakeOn;
            _equipment.OnItemTakeOff -= OnItemTakeOff;
        }
        
        public void OnItemTakeOn(Item item)
        {
            if(item.TryGetComponents(out IEnumerable<HeroEffectComponent> effects) == false) return;

            foreach (var heroEffect in effects)
            {
                heroEffect.AddEffect(_model);
            }
        }

        public void OnItemTakeOff(Item item)
        {
            if(item.TryGetComponents(out IEnumerable<HeroEffectComponent> effects) == false) return;

            foreach (var heroEffect in effects)
            {
                heroEffect.RemoveEffect(_model);
            }
        }
    }
}