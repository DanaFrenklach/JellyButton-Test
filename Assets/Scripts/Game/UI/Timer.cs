using System.Collections;
using JellyButton.Exam.Core.Data;
using JellyButton.Exam.Core.GameEvents;
using JellyButton.Exam.Core.Utils;
using TMPro;
using UnityEngine;

namespace JellyButton.Exam.Game.UI
{
    public class Timer : MonoBehaviour
    {
        [SerializeField] private GameData _gameData;
        [SerializeField] private TMP_Text _minutes;
        [SerializeField] private TMP_Text _seconds;

        [Space] [Header("Game Events")]
        [SerializeField] private GameEvent _onOneSecondPassed;
        
        private Coroutine _timeUpdaterRoutine;
        private int _currentSeconds;
        private int _currentMinutes;

        private void OnEnable()
        {
            GameStateEvents.InGameState += OnInGameState;
            GameStateEvents.GameOverState += GameOverState;
        }

        private void OnDisable()
        {
            GameStateEvents.InGameState -= OnInGameState;
            GameStateEvents.GameOverState -= GameOverState;
        }

        private void OnInGameState()
        {
            SetUpTimer();
        }

        private void GameOverState()
        {
            StopCoroutine(_timeUpdaterRoutine);
        }

        private void SetUpTimer()
        {
            _currentSeconds = 0;
            _currentMinutes = 0;
            UpdateTime();
            
            _timeUpdaterRoutine = StartCoroutine(TimeUpdater());
        }

        private IEnumerator TimeUpdater()
        {
            while (true)
            {
                yield return new WaitForSeconds(1f);
                
                if (_currentSeconds < 59)
                {
                    _currentSeconds++;
                }
                else
                {
                    _currentMinutes++;
                    _currentSeconds = 0;
                }

                UpdateTime();
                _onOneSecondPassed.Raise();
            }
        }

        private void UpdateTime()
        {
            var minutesString = Utility.NumberToTimeString(_currentMinutes);
            var secondsString = Utility.NumberToTimeString(_currentSeconds);
            _minutes.text = minutesString;
            _seconds.text = secondsString;

            _gameData.CurrentTime = new Vector2Int(_currentMinutes, _currentSeconds);
        }
    }
}
