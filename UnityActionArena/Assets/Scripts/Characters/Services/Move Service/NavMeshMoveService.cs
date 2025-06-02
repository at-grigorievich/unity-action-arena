using ATG.Observable;
using UnityEngine;
using UnityEngine.AI;

namespace ATG.Character.Move
{
    public class NavMeshMoveService: IMoveableService
    {
        private const float _smoothTime = 5f;
        
        private readonly IReadOnlyObservableVar<float> _speedVariable;
        private readonly NavMeshAgent _agent;
        
        public NavMeshMoveService(NavMeshAgent agent, IReadOnlyObservableVar<float> speedVariable)
        {
            _agent = agent;
            _speedVariable = speedVariable;
        }

        public void SetActive(bool isActive)
        {
            _agent.enabled = isActive;
            _agent.isStopped = true;
        }

        public void MoveTo(Vector3 position)
        {
            _agent.isStopped = false;
            _agent.speed = _speedVariable.Value;
            _agent.SetDestination(position);
            
            RotateToPosition(position);
        }

        public void Stop()
        {
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