using ATG.Character;
using ATG.Items.Equipment;
using Entry_Points;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace Scopes
{
    public sealed class ArenaScope: LifetimeScope
    {
        [SerializeField] private PlayerCharacterCreator playerCreator;
        [SerializeField] private StaticEquipmentSourceCreator staticEquipmentSourceCreator;
        
        protected override void Configure(IContainerBuilder builder)
        {
            staticEquipmentSourceCreator.Create(builder);
            playerCreator.Create(builder);

            builder.Register<ArenaEntryPoint>(Lifetime.Singleton).AsImplementedInterfaces();
        }
    }
}