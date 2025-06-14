using System;
using ATG.Character;
using ATG.Command;
using VContainer.Unity;

public class LobbyEntryPoint : IPostInitializable, IDisposable
{
    private readonly CommandInvoker _stepByStepEntry;

    public LobbyEntryPoint(PlayerPresenter presenter)
    {
        _stepByStepEntry = new CommandInvoker();
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