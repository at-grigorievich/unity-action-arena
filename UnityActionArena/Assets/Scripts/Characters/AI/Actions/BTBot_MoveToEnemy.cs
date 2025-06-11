using MBT;
using UnityEngine;
using Random = UnityEngine.Random;

namespace ATG.Character.AI
{
    [AddComponentMenu("")]
    [MBTNode("Bot Actions/Move To Enemy")]
    public class BTBot_MoveToEnemy: BTBot_Action
    {
        private Transform _targetEnemy;

        private float _rndRangeValue;
        
        public override void OnEnter()
        {
            base.OnEnter();
            
            _rndRangeValue = UnityEngine.Random.Range(0.5f * _bot.AttackRange, _bot.AttackRange);
            _targetEnemy = _bot.WeakestDetectedEnemy.GetEnemyData().Transform;
        }

        public override NodeResult Execute()
        {
            if (_targetEnemy == null) return NodeResult.failure;

            Vector3 targetPosition = _bot.Position + CalculateOffsetByDirection();

            if (TryReachPoint(targetPosition, out Vector3 resultPosition) == false)
            {
                resultPosition = _bot.Position + CalculateOffsetInsideCircle();
            }
            
            //Debug.DrawLine(_bot.Position, resultPosition, Color.magenta, 1f);

            if (Vector3.Distance(_bot.Position, _targetEnemy.position) <= _bot.AttackRange)
            {
                return NodeResult.success;
            }
            
            _bot.MoveTo(resultPosition);
            return NodeResult.running;
        }

        private Vector3 CalculateOffsetByDirection()
        {
            Vector3 targetPosition = _targetEnemy.position;
            Vector3 direction = targetPosition - _bot.Position;
            Vector3 normal = direction.normalized;
            
            return direction - normal * _rndRangeValue;
        }
        
        private Vector3 CalculateOffsetInsideCircle(int tryCounter = 10)
        {
            if (tryCounter <= 0)
            {
                return Vector3.zero;
            }
            
            Vector3 targetPosition = _targetEnemy.position;
            Vector2 rnd = Random.insideUnitCircle * _bot.AttackRange;

            rnd.x = Mathf.Clamp(rnd.x, 0.6f * _bot.AttackRange, _bot.AttackRange);
            rnd.y = Mathf.Clamp(rnd.y, 0.6f * _bot.AttackRange, _bot.AttackRange);
            
            Vector3 rndOffset = new Vector3(rnd.x, 0, rnd.y);

            if (TryReachPoint(targetPosition + rndOffset, out _) == false)
            {
                CalculateOffsetInsideCircle(tryCounter - 1);
            }
            
            return rndOffset;
        }
    
        private bool TryReachPoint(Vector3 target, out Vector3 resultPosition)
        {
            return _bot.CanReachPosition(target, out resultPosition);
        }
    }
}