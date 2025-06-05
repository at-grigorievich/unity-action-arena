using System.Collections.Generic;
using ATG.Animator.Event_Dispatcher;
using JetBrains.Annotations;

namespace ATG.Animator
{
    public class AnimatorWrapper: IAnimatorWrapper
    {
        private readonly UnityEngine.Animator _animator;
        private Dictionary<AnimatorTag, IAnimatorState> _statesByTag;
        
        [CanBeNull]
        public AnimatorEventDispatcher EventDispatcher { get; private set; }
        
        public AnimatorWrapper(UnityEngine.Animator animator, AnimatorStateSet data,
            AnimatorEventDispatcher eventDispatcher):this(animator, data)
        {
            EventDispatcher = eventDispatcher;
        }
        
        public AnimatorWrapper(UnityEngine.Animator animator, AnimatorStateSet data)
        {
            _animator = animator;
            _statesByTag = new Dictionary<AnimatorTag, IAnimatorState>(data.GetStatesByTag());
        }
        
        public void SetActive(bool isActive)
        {
            _animator.enabled = isActive;
        }

        public void SelectState(AnimatorTag tag)
        {
            SetState(tag, null);
        }

        public void SetState(AnimatorTag tag, object value)
        {
            if(_statesByTag.ContainsKey(tag) == false)
                throw new KeyNotFoundException($"Animator tag {tag} not found");
            
            _statesByTag[tag].ChangeState(_animator, value);
        }

        public void SetStates(params StateData[] states)
        {
            foreach (var stateData in states)
            {
                SetState(stateData.Tag, stateData.Value);
            }
        }
    }
}