using System;
using Cinemachine;
using UnityEngine;
using VContainer;

namespace ATG.Camera
{
    [Serializable]
    public sealed class CinemachineWrapperCreator
    {
        [SerializeField] private CinemachineVirtualCamera topDownCamera;
        [SerializeField] private CinemachineVirtualCamera showCamera;

        public void Create(IContainerBuilder builder)
        {
            builder.Register<CinemachineWrapper>(Lifetime.Singleton)
                .WithParameter("topDownCm", topDownCamera)
                .WithParameter("showCm", showCamera);
        }
    }
    
    public sealed class CinemachineWrapper
    {
        private readonly CinemachineVirtualCamera _topdownCinemachine;
        private readonly CinemachineVirtualCamera _showCinemachine;

        public CinemachineWrapper(CinemachineVirtualCamera topDownCm, CinemachineVirtualCamera showCm)
        {
            _topdownCinemachine = topDownCm;
            _showCinemachine = showCm;
            
            EnableShow();
        }

        public void SelectPlayerTarget(bool isSelected)
        {
            if(isSelected) EnableTopDown();
            else EnableShow();
        }
        
        public void EnableTopDown()
        {
            _showCinemachine.enabled = false;
            _topdownCinemachine.enabled = true;
        }

        public void EnableShow()
        {
            _topdownCinemachine.enabled = false;
            _showCinemachine.enabled = true;
        }
            
    }
}