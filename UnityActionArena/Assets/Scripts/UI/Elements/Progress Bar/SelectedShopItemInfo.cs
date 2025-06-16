using System;
using ATG.Items;
using UnityEngine;

namespace ATG.UI
{
    public sealed class SelectedShopItemInfo: UIElement<Item>, IDisposable
    {
        [SerializeField] private StatProgressBarSet itemStats;
        [SerializeField] private ShopItemDescription itemDescription;
        
        public override void Show(object sender, Item data)
        {
            itemDescription.Show(this, new ShopItemDescriptionData(data.MetaData));
            itemStats.Show(this, data);
        }

        public override void Hide()
        {
            itemDescription.Hide();
            itemStats.Hide();
        }

        public void Dispose()
        {
            itemStats.Dispose();
        }
    }
}