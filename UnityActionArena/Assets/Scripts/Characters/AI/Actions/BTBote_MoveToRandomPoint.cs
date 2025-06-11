using ATG.Move;
using MBT;
using UnityEngine;

namespace ATG.Character.AI
{
    [AddComponentMenu("")]
    [MBTNode("Bot Actions/Move To Random Point")]
    public class BTBote_MoveToRandomPoint: BTBot_Action
    {
        [SerializeField] private float minDistance = 0.2f;
        private TargetNavigationPointSet _pointsSet;
        
        private Vector3? _selectedPoint;

        protected override void Start()
        {
            base.Start();
            _pointsSet = _bot.NavigationPoints;
        }

        public override NodeResult Execute()
        {
            if(_bot == null) return NodeResult.failure;
            if (_pointsSet == null) return NodeResult.failure;
            
            if (_selectedPoint == null)
            {
                _selectedPoint = _pointsSet.GetRandomPointInRadiusXZ();
            }
            
            Vector3 tarPos = _selectedPoint.Value;
            tarPos.y = _bot.Position.y;
            
            float distance = Vector3.Distance(botView.Position, tarPos);
            
            if (distance > minDistance)
            {
                _bot.MoveTo(_selectedPoint.Value);
            }
            else
            {
                _selectedPoint = null;
                return NodeResult.success;
            }
            
            return NodeResult.running;
        }
    }
}