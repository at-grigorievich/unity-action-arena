using MBT;
using UnityEngine;

namespace ATG.Character.AI
{
    [AddComponentMenu("")]
    [MBTNode("Bot Actions/Move From Attacker")]
    public class BTBot_MoveFromAttacker: BTBot_Action
    {
        [SerializeField] private Vector2 minMaxMoveDistance;
        
        private bool _hasAttacker = false;
        private Vector3 _targetPosition;

        private Vector3[] _directions = new [] { Vector3.zero, Vector3.left, Vector3.right };
        
        public override void OnEnter()
        {
            base.OnEnter();

            _hasAttacker = _bot.LastReceivedDamage.HasValue;
            
            if(_hasAttacker == false) return;

            _directions[0] = _bot.LastReceivedDamage.Value.AttackerDirection;
            _targetPosition = CalculateTargetPosition();
        }

        public override NodeResult Execute()
        {
            if (_hasAttacker == false || _targetPosition == Vector3.zero) return NodeResult.success;
            
            Vector3 tarPos = _targetPosition;
            tarPos.y = _bot.Position.y;
            
            float distance = Vector3.Distance(_bot.Position, tarPos);

            if (distance <= 0.5f)
            {
                return NodeResult.success;
            }
            
            _bot.MoveTo(_targetPosition);
            return NodeResult.running;
        }

        private Vector3 CalculateTargetPosition(int tryCount = 10)
        {
            if (tryCount <= 0) return Vector3.zero;
            
            float rndDistance = UnityEngine.Random.Range(minMaxMoveDistance.x, minMaxMoveDistance.y);

            foreach (var dir in _directions)
            {
                Vector3 targetPosition = _bot.Position + dir * rndDistance;
                if (_bot.CanReachPosition(targetPosition, out Vector3 result) == true)
                {
                    return result;
                }
            }

            tryCount--;
            
            return CalculateTargetPosition(tryCount);
        }
    }
}