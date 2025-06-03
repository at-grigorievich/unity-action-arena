using ATG.Animator;
using ATG.Items.Equipment;
using UnityEngine;
using UnityEngine.AI;

namespace ATG.Character
{
    [RequireComponent(typeof(Rigidbody), typeof(Collider), typeof(CharacterEquipmentView))]
    public sealed class CharacterView : MonoBehaviour, IEquipmentViewable
    {
        private Rigidbody _rb;
        private Collider _collider;
        private CharacterEquipmentView _equipment;

        [field: SerializeField] public AnimatorWrapperCreator AnimatorWrapperCreator { get; set; }
        [field: SerializeField] public NavMeshAgent NavAgent { get; private set; }
        
        public Vector3 Position => transform.position;
        public Quaternion Rotation => transform.rotation;
        
        public CharacterPresenter MyPresenter { get; private set; }
        
        public void Initialize(CharacterPresenter presenter)
        {
            MyPresenter = presenter;
            
            _rb = GetComponent<Rigidbody>();
            _collider = GetComponent<Collider>();
            _equipment = GetComponent<CharacterEquipmentView>();

            _rb.isKinematic = true;
            _collider.isTrigger = false;
            
            _equipment.Initialize();
        }
        
        public void SetName(string newName)
        {
            gameObject.name = newName;
        }

        public void SetActive(bool isActive)
        {
            gameObject.SetActive(isActive);
        }

        public void SetPhysActive(bool isActive)
        {
            _collider.enabled = isActive;
        }

        public void PutOn(EquipmentViewData data)
        {
            _equipment.PutOn(data);
        }
    }
}
