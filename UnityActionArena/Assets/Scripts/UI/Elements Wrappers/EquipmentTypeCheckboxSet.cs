using System;
using ATG.OtusHW.Inventory;
using UnityEngine;

namespace ATG.UI
{
    public sealed class EquipmentTypeCheckboxSet: UIElement<EquipType>
    {
        [Serializable]
        private class CheckboxContainer
        {
            [field: SerializeField] public EquipType EquipType { get; private set; }
            [field: SerializeField] public EquipmentTypeCheckbox Checkbox { get; private set; }
        }
        
        [SerializeField] private CheckboxContainer[] checkboxes;

        public event Action<EquipType> OnSelect;
        
        public override void Show(object sender, EquipType data)
        {
            foreach (var c in checkboxes)
            {
                c.Checkbox.Show(this, c.EquipType);
                c.Checkbox.OnSelected += OnSelected;
                
                if(c.EquipType == data)
                    c.Checkbox.Select();
            }
        }

        public override void Hide()
        {
            foreach (var c in checkboxes)
            {
                c.Checkbox.Hide();
                c.Checkbox.OnSelected -= OnSelected;
            }
        }
        
        private void OnSelected(ScaleCheckboxButton<EquipType> obj)
        {
            OnSelect?.Invoke(obj.Data);
            
            foreach (var checkboxContainer in checkboxes)
            {
                if(ReferenceEquals(obj, checkboxContainer.Checkbox) == true) continue;
                
                checkboxContainer.Checkbox.DoReset();
            }
        }
    }
}