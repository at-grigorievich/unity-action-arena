using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using VContainer;

namespace ATG.Items.Equipment
{
    [Serializable]
    public sealed class StaticEquipmentSourceCreator
    {
        [SerializeField] private ItemConfig[] configs;

        public void Create(IContainerBuilder builder)
        {
            builder.Register<StaticEquipmentSource>(Lifetime.Singleton)
                .WithParameter(configs.AsEnumerable());
        }

        public IEquipmentSource Create()
        {
            return new StaticEquipmentSource(configs);
        }
    }
    
    public sealed class StaticEquipmentSource: IEquipmentSource
    {
        private readonly IEnumerable<Item> _cached;

        public StaticEquipmentSource(IEnumerable<ItemConfig> configs)
        {
            _cached = configs.Select(config => config.Prototype.Clone());
        }
        
        public IEnumerable<Item> GetItems() => _cached;
    }
}