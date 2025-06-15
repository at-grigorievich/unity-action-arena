using ATG.Items;
using TMPro;
using UnityEngine;

namespace ATG.UI
{
    public readonly struct ShopItemDescriptionData
    {
        public readonly string ItemName;
        public readonly string ItemDescription;

        public ShopItemDescriptionData(ItemMetaData meta)
        {
            ItemName = meta.Name;
            ItemDescription = meta.Description;
        }
    }
    
    public class ShopItemDescription: UIElement<ShopItemDescriptionData>
    {
        [SerializeField] private TMP_Text itemNameOutput;
        [SerializeField] private TMP_Text itemDescriptionOutput;
        
        public override void Show(object sender, ShopItemDescriptionData data)
        {
            gameObject.SetActive(true);
            
            itemNameOutput.text = data.ItemName;
            itemDescriptionOutput.text = data.ItemDescription;
        }

        public override void Hide()
        {
            gameObject.SetActive(false);
        }
    }
}