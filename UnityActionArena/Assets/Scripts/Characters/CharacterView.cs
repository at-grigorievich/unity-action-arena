using ATG.Items.Equipment;
using UnityEngine;

namespace ATG.Character
{
    [RequireComponent(typeof(CharacterEquipmentView))]
    public class CharacterView: MonoBehaviour, IEquipmentViewable
    {
        private CharacterEquipmentView _equipmentView;
        
        public void Initialize()
        {
            _equipmentView = GetComponent<CharacterEquipmentView>();
            _equipmentView.Initialize();
        }
        
        public void PutOn(EquipmentViewData data)
        {
            _equipmentView.PutOn(data);
        }
        
        public void SetVisible(bool isVisible)
        {
            _equipmentView.SetVisible(isVisible);
        }
    }
}