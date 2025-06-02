namespace ATG.Items.Equipment
{
    public sealed class CharacterEquipViewObserver: IEquipmentObserver
    {
        private readonly Equipment _equipment;
        private readonly IEquipmentViewable _view;

        public CharacterEquipViewObserver(Equipment equipment, IEquipmentViewable view)
        {
            _equipment = equipment;
            _view = view;
        }
        
        public void Initialize()
        {
            _equipment.OnItemTakeOn += OnItemTakeOn;
            _equipment.OnItemTakeOff += OnItemTakeOff;
        }
        
        public void Dispose()
        {
            _equipment.OnItemTakeOn -= OnItemTakeOn;
            _equipment.OnItemTakeOff -= OnItemTakeOff;
        }
        
        public void OnItemTakeOn(Item item)
        {
            if(item.TryGetComponent(out HeroEquipmentComponent equipData) == false) return;

            EquipmentViewData data = new EquipmentViewData(equipData.Type, equipData.Mesh, equipData.Material);
            _view.PutOn(data);
        }

        public void OnItemTakeOff(Item item)
        {
            //No implementation
        }
    }
}