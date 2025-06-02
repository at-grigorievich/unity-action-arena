using System.Collections.Generic;
using ATG.OtusHW.Inventory;
using UnityEngine;

namespace ATG.Items.Equipment
{
    public sealed class RandomEquipmentSource: IEquipmentSource
    {
        private readonly ItemsSetConfig _itemsSet;

        private readonly Dictionary<EquipType, IReadOnlyList<Item>> _equipmentByType;
        
        public RandomEquipmentSource(ItemsSetConfig itemsSet)
        {
            _itemsSet = itemsSet;

            _equipmentByType = new()
            {
                { EquipType.Head, _itemsSet.GetEquipByType(EquipType.Head) },
                { EquipType.Body, _itemsSet.GetEquipByType(EquipType.Body) },
                { EquipType.RightHand, _itemsSet.GetEquipByType(EquipType.RightHand) }
            };
        }

        public IEnumerable<Item> GetItemsByType(EquipType equipType)
        {
            if(_equipmentByType.TryGetValue(equipType, out var typeList) == false)
                throw new KeyNotFoundException($"{equipType} not found");
            
            return typeList;
        }
        
        public IEnumerable<Item> GetItems()
        {
            List<Item> result = new(3);
            result.Add(GetRandomHead());
            result.Add(GetRandomBody());
            result.Add(GetRandomRightHand());

            return result;
        }

        private Item GetRandomHead()
        {
            return GetRandom(_equipmentByType[EquipType.Head]);
        }

        private Item GetRandomBody()
        {
            return GetRandom(_equipmentByType[EquipType.Body]);
        }

        private Item GetRandomRightHand()
        {
            return GetRandom(_equipmentByType[EquipType.RightHand]);
        }

        private Item GetRandom(IReadOnlyList<Item> items)
        {
            int count = items.Count;
            int randomIndex = UnityEngine.Random.Range(0, count);
            return items[randomIndex];
        }
    }
}