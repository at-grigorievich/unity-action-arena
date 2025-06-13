using System;
using ATG.Character;
using ATG.Command;
using ATG.Items.Equipment;
using ATG.Spawn;

public sealed class SpawnPlayerStep: ICommand
{
    private readonly StaticEquipmentSource _equipmentSrc;
    private readonly PlayerPresenter _player;

    private readonly ISpawnService _spawnService;
    
    public event Action<bool> OnCompleted;

    public SpawnPlayerStep(PlayerPresenter player, StaticEquipmentSource equipmentSrc, ISpawnService spawnService)
    {
        _equipmentSrc = equipmentSrc;
        _player = player;
        _spawnService = spawnService;
    }
    
    public void Execute()
    {
        _player.SetVisible(false);
        _player.SetPhysActive(true);
        
        _spawnService.SpawnInstantly(_player);
        _player.OnSpawnRequired += _spawnService.SpawnAfterDelay;
        
        _player.TakeOnEquipments(_equipmentSrc.GetItems());
        
        OnCompleted?.Invoke(true);
    }
    
    public void Dispose()
    {
        _player.OnSpawnRequired -= _spawnService.SpawnAfterDelay;
    }
}