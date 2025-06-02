using System.Collections.Generic;

namespace ATG.Items.Equipment
{
    public interface IEquipmentSource
    {
        IEnumerable<Item> GetItems();
    }
}