using System.Collections.Generic;
using UnityEngine;

namespace JellyButton.Exam.Core.GameEvents
{
    /// <summary>
    /// Part of the scriptable object based event system, its MonoBehaviour representative.
    /// Holds the list of game event listeners, and is responsible for initializing their
    /// subscription to the events, as well as unsubscribing them on disable.
    /// </summary>
    public class GameEventReactions : MonoBehaviour
    {
        [SerializeField] private List<GameEventListener> _gameEventListeners = new();

        private void OnEnable()
        {
            foreach (var gameEventListener in _gameEventListeners)
            {
                gameEventListener._gameEvent.RegisterListener(gameEventListener);
            }
        }

        private void OnDisable()
        {
            foreach (var gameEventListener in _gameEventListeners)
            {
                gameEventListener._gameEvent.UnregisterListener(gameEventListener);
            }
        }
        
        public void AddListener(GameEventListener eventListener)
        {
            if (_gameEventListeners.Contains(eventListener)) return;
            _gameEventListeners.Add(eventListener);
            eventListener._gameEvent.RegisterListener(eventListener);
        }
    
        public void RemoveListener(GameEventListener eventListener)
        {
            if (!_gameEventListeners.Contains(eventListener)) return;
            _gameEventListeners.Remove(eventListener);
            eventListener._gameEvent.UnregisterListener(eventListener);
        }
    }
}
