using ATG.Move;
using UnityEngine;

namespace ATG.Character
{
    [RequireComponent(typeof(ArenaCharacterView))]
    public sealed class BotPresenterCreator: MonoBehaviour
    {
        [SerializeField] private BotCharacterCreator creator;

        public BotPresenter Create(TargetNavigationPointSet navigationPointSet) => 
            creator.Create(navigationPointSet);
    }
}