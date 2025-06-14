using System;
using ATG.Bank;
using ATG.Observable;

namespace User
{
    public class UserPresenter: IBank<int>
    {
        private readonly UserModel _model;
        private readonly IBank<int> _bank;
        
        public int Id => _model.Id;
        public string Name => _model.Name;

        public IReadOnlyObservableVar<int> Currency => _bank.Currency;
        public int KillCount => _model.KillCount;
        public int DeathCount => _model.DeathCount;

        public float KD => KillCount <= 0 ? 0f : (float)KillCount / DeathCount;
        

        public UserPresenter(UserModel model)
        {
            _model = model;
            _bank = new CurrencyBank(_model.Currency);
        }

        public void IncreaseKillCounter(int killAmount)
        {
            if (killAmount <= 0)
            {
                throw new ArgumentException("Kill count cannot be less or equal to zero");
            }
            
            _model.KillCount += killAmount;
        }

        public void IncreaseDeathCounter()
        {
            _model.DeathCount++;
        }
        
        public void AddCurrency(int amount)
        {
            _bank.AddCurrency(amount);
        }

        public bool TrySpendCurrency(int amount)
        {
            bool success = _bank.TrySpendCurrency(amount);
            return success;
        }
    }
}