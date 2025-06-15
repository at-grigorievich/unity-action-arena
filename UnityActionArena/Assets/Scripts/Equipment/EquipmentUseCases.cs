using System.Linq;
using ATG.OtusHW.Inventory;
using UnityEngine;

namespace ATG.Items.Equipment
{
    public static class EquipmentUseCases
    {
        public static bool AlreadyEquipped(this Equipment equipment, Item item)
        {
            if (item.CanEquip() == false) return false;
            if(item.TryGetComponent(out HeroEquipmentComponent component) == false) return false;

            EquipType equipType = component.Type;
            
            if (equipment.Items.TryGetValue(equipType, out var equipmentItem) == false) return false;

            return equipmentItem.Id == item.Id;
        }
        
        public static void TakeOnItem(this Equipment equipment, Item item)
        {
            if(item.CanEquip() == false) return;
            
            if(item.TryGetComponent(out HeroEquipmentComponent component) == false) return;

            if (equipment.Items.ContainsKey(component.Type) == true)
            {
                equipment.TakeOffItem(item);
            }
            
            equipment.Items.Add(component.Type, item);
            
            equipment.NotifyItemTakeOn(item);
        }

        public static Item TakeOffItem(this Equipment equipment, Item item)
        {
            if(item.CanEquip() == false) return null;
            
            if(item.TryGetComponent(out HeroEquipmentComponent component) == false) return null;

            if (equipment.Items.ContainsKey(component.Type) == true)
            {
                Item result = equipment.Items[component.Type];
                
                equipment.Items.Remove(component.Type);
                
                equipment.NotifyItemTakeOff(result);

                return result;
            }

            return null;
        }
        
        public static string[] GetItemsIdArray(Equipment equipment)
        {
            return equipment.Items.Values.Select(i => i.Id).ToArray();
        }
    }
}