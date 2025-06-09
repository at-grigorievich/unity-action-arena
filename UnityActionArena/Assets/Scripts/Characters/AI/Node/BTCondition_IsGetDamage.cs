using ATG.Observable;
using MBT;
using UnityEngine;

namespace ATG.Character.AI
{
    [AddComponentMenu("")]
    [MBTNode("Bot Conditions/Is Get Damage")]
    public class BTCondition_IsGetDamage: BTBot_Condition<bool>
    {
        protected override IObservableVar<bool> InitializeVariable(BotPresenter presenter)
        {
            return presenter.IsGetDamage;
        }

        protected override bool CheckCondition()
        {
            return Variable.Value == true;
        }
    }
}