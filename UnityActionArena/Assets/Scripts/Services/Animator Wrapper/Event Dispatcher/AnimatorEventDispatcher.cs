using System;
using System.Collections.Generic;
using UnityEngine;

namespace ATG.Animator.Event_Dispatcher
{
    [RequireComponent(typeof(UnityEngine.Animator))]
    public class AnimatorEventDispatcher : MonoBehaviour
    {
        private readonly Dictionary<AnimatorEventType, Action> _eventTable = new();
        
        public void Subscribe(AnimatorEventType eventType, Action callback)
        {
            if (_eventTable.TryGetValue(eventType, out var existing))
            {
                _eventTable[eventType] = existing + callback;
            }
            else
            {
                _eventTable[eventType] = callback;
            }
        }
        
        public void Unsubscribe(AnimatorEventType eventType, Action callback)
        {
            if (!_eventTable.TryGetValue(eventType, out var existing)) return;
            
            existing -= callback;
            if (existing == null)
                _eventTable.Remove(eventType);
            else
                _eventTable[eventType] = existing;
        }
        
        public void OnEvent(string eventName)
        {
            string eventNameFormatted = eventName.ToUpperInvariant();
            
            //Debug.Log(eventNameFormatted);
            
            if(Enum.TryParse(eventNameFormatted, out AnimatorEventType eventType) == false)
                throw new Exception($"Invalid event name: {eventName}... Check AnimatorEventType.cs for available events.");
            
            if (_eventTable.TryGetValue(eventType, out var callback))
            {
                callback?.Invoke();
            }
        }
        
        private void OnDestroy()
        {
            _eventTable.Clear();
        }
    }
}