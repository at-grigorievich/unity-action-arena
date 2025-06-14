using ATG.Input;
using ATG.Items;
using ATG.Save;
using UnityEngine;
using ATG.User;
using VContainer;
using VContainer.Unity;

namespace Scopes
{
    public sealed class ProjectScope : LifetimeScope
    {
        [SerializeField] private UserSaveLoaderCreator userSaveLoaderCreator;
        [Space(5)]
        [SerializeField] private ItemsSetConfig itemsSetConfig;
        
        protected override void Configure(IContainerBuilder builder)
        {
            RegisterConfigs(builder);
            RegisterServices(builder);
            
            RegisterUser(builder);
        }

        private void RegisterConfigs(IContainerBuilder builder)
        {
            builder.RegisterInstance(itemsSetConfig).As<ItemsSetConfig>();
        }

        private void RegisterServices(IContainerBuilder builder)
        {
            builder.Register<InputService>(Lifetime.Singleton).AsImplementedInterfaces();

            builder.Register<SerializableRepository>(Lifetime.Singleton).AsImplementedInterfaces();
            builder.Register<ISaveService, SaveLoadersService>(Lifetime.Singleton);
        }
        
        private void RegisterUser(IContainerBuilder builder)
        {
            UserModel model = new UserModel();
            
            userSaveLoaderCreator.Create(builder, model);

            builder.Register<UserPresenter>(Lifetime.Singleton)
                .WithParameter<UserModel>(model)
                .AsSelf().AsImplementedInterfaces();
        }
    }
}