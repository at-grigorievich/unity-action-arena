using ATG.Character;
using ATG.SceneManagement;
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
        [SerializeField] private SceneInfoData arena1SceneInfo;
        
        protected override void Configure(IContainerBuilder builder)
        {
            lobbyCharacterCreator.Create(builder);
            uiRootLocatorCreator.Create(builder);

            builder.Register<Arena1SceneLoader>(Lifetime.Singleton)
                .WithParameter<SceneInfoData>(arena1SceneInfo).As<SceneLoader>();
            
            builder.Register<LobbyEntryPoint>(Lifetime.Singleton).AsImplementedInterfaces();
        }
    }
}