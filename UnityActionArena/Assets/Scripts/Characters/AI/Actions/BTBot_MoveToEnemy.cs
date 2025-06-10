using MBT;
using UnityEngine;
using Random = UnityEngine.Random;

namespace ATG.Character.AI
{
    [AddComponentMenu("")]
    [MBTNode("Bot Actions/Move To Enemy")]
    public class BTBot_MoveToEnemy: BTBot_Action
    {
        private Vector3 _rndOffset;
        
        private Transform _targetEnemy;
        
        public override void OnEnter()
        {
            base.OnEnter();

            _targetEnemy = _bot.WeakestDetectedEnemy.GetEnemyData().Transform;
            CalculateOffset();
        }

        public override NodeResult Execute()
        {
            if (_targetEnemy == null) return NodeResult.failure;
            
            Vector3 targetPosition = _targetEnemy.position + _rndOffset;

            if (Vector3.Distance(_bot.Position, targetPosition) <= _bot.AttackRange) return NodeResult.success;

            if (TryReachPoint(targetPosition, out Vector3 resultPosition) == false)
            {
                CalculateOffset();
            }
            
            _bot.MoveTo(resultPosition);
            return NodeResult.running;
        }

        private void CalculateOffset(int tryCounter = 10)
        {
            if (tryCounter <= 0)
            {
                _rndOffset = Vector3.zero;
                return;
            }
            
            Vector3 targetPosition = _targetEnemy.position;
            Vector2 rnd = Random.insideUnitCircle * _bot.AttackRange;

            rnd.x = Mathf.Clamp(rnd.x, 0.6f * _bot.AttackRange, _bot.AttackRange);
            rnd.y = Mathf.Clamp(rnd.y, 0.6f * _bot.AttackRange, _bot.AttackRange);
            
            _rndOffset = new Vector3(rnd.x, 0, rnd.y);

            if (TryReachPoint(targetPosition + _rndOffset, out _) == false)
            {
                CalculateOffset(tryCounter - 1);
            }
        }

        private bool TryReachPoint(Vector3 target, out Vector3 resultPosition)
        {
            return (_bot.CanReachPosition(target, out resultPosition) == false);
        }
    }
}