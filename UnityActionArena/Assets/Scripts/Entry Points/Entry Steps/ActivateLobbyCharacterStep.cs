
using System;
using ATG.Character;
using ATG.Command;
using ATG.Items.Equipment;
using ATG.User;

public class ActivateLobbyCharacterStep : ICommand
{
    private readonly LobbyCharacterPresenter _lobbyCharacter;
    private readonly UserToLobbyEquipmentBinder _equipmentBinder;

    public event Action<bool> OnCompleted;

    public ActivateLobbyCharacterStep(UserPresenter user, LobbyCharacterPresenter lobbyCharacter)
    {
        _lobbyCharacter = lobbyCharacter;
        _equipmentBinder = new UserToLobbyEquipmentBinder(user, lobbyCharacter);
    }
    
    public void Execute()
    {
        _equipmentBinder.Initialize();
        OnCompleted?.Invoke(true);
    }

    public void Dispose()
    {
        _equipmentBinder?.Dispose();
    }
}
