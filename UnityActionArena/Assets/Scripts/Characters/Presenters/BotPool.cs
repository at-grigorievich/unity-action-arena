using System;
using System.Collections.Generic;
using ATG.Items.Equipment;
using ATG.Move;
using Settings;
using UnityEngine;
using UnityEngine.Serialization;
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
    
    public sealed class BotPool: IInitializable, IDisposable 
    {
        private readonly BotPresenterCreator _prefab;
        private readonly TargetNavigationPointSet _targetPointSet;
        
        private readonly RandomEquipmentSource _equipmentSource;
        
        private readonly Transform _root;
        
        private List<BotPresenter> _botSet;

        public IEnumerable<BotPresenter> Set => _botSet;
        
        public BotPool(BotPresenterCreator prefab, IArenaSize arenaSize, RandomEquipmentSource equipmentSource,
            TargetNavigationPointSet pointSet)
        {
            _prefab = prefab;
            
            _botSet = new List<BotPresenter>(arenaSize.PlayersOnArena - 1);
            
            _equipmentSource = equipmentSource;
            
            _targetPointSet = pointSet;
            
            _root = new GameObject("bots-root").transform;
        }
        
        public void Initialize()
        {
            for (var i = 0; i < _botSet.Capacity; i++)
            {
                BotPresenterCreator instance = GameObject.Instantiate(_prefab, _root);
                BotPresenter bot = instance.Create(_targetPointSet);
                
                _botSet.Add(bot);
                
                bot.Initialize();
                bot.TakeOnEquipments(_equipmentSource.GetItems());
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