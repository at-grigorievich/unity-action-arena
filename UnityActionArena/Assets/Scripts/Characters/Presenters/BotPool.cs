﻿using System;
using System.Collections.Generic;
using ATG.Attack;
using ATG.KillCounter;
using ATG.Move;
using Settings;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace ATG.Character
{
    [Serializable]
    public sealed class BotCharactersPoolCreator
    {
        [SerializeField] private BotPresenterCreator botPresenterCreator;

        public void Create(IContainerBuilder builder)
        {
            builder.Register<BotPool>(Lifetime.Singleton)
                .WithParameter(botPresenterCreator)
                .AsSelf().AsImplementedInterfaces();
        }
    }     
    
    public sealed class BotPool: IInitializable, ITickable, IDisposable, IDieCountable
    {
        private readonly BotPresenterCreator _prefab;
        private readonly TargetNavigationPointSet _targetPointSet;
        
        private readonly Transform _root;
        
        private List<BotPresenter> _botSet;

        public IEnumerable<BotPresenter> Set => _botSet;
        
        public event Action<AttackDamageData> OnDieCountRequired;
        
        public BotPool(BotPresenterCreator prefab, IArenaSize arenaSize, TargetNavigationPointSet pointSet)
        {
            _prefab = prefab;
            
            _botSet = new List<BotPresenter>(arenaSize.PlayersOnArena - 1);
            
            _targetPointSet = pointSet;
            
            _root = new GameObject("bots-root").transform;
        }
        
        public void Initialize()
        {
            for (var i = 0; i < _botSet.Capacity; i++)
            {
                BotPresenterCreator instance = GameObject.Instantiate(_prefab, _root);
                BotPresenter bot = instance.Create(_targetPointSet);
                
                bot.OnDieCountRequired += OnBotDieCountRequired;
                _botSet.Add(bot);
                
                bot.Initialize();
            }
        }

        public void Tick()
        {
            foreach (var botPresenter in _botSet)
            {
                botPresenter.Tick();
            }
        }
        
        public void Dispose()
        {
            foreach (var bot in _botSet)
            {
                bot.OnDieCountRequired += OnBotDieCountRequired;
                bot.Dispose();
            }
            
            _botSet.Clear();
        }
        
        private void OnBotDieCountRequired(AttackDamageData data) => OnDieCountRequired?.Invoke(data);
    }
}