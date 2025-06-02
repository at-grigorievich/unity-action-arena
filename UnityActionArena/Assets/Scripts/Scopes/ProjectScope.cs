using ATG.Input;
using ATG.Items;
using ATG.Items.Equipment;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace Scopes
{
    public sealed class ProjectScope : LifetimeScope
    {
        [SerializeField] private ItemsSetConfig itemsSetConfig;

        protected override void Configure(IContainerBuilder builder)
        {
            builder.RegisterInstance(itemsSetConfig).As<ItemsSetConfig>();
            builder.Register<UserAtBattleEquipmentSource>(Lifetime.Singleton).AsSelf();
            builder.Register<InputService>(Lifetime.Singleton).AsImplementedInterfaces();
        }
    }
}