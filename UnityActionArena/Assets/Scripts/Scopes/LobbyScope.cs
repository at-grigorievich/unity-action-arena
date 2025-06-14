using ATG.Character;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace Scopes
{
    public class LobbyScope: LifetimeScope
    {
        [SerializeField] private LobbyCharacterCreator lobbyCharacterCreator;
        
        protected override void Configure(IContainerBuilder builder)
        {
            lobbyCharacterCreator.Create(builder);
        }
    }
}