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

        protected override void Configure(IContainerBuilder builder)
        {
            builder.Register<RandomEquipmentSource>(Lifetime.Singleton).AsSelf();
            playerCreator.Create(builder);

            builder.Register<ArenaEntryPoint>(Lifetime.Singleton).AsImplementedInterfaces();
        }
    }
}