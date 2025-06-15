using System;
using System.Collections.Generic;
using System.Linq;
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
        public readonly UserPresenter UserPresenter;
        public readonly IEnumerable<Item> UserItems;
        public readonly ItemsSetConfig AllItems;
        public readonly LobbyCharacterPresenter LobbyCharacter;
        
        public LobbyShopViewData(UserPresenter userPresenter, LobbyCharacterPresenter lobbyCharacter, ItemsSetConfig allItems)
        {
            UserPresenter = userPresenter;
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
        [SerializeField] private StatProgressBarSet itemStats;
        [SerializeField] private ShopItemDescription itemDescription;
        [SerializeField] private CurrencyButton buyButton;
        [SerializeField] private EquipButton equipButton;
        
        private PlayerCurrencyOutput _userCurrency;
        
        private UserPresenter _userPresenter;
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
            _userPresenter = data.UserPresenter;
            
            checkboxes.OnSelect += OnCheckboxSelected;
            itemsSet.OnItemSelected += OnItemSelected;
            
            buyButton.OnClicked += OnBuyButtonClicked;
            equipButton.OnClicked += OnEquipButtonClicked;
            
            checkboxes.Show(this, EquipType.Body);
            
            _userCurrency.Show(_userPresenter.Currency);
            
            buyButton.Hide();
            equipButton.Hide();
        }
        public override void Hide()
        {
            base.Hide();

            ResetLobbyCharacterEquipment();
            
            buyButton.Hide();
            equipButton.Hide();
            
            itemsSet.Hide();
            checkboxes.Hide();
            itemStats.Hide();
            itemDescription.Hide();
            
            _userCurrency.Hide();
            
            checkboxes.OnSelect -= OnCheckboxSelected;
            itemsSet.OnItemSelected -= OnItemSelected;
            buyButton.OnClicked -= OnBuyButtonClicked;
            
            _lobbyCharacter = null;
            _selectedItem = null;
            _allItems = null;
            _userItems = null;
            _userPresenter = null;
        }
        public void Dispose()
        {
            _userCurrency.Dispose();
            itemsSet.Dispose();
            buyButton.Dispose();
            
            buyButton.OnClicked -= OnBuyButtonClicked;
            itemsSet.OnItemSelected -= OnItemSelected;
        }
        
        private void OnCheckboxSelected(EquipType equipType)
        {
            ResetSelectedItem();
            
            ResetLobbyCharacterEquipment();
            
            itemsSet.Show(this, _allItems.GetEquipByType(equipType));
        }
        
        private void OnItemSelected(Item obj)
        {
            _selectedItem = obj;
            _lobbyCharacter.TakeOnEquipments(_selectedItem);
            
            itemDescription.Show(this, new ShopItemDescriptionData(_selectedItem.MetaData));
            itemStats.Show(this, _selectedItem);
            
            ShowBuyButton();
            ShowEquipButton();
        }

        private void ResetSelectedItem()
        {
            ResetLobbyCharacterEquipment();
            itemDescription.Hide();
            itemStats.Hide();
            buyButton.Hide();
            
            _selectedItem = null;
        }

        private void ShowBuyButton()
        {
            if(_selectedItem == null) return;
            
            if (_selectedItem.CanStack() == false)
            {
                if (_userPresenter.HasInInventory(_selectedItem) == true)
                {
                    buyButton.Hide();
                }
                else
                {
                    buyButton.Show(this, new CurrencyButtonData(_userPresenter, _selectedItem.MetaData.Price));
                }
                return;
            }
            
            buyButton.Show(this, new CurrencyButtonData(_userPresenter, _selectedItem.MetaData.Price));
        }

        private void ShowEquipButton()
        {
            if(_selectedItem == null) return;
            if(_selectedItem.CanEquip() == false) return;
            
            equipButton.Show(this, null);
            
            if (_userPresenter.IsAlreadyEquipped(_selectedItem) == false &&
                _userPresenter.HasInInventory(_selectedItem) == true)
            {
                equipButton.Activate();
                return;
            }
            
            equipButton.Deactivate();
        }

        private void OnEquipButtonClicked()
        {
            if(_selectedItem == null) return;
            if(_selectedItem.CanEquip() == false) return;
            
            _userPresenter.EquipItem(_selectedItem);
            OnItemSelected(_selectedItem);
        }
        
        private void OnBuyButtonClicked()
        {
            if(_selectedItem == null) return;
            if (_userPresenter.TryBuyItem(_selectedItem) == true)
            {
                OnItemSelected(_selectedItem);
            }
        }
        
        private void ResetLobbyCharacterEquipment()
        {
            if(_lobbyCharacter == null || _userItems == null) return;
            _lobbyCharacter.TakeOnEquipments(_userItems.ToArray());
        }
    }
}