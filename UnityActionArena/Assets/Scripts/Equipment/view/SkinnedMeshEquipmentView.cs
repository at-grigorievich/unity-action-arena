using UnityEngine;

namespace ATG.Items.Equipment
{
    [RequireComponent(typeof(SkinnedMeshRenderer))]
    public sealed class SkinnedMeshEquipmentView: MonoBehaviour, IEquipmentViewable
    {
        private SkinnedMeshRenderer _skin;

        private void Awake()
        {
            _skin = GetComponent<SkinnedMeshRenderer>();
        }

        public void SetVisible(bool isActive)
        {
            _skin.enabled = isActive;
        }
        
        public void PutOn(EquipmentViewData data)
        {
            Mesh meshCopy = Instantiate(data.Mesh);
            Material matCopy = Instantiate(data.Material);
            
            _skin.sharedMesh = meshCopy;
            _skin.material = matCopy;
        }
    }
}