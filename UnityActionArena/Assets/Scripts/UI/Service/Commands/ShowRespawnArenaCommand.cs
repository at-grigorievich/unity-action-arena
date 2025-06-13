namespace ATG.UI.Service
{
    public sealed class ShowRespawnArenaCommand: UIRootCommand
    {
        public ShowRespawnArenaCommand(UIRootLocatorService locator) : base(locator)
        {
        }

        public override void Execute()
        {
            _locator.TryHideView(UiTag.ArenaPlay);
            _locator.TryShowView(UiTag.ArenaRespawn);
            
            Complete();
        }
    }
}