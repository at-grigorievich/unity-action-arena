using System;
using System.Collections.Generic;
using ATG.OtusHW.Inventory;
using Sirenix.OdinInspector;

namespace ATG.Items.Equipment
{
    [Serializable]
    public class Equipment
    {
        [ShowInInspector]
        public readonly Dictionary<EquipType, Item> Items = new();

        public event Action<Item> OnItemTakeOn;
        public event Action<Item> OnItemTakeOff;
        
        public void NotifyItemTakeOn(Item item)
        {
            OnItemTakeOn?.Invoke(item);
        }

        public void NotifyItemTakeOff(Item item)
        {
            OnItemTakeOff?.Invoke(item);
        }
    }
}