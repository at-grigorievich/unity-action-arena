using System;
using ATG.Character;
using ATG.Command;
using ATG.Save;
using ATG.UI.Service;
using ATG.User;
using VContainer.Unity;

public class LobbyEntryPoint : IStartable, IDisposable
{
    private readonly CommandInvoker _stepByStepEntry;
    private readonly ISaveService _saveService;
    
    public LobbyEntryPoint(UserPresenter user, LobbyCharacterPresenter lobbyCharacter, ISaveService saveService,
        UIRootLocatorService uiLocator)
    {
        _saveService = saveService;
        
        _stepByStepEntry = new CommandInvoker(true,
            new LoadUserDataStep(saveService),
            new ActivateLobbyCharacterStep(user, lobbyCharacter),
            new ShowLobbyUIStep(uiLocator));
    }

    public void Start()
    {
        _stepByStepEntry.Execute();
    }

    public void Dispose()
    {
        _saveService.Save();
        _stepByStepEntry.Dispose();
    }
}