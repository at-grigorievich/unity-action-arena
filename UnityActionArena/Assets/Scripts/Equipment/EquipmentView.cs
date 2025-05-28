using UnityEngine;

namespace ATG.Equipment
{
    public struct EquipmentViewData
    {
        public readonly Mesh Mesh;
        public readonly Material Material;

        public EquipmentViewData(Mesh mesh, Material material)
        {
            Mesh = mesh;
            Material = material;
        }
    }
    
    [RequireComponent(typeof(MeshRenderer))]
    public sealed class EquipmentView: MonoBehaviour
    {
        private MeshFilter _filter;
        private Renderer _renderer;

        private void Awake()
        {
            _filter = GetComponent<MeshFilter>();
            _renderer = GetComponent<MeshRenderer>();
        }

        public void SetActive(bool isActive)
        {
            _renderer.enabled = isActive;
        }
        
        public void ChangeVisual(EquipmentViewData data)
        {
            _filter.mesh = data.Mesh;
            _renderer.material = data.Material;
        }
    }
}