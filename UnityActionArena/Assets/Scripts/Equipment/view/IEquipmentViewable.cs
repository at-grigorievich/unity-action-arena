using UnityEngine;

namespace ATG.Items.Equipment
{
    public readonly struct EquipmentViewData
    {
        public readonly Mesh Mesh;
        public readonly Material Material;

        public EquipmentViewData(Mesh mesh, Material material)
        {
            Mesh = mesh;
            Material = material;
        }
    }
    
    public interface IEquipmentViewable
    {
        void PutOn(EquipmentViewData data);
    }
}