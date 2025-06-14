using System;
using System.Linq;
using ATG.Items;
using ATG.Items.Equipment;
using ATG.Items.Inventory;
using ATG.Save;
using UnityEngine;
using VContainer;

namespace ATG.User
{
    public struct UserData
    {
        public int Id;
        public string Name;
        
        public string[] Inventory;
        public string[] Equipment;

        public int Currency;
        public int KillCount;
        public int DeathCount;
    }

    [Serializable]
    public sealed class UserSaveLoaderCreator
    {
        [SerializeField] private StaticEquipmentSourceCreator defaultEquipmentCreator;

        public void Create(IContainerBuilder builder, UserModel model)
        {
            builder.Register<UserSaveLoader>(Lifetime.Singleton)
                .WithParameter<IEquipmentSource>(defaultEquipmentCreator.Create)
                .WithParameter<UserModel>(model)
                .As<ISaveLoader>();
        }
    }
    
    public sealed class UserSaveLoader : SaveLoader<UserModel, UserData>
    {
        private readonly IEquipmentSource _defaultSource;
        private readonly ItemsSetConfig _allItemsData;
        
        protected override string DATA_KEY => "user-model-data";
        
        public UserSaveLoader(IEquipmentSource defaultEquipment, ItemsSetConfig allItemsData,
            ISerializableRepository serializableRepository, UserModel dataService) 
            : base(serializableRepository, dataService)
        {
            _defaultSource = defaultEquipment;
            _allItemsData = allItemsData;
        }
        
        protected override UserData ConvertToData()
        {
            UserData data;

            data.Id = _dataService.Id;
            data.Name = _dataService.Name;

            data.Inventory = InventoryUseCases.GetItemsIdArray(_dataService.Inventory);
            data.Equipment = EquipmentUseCases.GetItemsIdArray(_dataService.Equipment);

            data.Currency = _dataService.Currency.Value;

            data.KillCount = _dataService.KillCount;
            data.DeathCount = _dataService.DeathCount;

            return data;
        }

        protected override void SetupData(UserData resourcesSet)
        {
            _dataService.Id = resourcesSet.Id;
            _dataService.Name = resourcesSet.Name;
            
            _dataService.Currency.Value = resourcesSet.Currency;
            
            _dataService.KillCount = resourcesSet.KillCount;
            _dataService.DeathCount = resourcesSet.DeathCount;
            
            foreach (var itemId in resourcesSet.Inventory)
            {
                Item item = _allItemsData.GetPrototypeById(itemId).Clone();
                
                InventoryUseCases.AddItem(_dataService.Inventory, item);

                if (resourcesSet.Equipment.Contains(itemId) == true)
                {
                    _dataService.Equipment.TakeOnItem(item);
                }
            }
        }

        protected override void SetupInitialData()
        {
            _dataService.Id = Guid.NewGuid().GetHashCode();
            _dataService.Name = "Player_1";
            _dataService.Currency.Value = 0;
            _dataService.KillCount = 0;
            _dataService.DeathCount = 0;
            
            var defaultItems = _defaultSource.GetItems();
            
            foreach (var defaultItem in defaultItems)
            {
                InventoryUseCases.AddItem(_dataService.Inventory, defaultItem);

                if (defaultItem.CanEquip() == true)
                {
                    _dataService.Equipment.TakeOnItem(defaultItem);
                }
            }
        }
    }
}