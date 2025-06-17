using System;
using ATG.Character;
using ATG.Command;
using ATG.Items.Equipment;
using ATG.Spawn;
using ATG.User;

public sealed class SpawnPlayerStep: ICommand
{
    private readonly UserToArenaEquipmentBinder _equipmentBinder;
    private readonly PlayerPresenter _player;

    private readonly ISpawnService _spawnService;
    
    public event Action<bool> OnCompleted;

    public SpawnPlayerStep(UserPresenter userPresenter, PlayerPresenter player, ISpawnService spawnService)
    {
        _equipmentBinder = new UserToArenaEquipmentBinder(userPresenter, player);
        _player = player;
        _spawnService = spawnService;
    }
    
    public void Execute()
    {
        _spawnService.SpawnInstantly(_player);
        _player.OnSpawnRequired += _spawnService.SpawnAfterDelay;
        
        _equipmentBinder.Execute();
        
        _player.SetVisible(false);
        _player.SetPhysActive(true);
        
        OnCompleted?.Invoke(true);
    }
    
    public void Dispose()
    {
        _player.OnSpawnRequired -= _spawnService.SpawnAfterDelay;
    }
}