using System;
using VContainer.Unity;

namespace ATG.Items.Equipment
{
    public interface IEquipmentObserver: IInitializable, IDisposable
    {
        void OnItemTakeOn(Item item);
        void OnItemTakeOff(Item item);
    }
}