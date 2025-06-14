using System;
using ATG.Bank;
using ATG.Items;
using ATG.Items.Equipment;
using ATG.Items.Inventory;
using ATG.Observable;
using ATG.Save;
using Unity.VisualScripting;

namespace ATG.User
{
    public class UserPresenter: IBank<int>
    {
        private readonly UserModel _model;
        private readonly IBank<int> _bank;

        private readonly ISaveService _saveService;
        
        public int Id => _model.Id;
        public string Name => _model.Name;

        public IReadOnlyObservableVar<int> Currency => _bank.Currency;
        public int KillCount => _model.KillCount;
        public int DeathCount => _model.DeathCount;

        public Inventory Inventory => _model.Inventory;
        public Equipment Equipment => _model.Equipment;
        
        public float KD => KillCount <= 0 ? 0f : (float)KillCount / DeathCount;
        
        public UserPresenter(UserModel model, ISaveService saveService)
        {
            _model = model;
            _bank = new CurrencyBank(_model.Currency);
            
            _saveService = saveService;
        }

        public void IncreaseKillCounter(int killAmount)
        {
            if (killAmount <= 0)
            {
                throw new ArgumentException("Kill count cannot be less or equal to zero");
            }
            
            _model.KillCount += killAmount;
            
            _saveService.Save();
        }

        public void IncreaseDeathCounter()
        {
            _model.DeathCount++;
            _saveService.Save();
        }
        
        public void AddCurrency(int amount)
        {
            _bank.AddCurrency(amount);
            _saveService.Save();
        }

        public bool TrySpendCurrency(int amount)
        {
            bool success = _bank.TrySpendCurrency(amount);
            return success;
        }

        public bool TryBuyItem(int amount, params Item[] items)
        {
            bool success = TrySpendCurrency(amount);

            if (success == false) return false;

            foreach (var proto in items)
            {
                Item item = proto.Clone();
                InventoryUseCases.AddItem(Inventory, item);

                if (item.CanEquip() == true)
                {
                    Equipment.TakeOnItem(item);
                }
            }

            _saveService.Save();
            return true;
        }
        
        public void TakeOnEquipments(params Items.Item[] items)
        {
            foreach (var item in items)
            {
                Equipment.TakeOnItem(item);               
            }
            
            _saveService.Save();
        }
    }
}