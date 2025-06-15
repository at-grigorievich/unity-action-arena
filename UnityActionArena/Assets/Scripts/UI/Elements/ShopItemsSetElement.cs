using System;
using System.Collections.Generic;
using System.Linq;
using ATG.Items;
using ATG.User;
using UnityEngine;

namespace ATG.UI
{
    public readonly struct ShopItemsSetUIData
    {
        public readonly UserPresenter User;
        public readonly IReadOnlyList<Item> Items;

        public ShopItemsSetUIData(UserPresenter user, IReadOnlyList<Item> items)
        {
            User = user;
            Items = items;
        }
    }
    
    public class ShopItemsSetElement: UIElement<ShopItemsSetUIData>, IDisposable
    {
        [SerializeField] private RectTransform root;

        private ShopItemElement[] _elements;

        private int _activeCount;
        private IReadOnlyList<Item> _items;

        public event Action<Item> OnItemSelected; 
        
        protected override void Awake()
        {
            base.Awake();
            
            _elements = root.GetComponentsInChildren<ShopItemElement>(includeInactive: true);
        }

        public override void Show(object sender, ShopItemsSetUIData data)
        {
            Dispose();
            
            var items = data.Items;
            
            _activeCount = items.Count;
            _items = items.OrderBy(i => i.MetaData.Price).ToList();
            
            for (var i = 0; i < _elements.Length; i++)
            {
                if (i >= _activeCount - 1)
                {
                    _elements[i].Hide();
                    continue;
                }
                
                Item item = _items[i];

                bool isBuyed = data.User.HasInInventory(item);
                bool isEquipped = data.User.IsAlreadyEquipped(item);
                
                _elements[i].Show(this, new ShopItemViewData(item.Id, item.MetaData, isEquipped, isBuyed ));
                _elements[i].OnSelect += OnSelectElement;
            }
        }

        public override void Hide()
        {
            foreach (var e in _elements)
            {
                e.Hide();
                e.OnSelect -= OnSelectElement;
            }
            
            _items = null;
        }
        
        public void Dispose()
        {
            foreach (var e in _elements)
            {
                e.OnSelect -= OnSelectElement;
            }

            _items = null;
        }
        
        private void OnSelectElement(ShopItemElement obj)
        {
            UnselectAllExcept(obj);

            Item selectedItem = _items.FirstOrDefault(i => i.Id == obj.SelectedItemId);
            
            if(selectedItem == null)
                throw new NullReferenceException("Selected Item is null");
            
            OnItemSelected?.Invoke(selectedItem);
        }

        private void UnselectAllExcept(ShopItemElement obj)
        {
            for (int i = 0; i < _activeCount; i++)
            {
                var e = _elements[i];
                
                if(ReferenceEquals(e, obj) == true) continue;
                
                e.Unselect();
            }
        }
    }
}