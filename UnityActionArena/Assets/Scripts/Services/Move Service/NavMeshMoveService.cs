using ATG.Observable;
using UnityEngine;
using UnityEngine.AI;

namespace ATG.Move
{
    public class NavMeshMoveService: IMoveableService
    {
        private const float _smoothTime = 5f;
        
        private readonly IReadOnlyObservableVar<float> _speedVariable;
        private readonly NavMeshAgent _agent;
        
        private readonly NavMeshPath _path;
        private NavMeshHit _hit;
        
        public NavMeshMoveService(NavMeshAgent agent, IReadOnlyObservableVar<float> speedVariable)
        {
            _agent = agent;
            _speedVariable = speedVariable;
            _path = new NavMeshPath();
        }

        public void SetActive(bool isActive)
        {
            _agent.enabled = isActive;
        }

        public void MoveTo(Vector3 position)
        {
            if(_agent.enabled == false) return;
            
            _agent.isStopped = false;
            _agent.speed = _speedVariable.Value;
            _agent.SetDestination(position);
            
            RotateToPosition(position);
        }

        public void PlaceTo(Vector3 position, Quaternion rotation)
        {
            SetActive(false);
            _agent.transform.position = position;
            _agent.transform.rotation = rotation;
            SetActive(true);
        }

        public bool CanReach(Vector3 inputPosition, out Vector3 resultPosition)
        {
            resultPosition = inputPosition;
            
            if(_agent.enabled == false) return false;
            
            if (NavMesh.SamplePosition(inputPosition, out _hit, 1.0f, NavMesh.AllAreas) != true) 
                return false;
            
            if (_agent.CalculatePath(_hit.position, _path) != true) 
                return false;
            
            resultPosition = _hit.position;
            return true;
        }

        public void Stop()
        {
            if(_agent.enabled == false) return;
            
            _agent.speed = 0f;
            _agent.isStopped = true;
        }

        private void RotateToPosition(Vector3 position)
        {
            Transform transform = _agent.transform;
            
            Vector3 dir = position - transform.position;
            
            transform.rotation = Quaternion.Lerp(transform.rotation,
                Quaternion.LookRotation(dir),_smoothTime * Time.deltaTime);
        }
    }
}