namespace ATG.UI.Service
{
    public sealed class ShowPlayArenaCommand: UIRootCommand
    {
        public ShowPlayArenaCommand(UIRootLocatorService locator) : base(locator) { }

        public override void Execute()
        {
            _locator.TryHideView(UiTag.ArenaRespawn);
            _locator.TryShowView(UiTag.ArenaPlay);
            
            Complete();
        }
    }
}