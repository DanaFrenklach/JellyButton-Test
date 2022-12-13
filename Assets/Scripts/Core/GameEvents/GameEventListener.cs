using System;
using UnityEngine;
using UnityEngine.Events;

namespace JellyButton.Exam.Core.GameEvents
{
    /// <summary>
    /// Part of the scriptable object based event system.
    /// This class is used in the inspector in order to create a pairing
    /// between the game event and the Unity Event response to it.
    /// </summary>
    [Serializable]
    public class GameEventListener
    {
        public GameEvent _gameEvent;
        [Tooltip("Response to invoke when the above event is raised.")]
        public UnityEvent _response;

        public GameEventListener(GameEvent gameEvent, UnityEvent response)
        {
            _gameEvent = gameEvent;
            _response = response;
        }

        public void OnEventRaised()
        {
            _response?.Invoke();
        }
    }
}
