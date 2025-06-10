using MBT;
using UnityEngine;

namespace ATG.Character.AI
{
    public abstract class BTBot_Condition: Condition
    {
        [SerializeField] private CharacterView botView;
        [SerializeField] private bool invert = false;
        
        protected BotPresenter _bot;
        
        protected virtual void Start()
        {
            if (botView.MyPresenter is not BotPresenter bot)
            {
                throw new UnityException("Presenter is not BotPresenter");
            }
            _bot = bot;
        }
        
        protected abstract bool CheckCondition();
        
        public override bool Check()
        {
            return (CheckCondition() == true) ^ invert;
        }
    }
}