using ATG.OtusHW.Inventory;
using UnityEngine;

namespace ATG.Items.Equipment
{
    public readonly struct EquipmentViewData
    {
        public readonly EquipType Type;
        public readonly Mesh Mesh;
        public readonly Material Material;

        public EquipmentViewData(EquipType type, Mesh mesh, Material material)
        {
            Type = type;
            Mesh = mesh;
            Material = material;
        }
    }
    
    public interface IEquipmentViewable
    {
        void PutOn(EquipmentViewData data);
    }
}