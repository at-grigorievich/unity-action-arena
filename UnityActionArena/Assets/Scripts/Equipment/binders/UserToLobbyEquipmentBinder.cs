using System;
using ATG.Character;
using ATG.User;

namespace ATG.Items.Equipment
{
    public sealed class UserToLobbyEquipmentBinder: UserToEquipmentUserBinder<LobbyCharacterPresenter>, IDisposable
    {
        public UserToLobbyEquipmentBinder(UserPresenter user, LobbyCharacterPresenter lobby) 
            : base(user, lobby) { }

        public override void Execute()
        {
            base.Execute();
            _userPresenter.Equipment.OnItemTakeOn += OnItemTakeOn;
        }

        public void Dispose()
        {
            _userPresenter.Equipment.OnItemTakeOn -= OnItemTakeOn;
        }
    }
}