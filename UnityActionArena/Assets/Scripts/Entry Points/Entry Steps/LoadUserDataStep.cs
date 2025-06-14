using System;
using ATG.Command;
using ATG.Save;

public class LoadUserDataStep: ICommand
{
    private readonly ISaveService _saveService;
    
    public event Action<bool> OnCompleted;

    public LoadUserDataStep(ISaveService saveService)
    {
        _saveService = saveService;
    }
    
    public void Execute()
    {
        _saveService.Load();
        OnCompleted?.Invoke(true);
    }
    
    public void Dispose()
    {
    }
}
