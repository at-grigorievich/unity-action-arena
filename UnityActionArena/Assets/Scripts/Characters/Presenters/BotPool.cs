using System;
using System.Collections.Generic;
using ATG.Move;
using ATG.Spawn;
using Settings;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace ATG.Character
{
    [Serializable]
    public sealed class BotCharactersPoolCreator
    {
        [SerializeField] private CharacterView botPrefab;

        public void Create(IContainerBuilder builder)
        {
            builder.Register<BotPool>(Lifetime.Singleton)
                .WithParameter(botPrefab)
                .AsSelf().AsImplementedInterfaces();
        }
    }     
    
    public sealed class BotPool: IInitializable, IDisposable 
    {
        private readonly CharacterView _prefab;
        private readonly TargetNavigationPointSet _targetPointSet;

        private readonly Transform _root;
        
        private List<BotPresenter> _botSet;

        public IEnumerable<BotPresenter> Set => _botSet;
        
        public BotPool(CharacterView prefab, IArenaSize arenaSize, TargetNavigationPointSet pointSet)
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
                CharacterView view = GameObject.Instantiate(_prefab, _root);
                BotPresenter bot = new BotPresenter(view, _targetPointSet);
                
                _botSet.Add(bot);
                
                bot.Initialize();
            }
        }

        public void SetActiveAll(bool isActive)
        {
            foreach (var bot in _botSet)
            {
                bot.SetActive(isActive);
            }
        }
        
        public void Dispose()
        {
            foreach (var botPresenter in _botSet)
            {
                botPresenter.Dispose();
            }
            
            _botSet.Clear();
        }
    }
}