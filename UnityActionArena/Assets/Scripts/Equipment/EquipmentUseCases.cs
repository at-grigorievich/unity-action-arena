namespace ATG.Items.Equipment
{
    public static class EquipmentUseCases
    {
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
    }
}