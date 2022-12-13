using JellyButton.Exam.Core.Data;
using TMPro;
using UnityEngine;

namespace JellyButton.Exam.Game.UI
{
    public class AsteroidsPassedStatsManager : MonoBehaviour
    {
        [SerializeField] private GameData _gameData;
        [SerializeField] private TMP_Text _asteroidsPassed;

        private void Awake()
        {
            SetUpAsteroidsPassedAmount();
        }

        private void OnEnable()
        {
            GameStateEvents.PreGameState += SetUpAsteroidsPassedAmount;
        }

        private void OnDisable()
        {
            GameStateEvents.PreGameState -= SetUpAsteroidsPassedAmount;
        }

        public void OnPassedAsteroidListener()
        {
            UpdateAsteroidsPassedAmount();
        }
        
        private void SetUpAsteroidsPassedAmount()
        {
            _gameData.AsteroidsPassed = 0;
            _asteroidsPassed.text = _gameData.AsteroidsPassed.ToString();
        }

        private void UpdateAsteroidsPassedAmount()
        {
            _gameData.AsteroidsPassed++;
            _asteroidsPassed.text = _gameData.AsteroidsPassed.ToString();
        }
    }
}
