using ATG.Move;
using MBT;
using UnityEngine;

namespace ATG.Character.AI
{
    [AddComponentMenu("")]
    [MBTNode("Bot Actions/Move To Random Point")]
    public class BTNode_MoveToRandomPoint: Leaf
    {
        private const float MinDistance = .2f;
        
        [SerializeField] private CharacterView botView;
        
        private TargetNavigationPointSet _pointsSet;
        private BotPresenter _bot;
        
        private Vector3? _selectedPoint;

        private void Start()
        {
            if (botView.MyPresenter is not BotPresenter bot)
            {
                throw new UnityException("Presenter is not BotPresenter");
            }
            
            _bot = bot;
            _pointsSet = bot.NavigationPoints;
        }
        
        public override NodeResult Execute()
        {
            if(_bot == null) return NodeResult.failure;
            if (_pointsSet == null) return NodeResult.failure;

            if (_selectedPoint == null)
            {
                _selectedPoint = _pointsSet.GetRandomPointInRadiusXZ();
            }

            float distance = Vector3.Distance(botView.Position, _selectedPoint.Value);
            
            if (distance > MinDistance)
            {
                _bot.MoveTo(_selectedPoint.Value);
            }
            else _selectedPoint = null;
            
            return NodeResult.running;
        }
    }
}