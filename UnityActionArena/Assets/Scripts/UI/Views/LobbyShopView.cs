using System;
using System.Collections.Generic;
using System.Linq;
using ATG.Bank;
using ATG.Character;
using ATG.Items;
using ATG.OtusHW.Inventory;
using ATG.User;
using UI.Views;
using UnityEngine;

namespace ATG.UI
{
    public readonly struct LobbyShopViewData
    {
        public readonly IBank<int> CurrencyBank;
        public readonly IEnumerable<Item> UserItems;
        public readonly ItemsSetConfig AllItems;
        public readonly LobbyCharacterPresenter LobbyCharacter;
        
        public LobbyShopViewData(UserPresenter userPresenter, LobbyCharacterPresenter lobbyCharacter, ItemsSetConfig allItems)
        {
            CurrencyBank = userPresenter;
            AllItems = allItems;
            LobbyCharacter = lobbyCharacter;
            UserItems = userPresenter.Equipment.Items.Values;
        }
    }
    
    public sealed class LobbyShopView: UIView<LobbyShopViewData>, IDisposable
    {
        [SerializeField] private CounterOutput currencyOutput;
        [SerializeField] private ShopItemsSetElement itemsSet;
        [SerializeField] private EquipmentTypeCheckboxSet checkboxes;
        
        private PlayerCurrencyOutput _userCurrency;
        
        private ItemsSetConfig _allItems;
        private LobbyCharacterPresenter _lobbyCharacter;
        private IEnumerable<Item> _userItems;
        
        private Item _selectedItem;
        
        protected override void Awake()
        {
            base.Awake();

            _userCurrency = new PlayerCurrencyOutput(currencyOutput);
        }
        
        public override void Show(object sender, LobbyShopViewData data)
        {
            base.Show(sender, data);

            _lobbyCharacter = data.LobbyCharacter;
            _allItems = data.AllItems;
            _userItems = data.UserItems;
            
            checkboxes.OnSelect += OnCheckboxSelected;
            itemsSet.OnItemSelected += OnItemSelected;
            
            checkboxes.Show(this, EquipType.Body);
            
            _userCurrency.Show(data.CurrencyBank.Currency);
        }

        public override void Hide()
        {
            base.Hide();

            ResetLobbyCharacterEquipment();
            
            itemsSet.Hide();
            checkboxes.Hide();
            
            _userCurrency.Hide();
            
            checkboxes.OnSelect -= OnCheckboxSelected;
            itemsSet.OnItemSelected -= OnItemSelected;
            
            _lobbyCharacter = null;
            _selectedItem = null;
            _allItems = null;
            _userItems = null;
        }

        public void Dispose()
        {
            _userCurrency.Dispose();
            itemsSet.Dispose();
            
            itemsSet.OnItemSelected -= OnItemSelected;
        }
        
        private void OnCheckboxSelected(EquipType equipType)
        {
            _selectedItem = null;
            
            ResetLobbyCharacterEquipment();
            itemsSet.Show(this, _allItems.GetEquipByType(equipType));
        }
        
        private void OnItemSelected(Item obj)
        {
            _selectedItem = obj;
            _lobbyCharacter.TakeOnEquipments(_selectedItem);
        }

        private void ResetLobbyCharacterEquipment()
        {
            if(_lobbyCharacter == null || _userItems == null) return;
            _lobbyCharacter.TakeOnEquipments(_userItems.ToArray());
        }
    }
}