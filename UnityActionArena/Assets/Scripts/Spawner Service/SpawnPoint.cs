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
        [SerializeField, ReadOnly] private bool isFreeDebug;
        
        private SphereCollider _collider;

        private HashSet<GameObject> _objectsOnPoint = new();

        public Vector3 Position => transform.position;
        public Quaternion Rotation => transform.rotation;
        
        public override float Radius => _collider.radius;
        
        public bool IsFree => _objectsOnPoint.Count == 0;
        
        private void Awake()
        {
            _collider = GetComponent<SphereCollider>();
            _collider.isTrigger = true;
        }

        private void OnTriggerEnter(Collider other)
        {
            _objectsOnPoint.Add(other.gameObject);
        }

        private void OnTriggerExit(Collider other)
        {
            _objectsOnPoint.Remove(other.gameObject);
        }
        
        private void OnDrawGizmosSelected()
        {
            isFreeDebug = _objectsOnPoint.Count <= 0;
            Gizmos.color = Color.green;
            Gizmos.DrawWireSphere(transform.position, Radius);
        }
    }
}