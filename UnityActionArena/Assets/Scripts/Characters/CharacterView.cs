using System;
using ATG.Attack;
using ATG.Items.Equipment;
using UnityEngine;
using UnityEngine.AI;

namespace ATG.Character
{
    [RequireComponent(typeof(Rigidbody), typeof(Collider), typeof(CharacterEquipmentView))]
    public sealed class CharacterView : MonoBehaviour, IEquipmentViewable, IAttackable
    {
        private Rigidbody _rb;
        private Collider _collider;
        private CharacterEquipmentView _equipmentView;
        
        [field: SerializeField] public NavMeshAgent NavAgent { get; private set; }
        
        public Vector3 Position => transform.position;
        public Quaternion Rotation => transform.rotation;

        public event Action<AttackDamageData> OnAttacked; 
        
        public CharacterPresenter MyPresenter { get; private set; }
        
        public void Initialize(CharacterPresenter presenter)
        {
            MyPresenter = presenter;
            
            _rb = GetComponent<Rigidbody>();
            _collider = GetComponent<Collider>();
            _equipmentView = GetComponent<CharacterEquipmentView>();

            _rb.isKinematic = true;
            _collider.isTrigger = false;
            
            _equipmentView.Initialize();
        }
        
        public void SetVisible(bool isVisible)
        {
            _equipmentView.SetVisible(isVisible);
        }
        
        public void SetPhysActive(bool isActive)
        {
            _collider.enabled = isActive;
        }

        public void PutOn(EquipmentViewData data)
        {
            _equipmentView.PutOn(data);
        }

        public void TakeHitByAttacker(AttackDamageData damageData)
        {
            OnAttacked?.Invoke(damageData);
        }
    }
}
