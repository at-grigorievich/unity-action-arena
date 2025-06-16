namespace ATG.SceneManagement
{
    public sealed class LobbySceneLoader: SceneLoader
    {
        private readonly ISceneManagement _sceneManager;
        private readonly SceneInfoData _lobbyScene;
        
        public LobbySceneLoader(ISceneManagement sceneManager, SceneInfoData lobbyScene) : base(sceneManager)
        {
            _lobbyScene = lobbyScene;
        }

        protected override SceneInfoData GetSceneInfo()
        {
            return _lobbyScene;
        }
    }
}