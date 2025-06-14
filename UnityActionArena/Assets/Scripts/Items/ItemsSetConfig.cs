using System;
using System.Collections.Generic;
using System.Linq;
using ATG.OtusHW.Inventory;
using UnityEngine;

namespace ATG.Items
{
    [CreateAssetMenu(fileName = "items set", menuName = "Configs/New Item Set Config", order = 0)]
    public class ItemsSetConfig: ScriptableObject
    {
        [SerializeField] private ItemConfig[] items;

        public Item GetPrototypeById(string id)
        {
            var res = items.FirstOrDefault(i => i.Prototype.Id == id);
            
            if(res == null)
                throw new NullReferenceException($"Cant find prototype by id = {id}");

            return res.Prototype;
        }
        
        public IReadOnlyList<Item> GetEquipByType(EquipType equipType)
        {
            List<Item> result = new();
            
            foreach (var itemConfig in items)
            {
                var item = itemConfig.Prototype;
                
                if (item.CanEquip() == false) continue;
                if (item.TryGetComponent(out HeroEquipmentComponent component) == false)
                {
                    throw new NullReferenceException($"{itemConfig.name} Hero Equipment Component" +
                                                     " is null, but has equippable flag");
                }
                if(component.Type != equipType) continue;
                
                result.Add(item.Clone());
            }

            return result;
        }
    }
}