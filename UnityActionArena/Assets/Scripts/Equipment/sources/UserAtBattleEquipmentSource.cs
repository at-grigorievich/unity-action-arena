using System;
using System.Collections.Generic;

namespace ATG.Items.Equipment
{
    public sealed class UserAtBattleEquipmentSource: IEquipmentSource, IDisposable
    {
        private IEnumerable<Item> _equippedItemsCached;

        public void Update(IEnumerable<Item> items)
        {
            _equippedItemsCached = items;
        }
        
        public IEnumerable<Item> GetItems()
        {
            if (_equippedItemsCached == null)
            {
                throw new NullReferenceException("The equipped items cache is null.");
            }

            return _equippedItemsCached;
        }

        public void Dispose()
        {
            _equippedItemsCached = null;
        }
    }
}