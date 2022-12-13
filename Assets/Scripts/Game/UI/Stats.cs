using JellyButton.Exam.Core.Data;
using JellyButton.Exam.Core.Utils;
using TMPro;
using UnityEngine;

namespace JellyButton.Exam.Game.UI
{
    public class Stats : MonoBehaviour
    {
        [SerializeField] private GameData _gameData;
        
        [Space][Header("Stats References")]
        [SerializeField] private TMP_Text _scoreTMP;
        [SerializeField] private TMP_Text _TimeMinutesTMP;
        [SerializeField] private TMP_Text _TimeSecondsTMP;
        [SerializeField] private TMP_Text _asteroidsPassed;
        [SerializeField] private GameObject _beatHighScore;

        private void OnEnable()
        {
            GameStateEvents.GameOverState += UpdateStats;
        }
        private void OnDisable()
        {
            GameStateEvents.GameOverState -= UpdateStats;
        }

        private void UpdateStats()
        {
            _scoreTMP.text = _gameData.CurrentScore.ToString();
            _TimeMinutesTMP.text =  Utility.NumberToTimeString(_gameData.CurrentTime.x);
            _TimeSecondsTMP.text = Utility.NumberToTimeString(_gameData.CurrentTime.y);
            _asteroidsPassed.text = _gameData.AsteroidsPassed.ToString();
            _beatHighScore.SetActive(_gameData.DidBeatHighScore);
        }
    }
}
