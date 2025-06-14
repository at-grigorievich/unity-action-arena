using ATG.Character;
using ATG.UI.Service;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace Scopes
{
    public class LobbyScope: LifetimeScope
    {
        [SerializeField] private LobbyCharacterCreator lobbyCharacterCreator;
        [SerializeField] private UIRootLocatorCreator uiRootLocatorCreator;
        
        protected override void Configure(IContainerBuilder builder)
        {
            lobbyCharacterCreator.Create(builder);
            uiRootLocatorCreator.Create(builder);
            builder.Register<LobbyEntryPoint>(Lifetime.Singleton).AsImplementedInterfaces();
        }
    }
}