using ATG.Items.Equipment;
using UnityEngine;
using UnityEngine.AI;
using VContainer.Unity;

namespace ATG.Character
{
    [RequireComponent(typeof(Rigidbody), typeof(Collider), typeof(CharacterEquipmentView))]
    public sealed class CharacterView : MonoBehaviour, IInitializable, IEquipmentViewable
    {
        private Rigidbody _rb;
        private Collider _collider;
        private CharacterEquipmentView _equipment;

        [field: SerializeField] public UnityEngine.Animator Animator { get; private set; }
        [field: SerializeField] public NavMeshAgent NavAgent { get; private set; }

        public Vector3 Position => transform.position;
        public Quaternion Rotation => transform.rotation;

        public void Initialize()
        {
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
            Debug.Log(data.Type);
            _equipment.PutOn(data);
        }
    }
}
