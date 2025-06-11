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

        private Vector3 _targetPos;
        
        public override void OnEnter()
        {
            base.OnEnter();
            
            int rndAngleIndex = UnityEngine.Random.Range(0, strafeAngles.Length);
            Quaternion rotation = Quaternion.Euler(0f, strafeAngles[rndAngleIndex], 0f);
            
            _targetPos = _bot.Position + rotation * _bot.Forward * directionScale;
        }

        public override NodeResult Execute()
        {
            if (_bot.CanReachPosition(_targetPos, out Vector3 result) == false)
            {
                return NodeResult.success;
            }
            
            Debug.DrawLine(_bot.Position, result, Color.magenta, 1f);

            if (Vector3.Distance(_bot.Position, _targetPos) <= 0.2f)
            {
                return NodeResult.success;
            }
            
            _bot.MoveTo(result);
            return NodeResult.running;
        }
    }
}