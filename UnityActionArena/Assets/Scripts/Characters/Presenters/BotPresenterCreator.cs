using ATG.Move;
using Settings;
using UnityEngine;

namespace ATG.Character
{
    [RequireComponent(typeof(CharacterView))]
    public sealed class BotPresenterCreator: MonoBehaviour
    {
        [SerializeField] private BotCharacterCreator creator;

        public BotPresenter Create(TargetNavigationPointSet navigationPointSet, IStaminaReset staminaReset) => 
            creator.Create(navigationPointSet, staminaReset);
    }
}