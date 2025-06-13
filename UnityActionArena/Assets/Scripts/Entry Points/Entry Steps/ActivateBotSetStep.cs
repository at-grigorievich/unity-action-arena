using System;
using ATG.Character;
using ATG.Command;

public sealed class ActivateBotSetStep : ICommand
{
    private readonly BotPool _pool;
    
    public event Action<bool> OnCompleted;

    public ActivateBotSetStep(BotPool pool)
    {
        _pool = pool;
    }
    
    public void Execute()
    {
        foreach (var botPresenter in _pool.Set)
        {
            botPresenter.SetVisible(true);
            botPresenter.SetActive(true);
            botPresenter.AllowActivatedOnSpawn = true;
        }
        
        OnCompleted?.Invoke(true);
    }

    public void Dispose()
    {

    }
}