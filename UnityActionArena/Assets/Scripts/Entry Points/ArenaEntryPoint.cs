using System;
using ATG.Character;
using ATG.Command;
using ATG.Items.Equipment;
using ATG.KillCounter;
using ATG.Spawn;
using VContainer.Unity;

public sealed class ArenaEntryPoint : IStartable, IDisposable
{
    private readonly ISpawnService _spawnService;
    private readonly IKillCounter _killCounter;

    private readonly CommandInvoker _stepByStepEntry;
    
    public ArenaEntryPoint(PlayerPresenter player, BotPool botPool, 
        StaticEquipmentSource staticEquipmentSrc, RandomEquipmentSource rndEquipmentSrc,
        ISpawnService spawnService, IKillCounter killCounter)
    {
        _spawnService = spawnService;
        _killCounter = killCounter;

        _stepByStepEntry = new CommandInvoker(true,
            new SpawnPlayerStep(player, staticEquipmentSrc, _spawnService),
            new SpawnBotSetStep(botPool, rndEquipmentSrc, _spawnService),
            new ActivateBotSetStep(botPool),
            new ActivatePlayerStep(player));
    }

    public void Start()
    {
        _stepByStepEntry.Execute();
    }

    public void Dispose()
    {
        _stepByStepEntry.Dispose();
        _killCounter.Dispose();
    }
}