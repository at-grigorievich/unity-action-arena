namespace ATG.SceneManagement
{
    public sealed class Arena1SceneLoader: SceneLoader
    {
        private readonly SceneInfoData _sceneInfo;
        
        public Arena1SceneLoader(ISceneManagement sceneManager, SceneInfoData sceneInfo) : base(sceneManager)
        {
            _sceneInfo = sceneInfo;
        }

        protected override SceneInfoData GetSceneInfo()
        {
            return _sceneInfo;
        }
    }
}