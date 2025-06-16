using System;
using Cysharp.Threading.Tasks;

namespace ATG.SceneManagement
{
    public interface ISceneManagement: IDisposable
    {
        public SceneInfoData CurrentScene { get; }
        
        UniTask LoadBySceneInfoAsync(SceneInfoData sceneInfo, bool isAdditive = false);

        void SetupLoadedScene(SceneInfoData sceneInfo);
        void Cancel();
    }
}