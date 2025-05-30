using UnityEngine;

namespace ATG.Items.Equipment
{
    [RequireComponent(typeof(MeshFilter), typeof(MeshRenderer))]
    public sealed class MeshFilterEquipmentView: MonoBehaviour, IEquipmentViewable
    {
        private MeshFilter _filter;
        private MeshRenderer _renderer;

        private void Awake()
        {
            _filter = GetComponent<MeshFilter>();
            _renderer = GetComponent<MeshRenderer>();
        }

        public void PutOn(EquipmentViewData data)
        {
            Mesh meshCopy = Instantiate(data.Mesh);
            Material materialCopy = new Material(data.Material);
            
            _filter.mesh = meshCopy;
            _renderer.material = materialCopy;
        }
    }
}