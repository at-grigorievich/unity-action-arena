using System;
using System.Linq;
using ATG.Character;
using ATG.Command;
using ATG.Items.Equipment;
using ATG.Spawn;

public sealed class SpawnBotSetStep : ICommand
{
    private readonly RandomEquipmentSource _equipmentSrc;
    private readonly BotPool _pool;
    
    private readonly ISpawnService _spawnService;
    
    public event Action<bool> OnCompleted;

    public SpawnBotSetStep(BotPool pool, RandomEquipmentSource equipmentSrc, ISpawnService spawnService)
    {
        _pool = pool;
        _equipmentSrc = equipmentSrc;
        _spawnService = spawnService;
    }
    
    public void Execute()
    {
        foreach (var bot in _pool.Set)
        {
            bot.SetVisible(false);
            bot.SetPhysActive(true);
            
            _spawnService.SpawnInstantly(bot);
            bot.OnSpawnRequired += _spawnService.SpawnAfterDelay;
            
            bot.TakeOnEquipments(_equipmentSrc.GetItems().ToArray());
        }
        
        OnCompleted?.Invoke(true);
    }
    
    public void Dispose()
    {
        foreach (var bot in _pool.Set)
        {
            bot.OnSpawnRequired -= _spawnService.SpawnAfterDelay;
        }
    }
}
