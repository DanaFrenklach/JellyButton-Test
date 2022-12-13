using System.Collections.Generic;
using UnityEngine;

namespace JellyButton.Exam.Core.GameEvents
{
    /// <summary>
    /// Part of the scriptable object based event system.
    /// This is the actual event, which holds a reference to all its listeners,
    /// handles their registration, as well as raises the subscribed methods.
    /// </summary>
    [CreateAssetMenu(menuName = "Game Event")]
    public class GameEvent : ScriptableObject
    {
        private readonly List<GameEventListener> _eventListeners = new();
        [TextArea][SerializeField] private string _description;

        public void Raise()
        {
            for (var i = _eventListeners.Count - 1; i >= 0; i--)
            {
                _eventListeners[i].OnEventRaised();
            }
        }
        
        public void RegisterListener(GameEventListener gameEventListener)
        {
            if (!_eventListeners.Contains(gameEventListener))
            {
                _eventListeners.Add(gameEventListener);
            }
        }
        
        public void UnregisterListener(GameEventListener gameEventListener)
        {
            if (_eventListeners.Contains(gameEventListener))
            {
                _eventListeners.Remove(gameEventListener);
            }
        }
    }
}
