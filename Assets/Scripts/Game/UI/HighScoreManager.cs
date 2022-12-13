using JellyButton.Exam.Core.Data;
using TMPro;
using UnityEngine;

namespace JellyButton.Exam.Game.UI
{
    public class HighScoreManager : MonoBehaviour
    {
        [SerializeField] private GameData _gameData;
        [SerializeField] private TMP_Text _highScore;

        private const string HighScorePlayerPrefs = "HighScore";

        private void OnEnable()
        {
            GameStateEvents.PreGameState += InitializeHighScore;
        }
        
        private void OnDisable()
        {
            GameStateEvents.PreGameState -= InitializeHighScore;
        }

        private void InitializeHighScore()
        {
            _gameData.HighScore = PlayerPrefs.GetInt(HighScorePlayerPrefs);
            _gameData.DidBeatHighScore = false;
            _highScore.text = _gameData.HighScore.ToString();
        }

        public void OnScoreUpdatedListener()
        {
            OnUpdateHighScore();
        }
        
        private void OnUpdateHighScore()
        {
            if (_gameData.CurrentScore <= _gameData.HighScore) return;

            _gameData.DidBeatHighScore = true;
            _gameData.HighScore = _gameData.CurrentScore;
            _highScore.text = _gameData.HighScore.ToString();
            
            PlayerPrefs.SetInt(HighScorePlayerPrefs, _gameData.HighScore);
        }
    }
}
