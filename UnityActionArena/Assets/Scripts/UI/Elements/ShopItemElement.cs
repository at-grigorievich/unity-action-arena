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

        public ShopItemViewData(string id, ItemMetaData meta)
        {
            Id = id;
            Meta = meta;
        }
    }
    
    public class ShopItemElement: UIElement<ShopItemViewData>, IPointerClickHandler
    {
        [Serializable]
        private sealed class ShopElementBorderColor
        {
            [SerializeField] private Color colorIdle;
            [SerializeField] private Color colorSelected;
            [Space(5)] 
            [SerializeField] private Image background;
            
            public void SetColor(bool isSelected) => background.color = isSelected ? colorSelected : colorIdle;
        }
        
        [SerializeField] private ShopElementBorderColor borderColor;
        [SerializeField] private TMP_Text itemName;
        [SerializeField] private TMP_Text price;
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
            
            borderColor.SetColor(isSelected: false);

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
            borderColor.SetColor(isSelected: true);
        }

        public void Unselect()
        {
            borderColor.SetColor(isSelected: false);
        }
    }
}