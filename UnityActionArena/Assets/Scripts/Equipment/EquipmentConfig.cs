using System;
using UnityEngine;

namespace ATG.Equipment
{
    [Serializable]
    public sealed class MetaEquipmentInfo
    {
        [field: SerializeField] public string Name { get; private set; }
        [field: SerializeField] public string Description { get; private set; }
        [field: SerializeField] public Sprite Icon { get; private set; }
        [field: SerializeField] public int Cost { get; private set; }
    }

    [Serializable]
    public sealed class StatEquipmentInfo
    {
        [field: SerializeField] public int Health { get; private set; }
        [field: SerializeField] public int Stamina { get; private set; }
        [field: SerializeField] public int Damage {get; private set; }
        [field: SerializeField] public float Speed { get; private set; }
        [field: SerializeField] public float AttackRange { get; private set; }
    }

    [Serializable]
    public sealed class VisualEquipmentInfo
    {
        [field: SerializeField] public Mesh Mesh { get; private set; }
        [field: SerializeField] public Material Material { get; private set; }
    }
    
    [CreateAssetMenu(fileName = "base-equipment-config", menuName = "Configs/New Equipment Config")]
    public class EquipmentConfig: ScriptableObject
    {
        [field: SerializeField] public EquipmentTag Tag { get; private set; }
        [field: Space(5)]
        [field:SerializeField] public MetaEquipmentInfo MetaInfo { get; private set; }
        [field: Space(5)]
        [field: SerializeField] public StatEquipmentInfo StatInfo { get; private set; }
        [field: Space(5)]
        [field: SerializeField] public VisualEquipmentInfo VisualInfo { get; private set; }
            
        public string Id => this.name;
    }
}