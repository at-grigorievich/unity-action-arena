using System;
using ATG.Character;
using ATG.User;

namespace ATG.Items.Equipment
{
    public sealed class UserToLobbyEquipmentBinder: IDisposable
    {
        private readonly UserPresenter _userPresenter;
        private readonly LobbyCharacterPresenter _lobbyCharacterPresenter;

        public UserToLobbyEquipmentBinder(UserPresenter user, LobbyCharacterPresenter lobby)
        {
            _userPresenter = user;
            _lobbyCharacterPresenter = lobby;
        }
        
        public void Initialize()
        {
            foreach (var item in _userPresenter.Equipment.Items.Values)
            {
                OnItemTakeOn(item);
            }

            _userPresenter.Equipment.OnItemTakeOn += OnItemTakeOn;
        }

        public void Dispose()
        {
            _userPresenter.Equipment.OnItemTakeOn -= OnItemTakeOn;
        }
        
        private void OnItemTakeOn(Item obj)
        {
            _lobbyCharacterPresenter.TakeOnEquipments(obj);
        }
    }
}