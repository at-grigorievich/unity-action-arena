using ATG.Character;
using ATG.Items.Equipment;
using UnityEngine;
using VContainer.Unity;

namespace Entry_Points
{
    public sealed class ArenaEntryPoint: IStartable
    {
        private readonly StaticEquipmentSource _equipmentSrc;
        private readonly PlayerPresenter _player;

        public ArenaEntryPoint(PlayerPresenter player, StaticEquipmentSource equipmentSrc)
        {
            _player = player;
            _equipmentSrc = equipmentSrc;
        }
        
        public void Start()
        {
            _player.TakeOnEquipments(_equipmentSrc.GetItems());
        }
    }
}