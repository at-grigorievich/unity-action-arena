using System;
using UnityEngine;
using VContainer;

namespace ATG.Animator
{
    [Serializable]
    public class AnimatorWrapperRegister
    {
        [SerializeField] private AnimatorStateSet animatorSet;
        [SerializeField] private UnityEngine.Animator animator;

        public void Register(IContainerBuilder builder)
        {
            builder.Register<IAnimatorWrapper, AnimatorWrapper>(Lifetime.Singleton)
                .WithParameter(animatorSet)
                .WithParameter(animator);
        }
    }
}