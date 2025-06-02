using System;
using UnityEngine;

namespace ATG.Animator
{
    [Serializable]
    public class AnimatorWrapperCreator
    {
        [SerializeField] private AnimatorStateSet animatorSet;
        [SerializeField] private UnityEngine.Animator animator;

        public IAnimatorWrapper Create()
        {
            return new AnimatorWrapper(animator, animatorSet);
        }
    }
}