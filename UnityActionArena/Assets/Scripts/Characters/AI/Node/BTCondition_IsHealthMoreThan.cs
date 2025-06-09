using ATG.Observable;
using MBT;
using UnityEngine;

namespace ATG.Character.AI
{
    [AddComponentMenu("")]
    [MBTNode("Bot Conditions/Is Health More Than")]
    public sealed class BTCondition_IsHealthMoreThan: BTBot_Condition<int>
    {
        [SerializeField] private int healthLevel;
        
        protected override IObservableVar<int> InitializeVariable(BotPresenter presenter)
        {
            return presenter.Health;
        }

        protected override bool CheckCondition()
        {
            return Variable.Value > healthLevel;
        }
    }
}