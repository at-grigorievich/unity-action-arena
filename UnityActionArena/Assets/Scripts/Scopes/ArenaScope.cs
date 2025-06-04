using ATG.Character;
using ATG.Items.Equipment;
using ATG.Move;
using ATG.Spawn;
using Entry_Points;
using Settings;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace Scopes
{
    public sealed class ArenaScope: LifetimeScope
    {
        [SerializeField] private ArenaSettings arenaSettings;
        [SerializeField] private SpawnServiceCreator spawnServiceCreator;
        
        [SerializeField] private PlayerCharacterCreator playerCreator;
        [SerializeField] private BotCharactersPoolCreator botPoolCreator;
        [SerializeField] private StaticEquipmentSourceCreator staticEquipmentSourceCreator;
        
        [SerializeField] private TargetNavigationPointSet targetNavigationPointSet;
        
        protected override void Configure(IContainerBuilder builder)
        {
            staticEquipmentSourceCreator.Create(builder);
            playerCreator.Create(builder);
            botPoolCreator.Create(builder);
            
            spawnServiceCreator.Create(builder);

            builder.RegisterInstance(arenaSettings).AsImplementedInterfaces();
            builder.RegisterInstance(targetNavigationPointSet);
            
            builder.Register<ArenaEntryPoint>(Lifetime.Singleton).AsImplementedInterfaces();
        }
    }
}