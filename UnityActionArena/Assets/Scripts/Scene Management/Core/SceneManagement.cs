using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace ATG.SceneManagement
{
    public class SceneManagement: ISceneManagement
    {
        private CancellationTokenSource _cts;
        
        public SceneInfoData CurrentScene { get; private set; }

        public async UniTask LoadBySceneInfoAsync(SceneInfoData sceneInfo, bool isAdditive = false)
        {
            if (ReferenceEquals(sceneInfo, CurrentScene) == true)
            {
                Debug.Log($"Scene {sceneInfo.name} is already loaded");
                return;
            }
            
            Cancel();
            int? sceneIndex = sceneInfo.GetBuildSettingsIndex();

            if (sceneIndex.HasValue == false) return;
            
            _cts = new CancellationTokenSource();
            
            if (isAdditive == true)
            {
                await LoadBySceneInfoAdditiveAsync(sceneIndex.Value, _cts.Token);
            }
            else
            {
                await LoadBySceneInfoSingleAsync(sceneIndex.Value, _cts.Token);
            }
            
            CurrentScene = sceneInfo;
        }

        public void SetupLoadedScene(SceneInfoData sceneInfo)
        {
            string currentSceneName = SceneManager.GetActiveScene().name;

            if (sceneInfo.SceneName != currentSceneName)
                throw new Exception($"Invalid scene info config! Loaded scene name is {currentSceneName}");
            
            CurrentScene = sceneInfo;
        }

        public void Cancel()
        {
            _cts?.Cancel();
            _cts?.Dispose();
            _cts = null;
        }
        
        public void Dispose()
        {
            CurrentScene = null;
            
            _cts?.Cancel();
            _cts?.Dispose();
            _cts = null;
        }

        private async UniTask LoadBySceneInfoSingleAsync(int sceneIndex, CancellationToken token)
        {
            AsyncOperation operation = SceneManager.LoadSceneAsync(sceneIndex, LoadSceneMode.Single);
            await operation.ToUniTask(cancellationToken: token);
        }

        private async UniTask LoadBySceneInfoAdditiveAsync(int sceneIndex, CancellationToken token)
        {
            throw new NotImplementedException();
        }
    }
}