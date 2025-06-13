using System;
using UnityEngine;
using VContainer;

namespace ATG.KillCounter
{
    [Serializable]
    public sealed class KillCounterCreator
    {
        [SerializeField] private MurderRatesConfig config;

        [SerializeField] private bool isPlayerEarnPerKill;

        public void Create(IContainerBuilder builder)
        {
            if (isPlayerEarnPerKill == true)
            {
                CreatePlayerEarnsPerKillCounter(builder);
                return;
            }
            
            CreateOnlyTableKillCounter(builder);
        }

        private void CreatePlayerEarnsPerKillCounter(IContainerBuilder builder)
        {
            builder.Register<PlayerEarnsPerKillDecorator>(Lifetime.Singleton)
                .WithParameter<MurderRatesConfig>(config)
                .AsImplementedInterfaces();
        }
        
        private void CreateOnlyTableKillCounter(IContainerBuilder builder)
        {
            builder.Register<TableKillCounter>(Lifetime.Singleton)
                .AsImplementedInterfaces();
        }
    }
}