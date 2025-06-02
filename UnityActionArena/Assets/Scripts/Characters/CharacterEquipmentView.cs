using UnityEngine;
using System.Collections.Generic;
using ATG.Items.Equipment;
using ATG.OtusHW.Inventory;
using VContainer.Unity;

namespace ATG.Character
{
    public class CharacterEquipmentView: MonoBehaviour, IInitializable, IEquipmentViewable
    {
        [SerializeField] private SkinnedMeshEquipmentView body;
        [SerializeField] private MeshFilterEquipmentView head;
        [SerializeField] private MeshFilterEquipmentView arm;

        private Dictionary<EquipType, IEquipmentViewable> _equipViewsDic;
        
        public void Initialize()
        {
            _equipViewsDic = new Dictionary<EquipType, IEquipmentViewable>
            {
                { EquipType.Head, head },
                { EquipType.Body, body },
                { EquipType.RightHand, arm }
            };
        }
        
        public void PutOn(EquipmentViewData data)
        {
            EquipType requiredType = data.Type;
            
            if(_equipViewsDic.ContainsKey(requiredType) == false)
                throw new KeyNotFoundException($"No equipment view exists with type {requiredType}");
            
            _equipViewsDic[requiredType].PutOn(data);
        }
    }
}