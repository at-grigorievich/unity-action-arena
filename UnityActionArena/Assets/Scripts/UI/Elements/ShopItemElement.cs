using System;
using ATG.Items;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace ATG.UI
{
    public readonly struct ShopItemViewData
    {
        public readonly string Id;
        public readonly ItemMetaData Meta;
        public readonly bool IsEquipped;
        public readonly bool IsBuyed;

        public ShopItemViewData(string id, ItemMetaData meta, bool isEquipped, bool isBuy)
        {
            Id = id;
            Meta = meta;
            IsEquipped = isEquipped;
            IsBuyed = isBuy;
        }
    }
    
    public class ShopItemElement: UIElement<ShopItemViewData>, IPointerClickHandler
    {
        private static string AlreadyBuyed = "Buyed";
        private static string AlreadyEquipped = "Equipped";
        
        [SerializeField] private UISkinSwitcher selectedSkin;
        [SerializeField] private UISkinSwitcher unselectedSkin;
        [Space(5)]
        [SerializeField] private TMP_Text itemName;
        [SerializeField] private TMP_Text price;
        [SerializeField] private TMP_Text status;
        [SerializeField] private Image icon;
        
        private bool _isActive;
        
        public event Action<ShopItemElement> OnSelect;

        public string SelectedItemId { get; private set; }
        
        protected override void Awake()
        {
            base.Awake();
            Hide();
        }

        public override void Show(object sender, ShopItemViewData shopItem)
        {
            gameObject.SetActive(true);
            
            ItemMetaData meta = shopItem.Meta;
            
            SelectedItemId= shopItem.Id;
            itemName.text = meta.Name;
            price.text = meta.Price.ToString();
            icon.sprite = meta.Icon;
            
            unselectedSkin.Select();
            
            UpdateStatus(shopItem.IsBuyed, shopItem.IsEquipped);
            _isActive = true;
        }

        public override void Hide()
        {
            gameObject.SetActive(false);
            Unselect();
            _isActive = false;
            SelectedItemId = null;
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            if(_isActive == false) return;
            
            Select();
            OnSelect?.Invoke(this);
        }

        public void Select()
        {
            selectedSkin.Select();
        }

        public void Unselect()
        {
            unselectedSkin.Select();
        }

        private void UpdateStatus(bool isBuyed, bool isEquipped)
        {
            if (isEquipped)
            {
                status.text = AlreadyEquipped;
            }
            else if (isBuyed)
            {
                status.text = AlreadyBuyed;
            }
            else
            {
                status.text = string.Empty;
            }
        }
    }
}