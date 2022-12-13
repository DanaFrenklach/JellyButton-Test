using JellyButton.Exam.Core.Data;
using JellyButton.Exam.Core.GameEvents;
using TMPro;
using UnityEngine;

namespace JellyButton.Exam.Game.UI
{
    public class ScoreManager : MonoBehaviour
    {
        [SerializeField] private GameData _gameData;
        [SerializeField] private ScoreRules _scoreRules;
        [SerializeField] private TMP_Text _scoreText;

        [Space] [Header("Game Events")]
        [SerializeField] private GameEvent _onScoreUpdated;

        private void OnEnable()
        {
            GameStateEvents.PreGameState += SetUpScore;
        }

        private void OnDisable()
        {
            GameStateEvents.PreGameState -= SetUpScore;
        }
        
        #region Game Events Listeners

        public void OnPassedAsteroidListener()
        {
            UpdateScore(_scoreRules.PassingAnAsteroid);
        }

        public void OnOneSecondPassedListener()
        {
            UpdateScore(GlobalData.IsSpeeding ? _scoreRules.BoostFlyingPerSecond : _scoreRules.FlyingPerSecond);
        }

        #endregion

        private void SetUpScore()
        {
            _gameData.CurrentScore = 0;
            _scoreText.text = _gameData.CurrentScore.ToString();
        }
        
        private void UpdateScore(int scoreToAdd)
        {
            _gameData.CurrentScore += scoreToAdd;
            _scoreText.text = _gameData.CurrentScore.ToString();
            _onScoreUpdated.Raise();
        }
    }
}
