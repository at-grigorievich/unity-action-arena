using System;
using ATG.Character;
using ATG.Command;
using ATG.Items.Equipment;
using ATG.KillCounter;
using ATG.Spawn;
using ATG.User;
using VContainer.Unity;

public sealed class ArenaEntryPoint : IStartable, IDisposable
{
    private readonly ISpawnService _spawnService;
    private readonly IKillCounter _killCounter;

    private readonly CommandInvoker _stepByStepEntry;
    
    public ArenaEntryPoint(UserPresenter user, PlayerPresenter player, BotPool botPool, 
        RandomEquipmentSource rndEquipmentSrc, ISpawnService spawnService, IKillCounter killCounter)
    {
        _spawnService = spawnService;
        _killCounter = killCounter;

        _stepByStepEntry = new CommandInvoker(true,
            new SpawnPlayerStep(user, player, _spawnService),
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