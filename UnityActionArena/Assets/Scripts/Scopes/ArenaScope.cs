using ATG.Character;
using ATG.Items.Equipment;
using ATG.Move;
using Entry_Points;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace Scopes
{
    public sealed class ArenaScope: LifetimeScope
    {
        [SerializeField] private PlayerCharacterCreator playerCreator;
        [SerializeField] private BotCharacterCreator botCreator;
        [SerializeField] private StaticEquipmentSourceCreator staticEquipmentSourceCreator;
        
        [SerializeField] private TargetNavigationPointSet targetNavigationPointSet;
        
        protected override void Configure(IContainerBuilder builder)
        {
            staticEquipmentSourceCreator.Create(builder);
            playerCreator.Create(builder);
            botCreator.Create(builder);

            builder.RegisterInstance(targetNavigationPointSet);
            
            builder.Register<ArenaEntryPoint>(Lifetime.Singleton).AsImplementedInterfaces();
        }
    }
}