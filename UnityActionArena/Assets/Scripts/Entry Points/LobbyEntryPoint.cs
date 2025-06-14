using System;
using ATG.Character;
using ATG.Command;
using ATG.Save;
using ATG.User;
using VContainer.Unity;

public class LobbyEntryPoint : IPostInitializable, IDisposable
{
    private readonly CommandInvoker _stepByStepEntry;

    public LobbyEntryPoint(UserPresenter user, LobbyCharacterPresenter lobbyCharacter, ISaveService saveService)
    {
        _stepByStepEntry = new CommandInvoker(true,
            new LoadUserDataStep(saveService),
            new ActivateLobbyCharacterStep(user, lobbyCharacter));
    }
    
    public void PostInitialize()
    {
        _stepByStepEntry.Execute();
    }

    public void Dispose()
    {
        _stepByStepEntry.Dispose();
    }
}