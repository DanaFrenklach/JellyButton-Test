using JellyButton.Exam.Core.GameEvents;
using UnityEngine;
using UnityEngine.Events;

namespace Tests.PlayMode
{
    public class TestsListener : MonoBehaviour
    {
        [SerializeField] private GameEventReactions _eventReactions;
        public bool WasRaised { get; private set; }
        
        public void OnListenToGameEvent()
        {
            WasRaised = true;
        }

        public void RegisterEvent(GameEvent gameEvent)
        {
            var response = new UnityEvent();
            response.AddListener(OnListenToGameEvent);
            _eventReactions.AddListener(new GameEventListener(gameEvent, response));
        }
    }
}
