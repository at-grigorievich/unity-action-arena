using ATG.Observable;
using MBT;
using UnityEngine;

namespace ATG.Character.AI
{
    [AddComponentMenu("")]
    [MBTNode("Bot Conditions/Has Detected Enemies")]
    public class BTCondition_DetectEnemies: BTBot_VariableCondition<bool>
    {
        protected override IObservableVar<bool> InitializeVariable(BotPresenter presenter)
        {
            return presenter.HasDetectedEnemies;
        }

        protected override bool CheckCondition()
        {
            return Variable.Value;
        }
    }
}