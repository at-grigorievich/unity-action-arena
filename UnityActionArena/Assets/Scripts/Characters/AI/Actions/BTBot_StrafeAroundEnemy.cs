using MBT;
using UnityEngine;

namespace ATG.Character.AI
{
    [AddComponentMenu("")]
    [MBTNode("Bot Actions/Strafe Around Enemy")]
    public class BTBot_StrafeAroundEnemy: BTBot_Action
    {
        [SerializeField] private float directionScale = 1;
        [SerializeField] private int[] strafeAngles;
        [SerializeField] private bool useBotForward = true;
        
        private Vector3 _targetPos;
        
        public override void OnEnter()
        {
            base.OnEnter();
            
            int rndAngleIndex = UnityEngine.Random.Range(0, strafeAngles.Length);
            Quaternion rotation = Quaternion.Euler(0f, strafeAngles[rndAngleIndex], 0f);

            if (useBotForward == true)
            {
                _targetPos = _bot.Position + rotation * _bot.Forward * directionScale;
            }
            else if (_bot.TargetDetectedEnemy != null)
            {
                Vector3 botPosition = _bot.TargetDetectedEnemy.GetEnemyData().Transform.position;
                Vector3 direction = (botPosition - _bot.Position).normalized;
                _targetPos = _bot.Position + rotation * direction * directionScale;
            }
        }

        public override NodeResult Execute()
        {
            if (_bot.CanReachPosition(_targetPos, out Vector3 result) == false)
            {
                return NodeResult.failure;
            }
            
            Debug.DrawLine(_bot.Position, result, Color.magenta);
            
            Vector3 tarPos = result;
            tarPos.y = _bot.Position.y;
            
            if (Vector3.Distance(_bot.Position, tarPos) <= 0.25f)
            {
                return NodeResult.success;
            }
            
            _bot.MoveTo(result);
            return NodeResult.running;
        }
    }
}