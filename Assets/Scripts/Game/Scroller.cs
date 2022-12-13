using JellyButton.Exam.Core.Data;
using UnityEngine;

namespace JellyButton.Exam.Game
{
    /// <summary>
    /// Responsible for scrolling whatever it's being used upon.
    /// </summary>
    public class Scroller : MonoBehaviour
    {
        private Transform _transform;
        private bool _shouldScroll;

        private void Awake()
        {
            _transform = transform;
            _shouldScroll = true;
        }

        private void OnEnable()
        {
            GameStateEvents.PreGameState += OnPreGame;
            GameStateEvents.GameOverState += OnGameOver;
        }

        private void OnDisable()
        {
            GameStateEvents.PreGameState -= OnPreGame;
            GameStateEvents.GameOverState -= OnGameOver;
        }

        private void OnPreGame()
        {
            _shouldScroll = true;
        }

        private void OnGameOver()
        {
            _shouldScroll = false;
        }

        private void Update()
        {
            Scroll();
        }

        private void Scroll()
        {
            if (!_shouldScroll) return;
            _transform.position += -Vector3.forward * GlobalData.CurrentScrollSpeed * Time.deltaTime;
        }
    }
}
