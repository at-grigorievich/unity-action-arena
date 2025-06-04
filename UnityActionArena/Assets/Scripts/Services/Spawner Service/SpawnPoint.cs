using System.Collections.Generic;
using ATG.Move;
using Sirenix.OdinInspector;
using UnityEngine;

namespace ATG.Spawn
{
    public interface ISpawnPoint
    {
        bool IsFree { get; }
        Vector3 Position { get; }
        Quaternion Rotation { get; }
    }
    
    [RequireComponent(typeof(SphereCollider))]
    public class SpawnPoint: TargetNavigationPoint, ISpawnPoint
    {
        [SerializeField, ReadOnly] private bool isFreeDebug = true;
        
        private SphereCollider _collider;

        private HashSet<GameObject> _objectsOnPoint = new();

        public Vector3 Position => transform.position;
        public Quaternion Rotation => transform.rotation;

        public bool IsFree { get; private set; } = true;
        
        private void Awake()
        {
            _collider = GetComponent<SphereCollider>();
            _collider.isTrigger = true;
        }

        private void OnTriggerEnter(Collider other)
        {
            _objectsOnPoint.Add(other.gameObject);
            IsFree = false;
            isFreeDebug = IsFree;
        }

        private void OnTriggerExit(Collider other)
        {
            _objectsOnPoint.Remove(other.gameObject);
            IsFree = _objectsOnPoint.Count <= 0;
            isFreeDebug = IsFree;
        }
        
        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.green / 2.0f;
            Gizmos.DrawWireSphere(transform.position, Radius);
        }
    }
}