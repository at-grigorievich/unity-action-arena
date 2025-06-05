using ATG.Animator.Event_Dispatcher;

namespace ATG.Animator
{
    public readonly struct StateData
    {
        public readonly AnimatorTag Tag;
        public readonly object Value;

        public StateData(AnimatorTag tag, object value)
        {
            Tag = tag;
            this.Value = value;
        }
    }
    
    public interface IAnimatorWrapper
    {
        AnimatorEventDispatcher EventDispatcher { get; }
        
        void SetActive(bool isActive);

        void SelectState(AnimatorTag tag);
        
        void SetState(AnimatorTag tag, object value);
        void SetStates(params StateData[] states);
    }
}