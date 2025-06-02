using ATG.Character;
using ATG.Items.Equipment;
using VContainer.Unity;

namespace Entry_Points
{
    public sealed class ArenaEntryPoint: IStartable
    {
        private readonly RandomEquipmentSource _rndEquipmentSrc;
        private readonly PlayerPresenter _player;

        public ArenaEntryPoint(PlayerPresenter player, RandomEquipmentSource rndEquipmentSrc)
        {
            _player = player;
            _rndEquipmentSrc = rndEquipmentSrc;
        }
        
        public void Start()
        {
            _player.TakeOnEquipments(_rndEquipmentSrc.GetItems());
        }
    }
}