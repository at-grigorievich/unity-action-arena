using System;
using ATG.Command;
using ATG.UI;
using ATG.UI.Service;

public class ShowLobbyUIStep: ICommand
{
    private readonly UIRootLocatorService _uiLocator;
    
    public event Action<bool> OnCompleted;

    public ShowLobbyUIStep(UIRootLocatorService uiLocator)
    {
        _uiLocator = uiLocator;
    }
    
    public void Execute()
    {
        bool success = _uiLocator.TryShowView(UiTag.Lobby);
        
        OnCompleted?.Invoke(success);
    }
    
    public void Dispose()
    {
        _uiLocator.Dispose();
    }
}