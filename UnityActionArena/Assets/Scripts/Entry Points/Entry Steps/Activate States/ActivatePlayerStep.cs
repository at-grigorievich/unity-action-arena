using System;
using ATG.Character;
using ATG.Command;

public sealed class ActivatePlayerStep : ICommand
{
    private readonly PlayerPresenter _playerPresenter;
    
    public event Action<bool> OnCompleted;

    public ActivatePlayerStep(PlayerPresenter playerPresenter)
    {
        _playerPresenter = playerPresenter;
    }
    
    public void Execute()
    {
        _playerPresenter.SetVisible(true);
        _playerPresenter.SetActive(true);
        _playerPresenter.AllowActivatedOnSpawn = true;
        
        OnCompleted?.Invoke(true);
    }
    
    public void Dispose()
    {

    }
}
