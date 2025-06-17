namespace ATG.Items.Equipment
{
    public interface IUseEquipment
    {
        void TakeOnEquipments(params Items.Item[] items);
    }
}