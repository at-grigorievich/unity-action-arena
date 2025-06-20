using ATG.Observable;
using MBT;
using UnityEngine;

namespace ATG.Character.AI
{
    [AddComponentMenu("")]
    [MBTNode("Bot Conditions/Is Get Damage")]
    public sealed class BTCondition_IsGetDamage: BTBot_VariableCondition<bool>
    {
        protected override IObservableVar<bool> InitializeVariable(BotPresenter presenter)
        {
            return presenter.IsGetDamage;
        }

        protected override bool CheckCondition()
        {
            return Variable.Value == true;
        }

        protected override void OnVariableChange(bool newValue)
        {
            if(newValue == false) return;
            base.OnVariableChange(newValue);
        }
    }
}