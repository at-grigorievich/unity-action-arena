using UnityEngine;
using UnityEngine.AI;

namespace ATG.Character
{
    [RequireComponent(typeof(Rigidbody), typeof(Collider))]
    public sealed class CharacterView : MonoBehaviour
    {
        private Rigidbody _rb;
        private Collider _collider;

        [field: SerializeField] public UnityEngine.Animator Animator { get; private set; }
        [field: SerializeField] public NavMeshAgent NavAgent { get; private set; }

        public Vector3 Position => transform.position;
        public Quaternion Rotation => transform.rotation;

        private void Awake()
        {
            _rb = GetComponent<Rigidbody>();
            _collider = GetComponent<Collider>();

            _rb.isKinematic = true;
            _collider.isTrigger = false;
        }

        public void SetName(string name)
        {
            gameObject.name = name;
        }

        public void SetActive(bool isActive)
        {
            gameObject.SetActive(isActive);
        }

        public void SetPhysActive(bool isActive)
        {
            _collider.enabled = isActive;
        }
    }
}
