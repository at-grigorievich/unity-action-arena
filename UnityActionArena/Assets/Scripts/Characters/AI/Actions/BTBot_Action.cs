using MBT;
using UnityEngine;

namespace ATG.Character.AI
{
    public abstract class BTBot_Action: Leaf
    {
        [SerializeField] protected CharacterView botView;
        
        protected BotPresenter _bot;
        
        protected virtual void Start()
        {
            if (botView.MyPresenter is not BotPresenter bot)
            {
                throw new UnityException("Presenter is not BotPresenter");
            }
            
            _bot = bot;
        }
    }
}